using System;
using System.Text.RegularExpressions;
using Terminal.Gui;

public class RegisterMenu : Toplevel
{
    public RegisterMenu()
    {
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam:",
        };

        TextField firstnameText = new TextField("") {
            X = Pos.Right(firstnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(firstnameText) + 1
        };

        TextField lastnameText = new TextField("") {
            X = Pos.Right(lastnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Add(firstnameLabel, firstnameText, lastnameLabel, lastnameText);
        #endregion

        #region User
        Label usernameLabel = new Label() {
            Text = "Gebruikersnaam:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        TextField usernameText = new TextField ("") {
            X = Pos.Right(usernameLabel) + 1,
            Y = Pos.Top(usernameLabel),
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


        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(passwordLabel) + 1,
        };

        TextField emailText = new TextField("") {
            X = Pos.Left(passwordText),
            Y = Pos.Top(emailLabel),
            Width = Dim.Percent(20),
        };

        Add(usernameLabel, usernameText, passwordLabel, passwordText, passwordCheckBox, emailLabel, emailText);
        #endregion

        #region Date of birth
        int[] differentDays = {4, 6, 9, 11};
        int increaseDay = 1;

        Label dateOfBirthLabel = new Label() {
            Text = "Geboortedatum:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(emailLabel) + 1,
        };

        ComboBox dayComboBox = new ComboBox(){
            X = Pos.Left(passwordText),
            Y = Pos.Top(dateOfBirthLabel),
            Height = Dim.Fill(2),
            Width = Dim.Percent(5),
        };

        dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());


        ComboBox monthComboBox = new ComboBox(){
            X = Pos.Right(dayComboBox) + 1,
            Y = Pos.Top(dayComboBox),
            Height = Dim.Fill(2),
            Width = Dim.Percent(5),
        };

        monthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

        monthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
            dayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
            } else if (differentDays.Contains(month)) {
                dayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
             }
            else {
                dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
            } dayComboBox.SelectedItem = 0; };

        ComboBox yearComboBox = new ComboBox(){
            X = Pos.Right(monthComboBox) + 1 ,
            Y = Pos.Top(monthComboBox),
            Height = Dim.Fill(2),
            Width = Dim.Percent(5),
        };

        yearComboBox.SetSource(Enumerable.Range(1960, 46).ToList());

        yearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
                increaseDay = 1;
                monthComboBox.SelectedItem = 0;
            }
            else increaseDay = 0;
            };

        yearComboBox.SelectedItem = 0;
        monthComboBox.SelectedItem = 0;
        dayComboBox.SelectedItem = 0;

        Add(dateOfBirthLabel, dayComboBox, monthComboBox, yearComboBox);
        #endregion

        #region Register
        Button registerButton = new Button() {
            Text = "Registreren",
            X = Pos.Left(dateOfBirthLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        registerButton.Clicked += () => {
            int year = Convert.ToInt32(yearComboBox.Text);
            int month = Convert.ToInt32(monthComboBox.Text);
            int day = Convert.ToInt32(dayComboBox.Text);
            string? result = RegisterUser((string)firstnameText.Text, (string)lastnameText.Text, (string)usernameText.Text,
                                        (string)passwordText.Text, (string)emailText.Text, new DateTime(year, month, day));
            if (result != null)
                WindowManager.SetWindow(this, new MainMenu(result)); };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(registerButton) + 1,
            Y = Pos.Top(registerButton),
        };

        backButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(registerButton, backButton);
        #endregion
    }


    private string RegisterUser(string fname, string lname, string uname, string password, string email, DateTime dateOfBirth)
    {
        if (fname == "" || lname == "" || uname == "" || password == "" || email == "")
        {
            MessageBox.ErrorQuery("Registreren", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (!IsValidEmail(email))
        {
            MessageBox.ErrorQuery("Registreren", "Onjuist email", "Ok");
            return null;
        }

        DateTime currentDate = DateTime.Today;
        int age =  currentDate.Year - dateOfBirth.Year;
        if (currentDate < dateOfBirth.AddYears(age))
            age--;

        if (age < 18)
        {
            MessageBox.ErrorQuery("Registreren", "Niet oud genoeg", "Sluiten");
            WindowManager.SetWindow(this, new LoginMenu());
        }

        return uname;
    }
    private bool IsValidEmail(string email)
    {
        string pattern = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        Regex regex = new Regex(pattern);
        Match match = regex.Match(email);
        return match.Success;
    }
}