using ForgotPasswordApp.Models;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace ForgotPasswordApp.Controllers
{
    public class AuthController : Controller
    {
        // GET: Register (Display registration form)
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // POST: Register (Create user and store in database)
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Use database to check if user already exists
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", model.UserName);
                    int userCount = (int)cmd.ExecuteScalar();

                    if (userCount > 0) // Check if username already exists
                    {
                        ModelState.AddModelError("", "Username already exists.");
                        return View(model);
                    }

                    // Create new user and insert into the database
                    string insertQuery = "INSERT INTO Users (UserName, Password) VALUES (@UserName, @Password)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@UserName", model.UserName);
                    insertCmd.Parameters.AddWithValue("@Password", model.Password); // In real apps, hash the password!
                    insertCmd.ExecuteNonQuery();
                }

                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: Login (Display login form)
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login (Validate user credentials using database)
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", model.UserName);
                    cmd.Parameters.AddWithValue("@Password", model.Password); // In real apps, hash the password!
                    int userCount = (int)cmd.ExecuteScalar();

                    if (userCount > 0) // If valid user is found
                    {
                        // Create a session to keep the user logged in
                        Session["UserName"] = model.UserName;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid username or password.");
                        return View(model);
                    }
                }
            }

            return View(model);
        }

        // Logout action
        public ActionResult Logout()
        {
            Session.Clear();  // Clear session data to log out
            return RedirectToAction("Login");
        }

        // GET: ForgotPassword (Display forgot password form)
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        // POST: ForgotPassword
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Checking
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserName", model.UserName);
                    int userCount = (int)cmd.ExecuteScalar();

                    if (userCount == 0)
                    {
                        ModelState.AddModelError("", "User not found.");
                        return View(model);
                    }

                    // Update phase
                    string updateQuery = "UPDATE Users SET Password = @NewPassword WHERE UserName = @UserName";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, conn);
                    updateCmd.Parameters.AddWithValue("@UserName", model.UserName);
                    updateCmd.Parameters.AddWithValue("@NewPassword", model.NewPassword); // In real apps, hash the password!
                    updateCmd.ExecuteNonQuery();
                }

                ViewBag.Message = "Password updated successfully!";
                return View();
            }

            return View(model);
        }
    }
}
