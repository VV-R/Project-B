using System;
using System.Net.Mail;
using Terminal.Gui;

public class LoginScreen : Toplevel
{
    public LoginScreen()
    {
        Label emailLabel = new Label() {
            Text = "E-mailadres:",
        };

        TextField emailText = new TextField("") {
            X = Pos.Right(emailLabel) + 1,
            Width = Dim.Percent(20),
        };

        Label passwordLabel = new Label() {
            Text = "Wachtwoord:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1,
        };

        TextField passwordText = new TextField("") {
            Secret = true,
            X = Pos.Left(emailText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Percent(20),
        };

        CheckBox passwordCheckBox = new CheckBox() {
            X = Pos.Right(passwordText) + 1,
            Y = Pos.Top(passwordLabel),
        };

        passwordCheckBox.Toggled += (e) => {
            if (!e)
                passwordText.Secret = false;
            else passwordText.Secret = true;
        };


        Button loginButton = new Button() {
            Text = "Log in",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(passwordLabel) + 1,
        };

        loginButton.Clicked += () => {
            User? user = CheckLogin(((string)emailText.Text), ((string)passwordText.Text));
            if (user != null) {
                WindowManager.CurrentUser = user;
                MainWindow.LoginButton.Text = "Uitloggen";
                if ((string)emailText.Text == "admin@admin.com"){
                    WindowManager.CurrentColor = Colors.TopLevel;
                    WindowManager.GoForwardOne(new AdminMenu());
                } else {
                    WindowManager.GoForwardOne(new UserMenu());
                }
            } else {
                MessageBox.ErrorQuery("Logging In", "Verkeerd gebruikersnaam of wachtwoord.", "Ok");
            }
        };

        Button registerButton = new Button() {
            Text = "Registreren",
            X = Pos.Right(loginButton) + 1,
            Y = Pos.Top(loginButton),
        };

        registerButton.Clicked += () => { WindowManager.GoForwardOne(new RegisterMenu()); };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(registerButton) + 1,
            Y = Pos.Top(loginButton),
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(emailLabel, emailText, passwordLabel, passwordText, passwordCheckBox, loginButton, registerButton ,backButton);
    }

    private User? CheckLogin(string email, string password)
    {
        // TODO: Add database check here

        if (email == "admin@admin.com" && password == "password")
            return new User(0, "Levi", "van", "Daalen", "password", new MailAddress("admin@admin.com"),
                            "+31|613856964", new DateTime(2004, 1, 19), "Nederland");
        if (email == "user@user.com" && password == "password")
            return new User(1, "Levi", "van", "Daalen", "password", new MailAddress("user@user.com"),
                            "+31|613856964", new DateTime(2004, 1, 19), "Nederland");
        return null;
    }
}