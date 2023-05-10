using System;
using System.Net.Mail;
using Newtonsoft.Json;
using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class RegisterMenu : Toplevel
{
    TextField firstnameText;
    TextField prepositionText;
    TextField lastnameText;
    TextField passwordText;
    TextField passwordRepeat;
    TextField emailText;
    ComboBox dialCodesComboBox;
    TextField phoneText;
    ComboBox nationalityComboBox;
    TextField documentNumber;
    ComboBox documentTypeComboBox;

    public RegisterMenu()
    {
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
        };

        firstnameText = new TextField("") {
            X = Pos.Right(firstnameLabel) + 1,
            Width = 22,
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(firstnameText) + 1
        };

        prepositionText = new TextField("") {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(prepositionText) + 1
        };

        lastnameText = new TextField("") {
            X = Pos.Right(lastnameLabel) + 1,
            Width = 22,
        };

        Label attentionLabel = new Label {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(lastnameText) + 2,
        };

        Add(firstnameLabel, firstnameText, prepositionLabel, prepositionText, lastnameLabel, lastnameText, attentionLabel);
        #endregion

        #region User
        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        emailText = new TextField("") {
            X = Pos.Right(emailLabel) + 8,
            Y = Pos.Top(emailLabel),
            Width = Dim.Percent(20),
        };

        emailText.TextChanged += (text) => {
            if (MailAddress.TryCreate((string)emailText.Text, out _) && emailText.Text.Contains("."))
                emailText.ColorScheme = Colors.ColorSchemes["TextCorrect"];
            else
                emailText.ColorScheme = Colors.ColorSchemes["TextIncorrect"];
        };

        Label passwordLabel = new Label() {
            Text = "Wachtwoord:",
            Y = Pos.Bottom(emailLabel) + 1,
        };

        passwordText = new TextField("") {
            Secret = true,
            X = Pos.Left(emailText),
            Y = Pos.Top(passwordLabel),
            Width = Dim.Percent(20),
        };

        Label passwordCheckBox = new Label("show") {
            X = Pos.Right(passwordText) + 1,
            Y = Pos.Top(passwordLabel),
        };

        passwordCheckBox.Clicked += () => {
            passwordText.Secret = !passwordText.Secret;
            passwordCheckBox.Text = passwordText.Secret ? "show" : "hide";
        };

        Label passwordRepeatLabel = new Label() {
            Text = "Herhaal-Wachtwoord:",
            Y = Pos.Bottom(passwordLabel) + 1,
        };

        passwordRepeat = new TextField("") {
            Secret = true,
            X = Pos.Left(passwordText),
            Y = Pos.Bottom(passwordLabel) + 1,
            Width = Dim.Percent(20),
        };

        passwordText.TextChanged += (text) => {
            if (passwordText.Text.Length > 8)
                passwordText.ColorScheme = Colors.ColorSchemes["TextCorrect"];
            else passwordText.ColorScheme = Colors.ColorSchemes["TextIncorrect"];
        };

        passwordRepeat.TextChanged += (text) => {
            if (passwordRepeat.Text == passwordText.Text)
                passwordRepeat.ColorScheme = Colors.ColorSchemes["TextCorrect"];
            else passwordRepeat.ColorScheme = Colors.ColorSchemes["TextIncorrect"];
        };

        Label passwordRepeatCheckBox = new Label("show") {
            X = Pos.Right(passwordRepeat) + 1,
            Y = Pos.Top(passwordRepeatLabel),
        };

        passwordRepeatCheckBox.Clicked += () => {
            passwordRepeat.Secret = !passwordRepeat.Secret;
            passwordRepeatCheckBox.Text = passwordRepeat.Secret ? "show" : "hide";
        };

        Add(emailLabel, emailText, passwordLabel, passwordText, passwordCheckBox, passwordRepeat, passwordRepeatLabel, passwordRepeatCheckBox);
        #endregion

        #region Phonenumber
        StreamReader dialcodesReader = new StreamReader("dial_codes.json");
        string dialcodesFile = dialcodesReader.ReadToEnd();

        Label phoneLabel = new Label() {
            Text = "Telefoonnummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(passwordRepeatLabel) + 1,
        };
        dialCodesComboBox = new ComboBox() {
            X = Pos.Left(passwordText),
            Y = Pos.Top(phoneLabel),
            Width = 7,
            Height = 4,
        };

        dialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));

        phoneText = new TextField("") {
            X = Pos.Right(dialCodesComboBox) + 1,
            Y = Pos.Top(phoneLabel),
            Width = 39
        };

        phoneText.TextChanged += (text) => {
        if (!int.TryParse(phoneText.Text == "" ? "0" : (string)phoneText.Text, out _))
            phoneText.Text = text == "" ? "" : text;
        else if (phoneText.Text.Length > 10)
            phoneText.Text = text;
        phoneText.CursorPosition = phoneText.Text.Length;};

        phoneText.TextChanged += (text) => {
            if (phoneText.Text.Length > 9)
                phoneText.ColorScheme = Colors.ColorSchemes["TextCorrect"];
            else phoneText.ColorScheme = Colors.ColorSchemes["TextIncorrect"];
        };

        Add(phoneLabel, dialCodesComboBox, phoneText);
        #endregion

        #region Date of birth
        int[] differentDays = {4, 6, 9, 11};
        int increaseDay = 1;

        Label dateOfBirthLabel = new Label() {
            Text = "Geboortedatum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(phoneLabel) + 1,
        };

        ComboBox dayComboBox = new ComboBox(){
            X = Pos.Left(passwordText),
            Y = Pos.Top(dateOfBirthLabel),
            Height = 4,
            Width = 8,
        };

        dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

        ComboBox monthComboBox = new ComboBox(){
            X = Pos.Right(dayComboBox) + 1,
            Y = Pos.Top(dayComboBox),
            Height = 4,
            Width = 8,
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
            Height = 4,
            Width = 8,
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

        #region Nationality
        StreamReader reader = new StreamReader("countries.json");
        string countriesFile = reader.ReadToEnd();

        Label nationalityLabel = new Label() {
            Text = "Nationaliteit:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        nationalityComboBox = new ComboBox() {
            X = Pos.Left(emailText),
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

        Add(nationalityLabel, nationalityComboBox);
        #endregion

        #region Document Information
        Label optionalLabel = new Label() {
            Text = "Optioneel:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        LineView optionalLine = new LineView() {
            X = Pos.Right(optionalLabel),
            Y = Pos.Top(optionalLabel),
        };

        Add(optionalLabel, optionalLine);

        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            X = Pos.Left(emailLabel) + 2,
            Y = Pos.Bottom(optionalLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Left(passwordText) + 2,
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20) - 2,
        };

        documentNumber.TextChanged += (text) => {
        if (!int.TryParse(documentNumber.Text == "" ? "0" : (string)documentNumber.Text, out _))
            documentNumber.Text = text == "" ? "" : text;
        else if (documentNumber.Text.Length > 9)
            documentNumber.Text = text;
        documentNumber.CursorPosition = documentNumber.Text.Length;};

        Label documentTypeLabel = new Label() {
            Text = "Type:",
            X = Pos.Right(documentNumber) + 1,
            Y = Pos.Top(documentNumberLabel),
        };

        documentTypeComboBox = new ComboBox() {
            X = Pos.Right(documentTypeLabel) + 1,
            Y = Pos.Top(documentNumberLabel),
            Width = 10,
            Height = 4,
        };
        documentTypeComboBox.SetSource(new List<string>() {"Paspoort", "ID"});

        Label expireDateLabel = new Label() {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel) + 2,
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ComboBox exipreDayComboBox = new ComboBox(){
            X = Pos.Left(passwordText) + 2,
            Y = Pos.Top(expireDateLabel),
            Height = 4,
            Width = 8,
        };

        exipreDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

        ComboBox expireMonthComboBox = new ComboBox(){
            X = Pos.Right(exipreDayComboBox) + 1,
            Y = Pos.Top(exipreDayComboBox),
            Height = 4,
            Width = 8,
        };

        expireMonthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

        expireMonthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
            exipreDayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
            } else if (differentDays.Contains(month)) {
                exipreDayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
             }
            else {
                exipreDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
            } exipreDayComboBox.SelectedItem = 0; };

        ComboBox expireYearComboBox = new ComboBox(){
            X = Pos.Right(expireMonthComboBox) + 1 ,
            Y = Pos.Top(expireMonthComboBox),
            Height = 4,
            Width = 8,
        };

        expireYearComboBox.SetSource(Enumerable.Range(DateTime.Now.Year, 10).ToList());

        expireYearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
            if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
                increaseDay = 1;
                expireMonthComboBox.SelectedItem = 0;
            }
            else increaseDay = 0;
            };

        expireYearComboBox.SelectedItem = 0;
        expireMonthComboBox.SelectedItem = 0;
        exipreDayComboBox.SelectedItem = 0;

        Add(documentNumberLabel, documentNumber, documentTypeLabel, documentTypeComboBox);
        Add(expireDateLabel, exipreDayComboBox, expireMonthComboBox, expireYearComboBox);
        #endregion

        #region Register
        Button registerButton = new Button() {
            Text = "Registreren",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(expireDateLabel) + 1,
        };

        registerButton.Clicked += () => {
            try {
                DateTime dateOfBirth = new DateTime(Convert.ToInt32(yearComboBox.Text), Convert.ToInt32(monthComboBox.Text), Convert.ToInt32(dayComboBox.Text));
                DateTime expireDate = new DateTime(Convert.ToInt32(expireYearComboBox.Text), Convert.ToInt32(expireMonthComboBox.Text), Convert.ToInt32(exipreDayComboBox.Text));
                User? user = RegisterUser(dateOfBirth, expireDate);
                if (user != null) {
                    WindowManager.CurrentUser = user;
                    MainWindow.LoginButton.Text = "Uitloggen";
                    WindowManager.GoForwardOne(new UserMenu(user));
                }
            } catch (FormatException e) {
                Console.WriteLine(e.Message);
            }
            };

        Button backButton = new Button() {
            Text = "Terug",
            X = Pos.Right(registerButton) + 1,
            Y = Pos.Top(registerButton),
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(registerButton, backButton);
        #endregion
    }

    private User? RegisterUser(DateTime dateOfBirth, DateTime expireDate)
    {
        if (firstnameText.Text == "" || lastnameText.Text == "" || passwordText.Text == "" || passwordRepeat.Text == "" ||
            emailText.Text == "" || phoneText.Text == "" || dialCodesComboBox.Text == "" || nationalityComboBox.Text == "") {
            MessageBox.ErrorQuery("Registreren", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        if (passwordText.Text != passwordRepeat.Text) {
            MessageBox.ErrorQuery("Registreren", "Wachtwoorden komt niet overheen.", "Ok");
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

        if (phoneText.Text.Length < 9) {
            MessageBox.ErrorQuery("Registreren", "Onjuist telefoonnummer", "Ok");
            return null;
        }

        DateTime currentDate = DateTime.Today;
        int age =  currentDate.Year - dateOfBirth.Year;
        if (currentDate < dateOfBirth.AddYears(age))
            age--;

        if (age < 18) {
            MessageBox.ErrorQuery("Registreren", "Niet oud genoeg", "Ok");
            return null;
        }

        if (documentTypeComboBox.Text == "" && documentNumber.Text != "") {
            MessageBox.ErrorQuery("Registreren", "Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate && documentNumber.Text != "") {
            MessageBox.ErrorQuery("Registreren", $"Uw {documentTypeComboBox.Text} is vervallen", "Ok");
            return null;
        }
        string phonenumber = $"{dialCodesComboBox.Text}|{phoneText.Text}";
        User user = new User(0, (string)firstnameText.Text, (string)prepositionText.Text, (string)lastnameText.Text, (string)passwordText.Text,
                            address, phonenumber, dateOfBirth, (string)nationalityComboBox.Text);

        if (documentNumber.Text != "") {
            user.DocumentNumber = (string)documentNumber.Text;
            user.DocumentType = (string)documentTypeComboBox.Text;
            user.ExpirationDate = expireDate;
        }

        return user;
    }
}