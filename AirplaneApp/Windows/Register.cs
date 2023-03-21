using System;
using System.Net.Mail;
using Newtonsoft.Json;
using Terminal.Gui;

public class RegisterMenu : Toplevel
{
    TextField firstnameText;
    TextField lastnameText;
    TextField usernameText;
    TextField passwordText;
    TextField emailText;
    TextField documentNumber;
    ComboBox documentTypeComboBox;
    ComboBox nationalityComboBox;

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
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        nationalityComboBox = new ComboBox() {
            X = Pos.Left(usernameText),
            Y = Pos.Top(nationalityLabel),
            Width = 46,
            Height = 8,
        };

        nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

        Add(nationalityLabel, nationalityComboBox);
        #endregion

        #region Document Information
        Label optionalLabel = new Label() {
            Text = "Optioneel:",
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        LineView optionalLine = new LineView() {
            X = Pos.Right(optionalLabel),
            Y = Pos.Top(optionalLabel),
        };

        Add(optionalLabel, optionalLine);

        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            X = Pos.Left(usernameLabel) + 2,
            Y = Pos.Bottom(optionalLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Left(passwordText) + 2,
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20),
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
            X = Pos.Left(usernameLabel) + 2,
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
            X = Pos.Left(usernameLabel),
            Y = Pos.Bottom(expireDateLabel) + 1,
        };

        registerButton.Clicked += () => {
            try {
                DateTime dateOfBirth = new DateTime(Convert.ToInt32(yearComboBox.Text), Convert.ToInt32(monthComboBox.Text), Convert.ToInt32(dayComboBox.Text));
                DateTime expireDate = new DateTime(Convert.ToInt32(expireYearComboBox.Text), Convert.ToInt32(expireMonthComboBox.Text), Convert.ToInt32(exipreDayComboBox.Text));
                string? result = RegisterUser(dateOfBirth, expireDate);
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

    private string? RegisterUser(DateTime dateOfBirth, DateTime expireDate)
    {
        if (firstnameText.Text == "" || lastnameText.Text == "" || usernameText.Text == "" ||
            passwordText.Text == "" || emailText.Text == "" || nationalityComboBox.Text == "")
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

        if (documentTypeComboBox.Text == "" && documentNumber.Text != "") {
            MessageBox.ErrorQuery("Registreren", "Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate && documentNumber.Text != "") {
            MessageBox.ErrorQuery("Registreren", $"{documentTypeComboBox.Text} vervallen", "Ok");
            return null;
        }

        if (age < 18)
        {
            MessageBox.ErrorQuery("Registreren", "Niet oud genoeg", "Sluiten");
            return null;
        }

        return (string)usernameText.Text;
    }
}