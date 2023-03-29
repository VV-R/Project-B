using System;
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
            if (CheckLogin(((string)emailText.Text), ((string)passwordText.Text))) {
                MainWindow.LoginButton.Text = "Uitloggen";
                if ((string)emailText.Text == "admin@admin.com"){
                    WindowManager.CurrentColor = Colors.TopLevel;
                    WindowManager.GoForwardOne(new AdminMenu((string)emailText.Text));
                } else {
                    WindowManager.GoForwardOne(new UserMenu((string)emailText.Text));
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

    private bool CheckLogin(string email, string password)
    {
        // TODO: Add database check here
        // TODO: Change the return type to a user class instead of bool

        return (email == "admin@admin.com" && password == "password") ||
                (email == "user@user.com" && password == "password");
    }
}