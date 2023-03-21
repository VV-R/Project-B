using System;
using Terminal.Gui;

public class LoginMenu : Toplevel
{
    public LoginMenu()
    {
        Button loginButton = new Button() {
            Text = "Inloggen",
        };

        loginButton.Clicked += () => { WindowManager.SetWindow(this, new LoginScreen()); };

        Button registerButton = new Button() {
            Text = "Registreren",
            Y = Pos.Bottom(loginButton),
        };

        registerButton.Clicked += () => { WindowManager.SetWindow(this, new RegisterMenu()); };

        Button guestButton = new Button() {
            Text = "Doorgaan als gast",
            Y = Pos.Bottom(registerButton),
        };

        guestButton.Clicked += () => { WindowManager.SetWindow(this, new UserMenu("guest")); };

        Button exitButton = new Button() {
            Text = "Afsluiten",
            Y = Pos.Bottom(guestButton),
        };

        exitButton.Clicked += () => { Application.RequestStop();};

        Add(loginButton, registerButton, guestButton, exitButton);
    }
}
public class LoginScreen : Toplevel
{
    public LoginScreen()
    {
        Label usernameLabel = new Label() {
            Text = "Gebruikersnaam:",
        };


        TextField usernameText = new TextField ("") {
            X = Pos.Right(usernameLabel) + 1,
            Width = Dim.Percent(20),
        };

        Label passwordLabel = new Label() {
            Text = "Wachtwoord:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(usernameLabel) + 1,
        };

        TextField passwordText = new TextField("") {
            Secret = true,
            X = Pos.Left(usernameText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Percent(20),
        };

        CheckBox passwordCheckBox = new CheckBox() {
            X = Pos.Right(passwordText) + 1,
            Y = Pos.Top(passwordLabel),
        };

        passwordCheckBox.Toggled += (e) => {if (!e)
            passwordText.Secret = false;
            else passwordText.Secret = true;};


        Button loginButton = new Button() {
            Text = "Log in",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(passwordLabel) + 1,
        };

        loginButton.Clicked += () => {
            if (CheckLogin(((string)usernameText.Text), ((string)passwordText.Text))) {
                if ((string)usernameText.Text == "admin") {
                    WindowManager.currentColor = Colors.TopLevel;
                    WindowManager.SetWindow(this, new AdminMenu((string)usernameText.Text));
                } else {
                    WindowManager.SetWindow(this, new UserMenu((string)usernameText.Text));
                }
            }  else {
				MessageBox.ErrorQuery("Logging In", "Verkeerd gebruikersnaam of wachtwoord.", "Ok");
			}
        };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(loginButton) + 1,
            Y = Pos.Top(loginButton),
        };

        backButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(usernameLabel, usernameText, passwordLabel, passwordText, passwordCheckBox, loginButton, backButton);
    }

    private bool CheckLogin(string username, string password)
    {
        // TODO: Add database check here
        // TODO: Change the return type to a user class instead of bool

        return (username == "admin" && password == "password") ||
                (username == "user" && password == "password");
    }
}