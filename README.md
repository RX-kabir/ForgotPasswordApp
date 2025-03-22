# Forgot Password App

This is a simple web application built using ASP.NET MVC 5 where users can register, log in, and reset their passwords. It is designed to demonstrate basic user authentication features such as login, registration, and password reset functionalities.

## Features
- User registration (creates an account).
- Login system with username and password.
- Forgot password feature to reset passwords.
- Basic front-end design with Bootstrap.
- In-memory database simulation (for simplicity).

## Technologies Used
- ASP.NET MVC 5
- C#
- SQL Server (for storing user data)
- Bootstrap (for front-end design)
- jQuery (for client-side scripting)

## Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/RX-kabir/ForgotPasswordApp.git
    ```

2. Open the solution in Visual Studio.

3. Ensure that you have the necessary dependencies installed:
   - .NET Framework 4.8
   - SQL Server (for database setup)

4. Set up the database by executing the SQL script to create a **Users** table.

5. Run the application via Visual Studio by pressing **F5** or using the "Start" button.

6. Access the app via your browser: `http://localhost:your-port`

## Usage

1. Open the application in your browser.
2. Go to the **Login** page, where you can log in using your credentials.
3. If you don’t have an account, navigate to the **Register** page to create a new account.
4. If you forget your password, use the **Forgot Password** link to reset your password.

## Contributing

Feel free to open issues or submit pull requests if you would like to contribute!

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes and commit (`git commit -am 'Add new feature'`).
4. Push to your branch (`git push origin feature-branch`).
5. Create a new Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
