using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using Terminal.Gui;

public class RegisterMenu : Toplevel
{
    TextField firstnameText;
    TextField lastnameText;
    TextField usernameText;
    TextField passwordText;
    TextField emailText;
    TextField documentNumber;
    TextField ibanText;

    public RegisterMenu()
    {
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam:",
        };

        firstnameText = new TextField("") {
            X = Pos.Right(firstnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(firstnameText) + 1
        };

        lastnameText = new TextField("") {
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

        usernameText = new TextField ("") {
            X = Pos.Right(usernameLabel) + 2,
            Y = Pos.Top(usernameLabel),
            Width = Dim.Percent(20),
        };

        Label passwordLabel = new Label() {
            Text = "Wachtwoord:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(usernameLabel) + 1,
        };

        passwordText = new TextField("") {
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

        emailText = new TextField("") {
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

        #region Private Data
        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            X = Pos.Left(dateOfBirthLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Left(passwordText),
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20),
        };

        documentNumber.TextChanged += (text) => {
        if (!int.TryParse(documentNumber.Text == "" ? "0" : (string)documentNumber.Text, out _))
            documentNumber.Text = text == "" ? "" : text;
        else if (documentNumber.Text.Length > 9)
            documentNumber.Text = text;
        documentNumber.CursorPosition = documentNumber.Text.Length;};

        Label ibanLabel = new Label() {
            Text = "IBAN:",
            X = Pos.Left(documentNumberLabel),
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ibanText = new TextField("") {
            X = Pos.Left(passwordText),
            Y = Pos.Top(ibanLabel),
            Width = Dim.Percent(20),
        };

        ibanText.TextChanged += (text) => { int len = ibanText.Text.Split(" ").Last().Length;
        if (len >= 4 && text.Length < ibanText.Text.Length)
            ibanText.InsertText(" ");
        else if (len == 4 && text.Length > ibanText.Text.Length) {
            ibanText.CursorPosition -= 1;
            ibanText.DeleteCharRight();
        }
        if (ibanText.Text.Length > 22) {
            ibanText.Text = text;
        }
        ibanText.CursorPosition = ibanText.Text.Length; };

        Add(documentNumberLabel, documentNumber, ibanLabel, ibanText);
        #endregion

        #region Register
        Button registerButton = new Button() {
            Text = "Registreren",
            X = Pos.Left(ibanLabel),
            Y = Pos.Bottom(ibanLabel) + 1,
        };

        registerButton.Clicked += () => {
            try {
                int year = Convert.ToInt32(yearComboBox.Text);
                int month = Convert.ToInt32(monthComboBox.Text);
                int day = Convert.ToInt32(dayComboBox.Text);
                string? result = RegisterUser(new DateTime(year, month, day));
                if (result != null)
                    WindowManager.SetWindow(this, new MainMenu(result));
            } catch (FormatException e) {
                Console.WriteLine(e.Message);
            }
            };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(registerButton) + 1,
            Y = Pos.Top(registerButton),
        };

        backButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(registerButton, backButton);
        #endregion
    }


    private string? RegisterUser(DateTime dateOfBirth)
    {
        if (firstnameText.Text == "" || lastnameText.Text == "" || usernameText.Text == "" || passwordText.Text == "" ||
            emailText.Text == "" || documentNumber.Text == "" || ibanText.Text == "")
        {
            MessageBox.ErrorQuery("Registreren", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        MailAddress address;
        bool isValid = false;
        try {
            address = new MailAddress((string)emailText.Text);
            isValid = (address.Address == (string)emailText.Text);
        } catch (FormatException) {
            MessageBox.ErrorQuery("Registreren", "Onjuist email", "Ok");
            return null;
        }
        if (!isValid) {
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

        return (string)usernameText.Text;
    }
}