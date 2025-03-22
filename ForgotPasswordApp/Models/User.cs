namespace ForgotPasswordApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }  // In real apps, hash the password
    }
}
