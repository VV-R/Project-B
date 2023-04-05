using System;
using System.Net.Mail;
using Newtonsoft.Json;
using Terminal.Gui;

public class UserInfo : Toplevel
{
    public TextField FirstnameText;
    public TextField PrepositionText;
    public TextField LastnameText;
    public TextField EmailText;
    public ComboBox DialCodesComboBox;
    public TextField PhoneText;
    public ComboBox NationalityComboBox;
    public TextField DocumentNumber;
    public ComboBox DocumentTypeComboBox;
    public Label ExpireDateLabel;

    public UserInfo()
    {
        User user = WindowManager.CurrentUser;
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
        };

        FirstnameText = new TextField(user.FirstName) {
            X = Pos.Right(firstnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };

        PrepositionText = new TextField(user.Preposition) {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(PrepositionText) + 1
        };

        LastnameText = new TextField(user.LastName) {
            X = Pos.Right(lastnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label attentionLabel = new Label {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(LastnameText) + 2,
        };

        Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);
        #endregion

        #region User
        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        EmailText = new TextField(user.Email.Address) {
            X = Pos.Right(emailLabel) + 8,
            Y = Pos.Top(emailLabel),
            Width = Dim.Percent(20),
        };

        Add(emailLabel, EmailText);
        #endregion

        #region Phonenumber
        StreamReader dialcodesReader = new StreamReader("dial_codes.json");
        string dialcodesFile = dialcodesReader.ReadToEnd();

        Label phoneLabel = new Label() {
            Text = "Telefoonnummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1,
        };
        DialCodesComboBox = new ComboBox() {
            X = Pos.Left(EmailText),
            Y = Pos.Top(phoneLabel),
            Width = 7,
            Height = 4,
        };

        DialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));
        DialCodesComboBox.SelectedItem = DialCodesComboBox.Source.ToList().IndexOf(user.PhoneNumber.Split("|")[0]);

        PhoneText = new TextField(user.PhoneNumber.Split("|")[1]) {
            X = Pos.Right(DialCodesComboBox) + 1,
            Y = Pos.Top(phoneLabel),
            Width = 39
        };

        PhoneText.TextChanged += (text) => {
        if (!int.TryParse(PhoneText.Text == "" ? "0" : (string)PhoneText.Text, out _))
            PhoneText.Text = text == "" ? "" : text;
        else if (PhoneText.Text.Length > 10)
            PhoneText.Text = text;
        PhoneText.CursorPosition = PhoneText.Text.Length;};

        Add(phoneLabel, DialCodesComboBox, PhoneText);
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
            X = Pos.Left(EmailText),
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

        yearComboBox.SelectedItem = user.DateOfBirth.Year - 1960;
        monthComboBox.SelectedItem = user.DateOfBirth.Month - 1;
        dayComboBox.SelectedItem = user.DateOfBirth.Day - 1;

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

        NationalityComboBox = new ComboBox() {
            X = Pos.Left(EmailText),
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        NationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));
        NationalityComboBox.SelectedItem = NationalityComboBox.Source.ToList().IndexOf(user.Nationality);

        Add(nationalityLabel, NationalityComboBox);
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

        DocumentNumber = new TextField(user.DocumentNumber != null ? user.DocumentNumber : "") {
            X = Pos.Left(EmailText) + 2,
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20) - 2,
        };

        DocumentNumber.TextChanged += (text) => {
        if (!int.TryParse(DocumentNumber.Text == "" ? "0" : (string)DocumentNumber.Text, out _))
            DocumentNumber.Text = text == "" ? "" : text;
        else if (DocumentNumber.Text.Length > 9)
            DocumentNumber.Text = text;
        DocumentNumber.CursorPosition = DocumentNumber.Text.Length;};

        Label documentTypeLabel = new Label() {
            Text = "Type:",
            X = Pos.Right(DocumentNumber) + 1,
            Y = Pos.Top(documentNumberLabel),
        };

        DocumentTypeComboBox = new ComboBox() {
            X = Pos.Right(documentTypeLabel) + 1,
            Y = Pos.Top(documentNumberLabel),
            Width = 10,
            Height = 4,
        };
        DocumentTypeComboBox.SetSource(new List<string>() {"Paspoort", "ID"});

        DocumentTypeComboBox.SelectedItem = user.DocumentType != null ? DocumentTypeComboBox.Source.ToList().IndexOf(user.DocumentType) : 0;

        ExpireDateLabel = new Label() {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel) + 2,
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ComboBox exipreDayComboBox = new ComboBox(){
            X = Pos.Left(EmailText) + 2,
            Y = Pos.Top(ExpireDateLabel),
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

        if (user.DocumentNumber != null) {
            expireYearComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Year) - 1;
            expireMonthComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Month) - 1;
            exipreDayComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Day) - 1;
        }


        Add(documentNumberLabel, DocumentNumber, documentTypeLabel, DocumentTypeComboBox);
        Add(ExpireDateLabel, exipreDayComboBox, expireMonthComboBox, expireYearComboBox);
        #endregion
    }
    public UserInfo(User user)
    {
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
        };

        FirstnameText = new TextField(user.FirstName) {
            X = Pos.Right(firstnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };

        PrepositionText = new TextField(user.Preposition) {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(PrepositionText) + 1
        };

        LastnameText = new TextField(user.LastName) {
            X = Pos.Right(lastnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label attentionLabel = new Label {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(LastnameText) + 2,
        };

        Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);
        #endregion

        #region User
        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        EmailText = new TextField(user.Email.Address) {
            X = Pos.Right(emailLabel) + 8,
            Y = Pos.Top(emailLabel),
            Width = Dim.Percent(20),
        };

        Add(emailLabel, EmailText);
        #endregion

        #region Phonenumber
        StreamReader dialcodesReader = new StreamReader("dial_codes.json");
        string dialcodesFile = dialcodesReader.ReadToEnd();

        Label phoneLabel = new Label() {
            Text = "Telefoonnummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(emailLabel) + 1,
        };
        DialCodesComboBox = new ComboBox() {
            X = Pos.Left(EmailText),
            Y = Pos.Top(phoneLabel),
            Width = 7,
            Height = 4,
        };

        DialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));
        DialCodesComboBox.SelectedItem = DialCodesComboBox.Source.ToList().IndexOf(user.PhoneNumber.Split("|")[0]);

        PhoneText = new TextField(user.PhoneNumber.Split("|")[1]) {
            X = Pos.Right(DialCodesComboBox) + 1,
            Y = Pos.Top(phoneLabel),
            Width = 39
        };

        PhoneText.TextChanged += (text) => {
        if (!int.TryParse(PhoneText.Text == "" ? "0" : (string)PhoneText.Text, out _))
            PhoneText.Text = text == "" ? "" : text;
        else if (PhoneText.Text.Length > 10)
            PhoneText.Text = text;
        PhoneText.CursorPosition = PhoneText.Text.Length;};

        Add(phoneLabel, DialCodesComboBox, PhoneText);
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
            X = Pos.Left(EmailText),
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

        yearComboBox.SelectedItem = user.DateOfBirth.Year - 1960;
        monthComboBox.SelectedItem = user.DateOfBirth.Month - 1;
        dayComboBox.SelectedItem = user.DateOfBirth.Day - 1;

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

        NationalityComboBox = new ComboBox() {
            X = Pos.Left(EmailText),
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        NationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));
        NationalityComboBox.SelectedItem = NationalityComboBox.Source.ToList().IndexOf(user.Nationality);

        Add(nationalityLabel, NationalityComboBox);
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

        DocumentNumber = new TextField(user.DocumentNumber != null ? user.DocumentNumber : "") {
            X = Pos.Left(EmailText) + 2,
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20) - 2,
        };

        DocumentNumber.TextChanged += (text) => {
        if (!int.TryParse(DocumentNumber.Text == "" ? "0" : (string)DocumentNumber.Text, out _))
            DocumentNumber.Text = text == "" ? "" : text;
        else if (DocumentNumber.Text.Length > 9)
            DocumentNumber.Text = text;
        DocumentNumber.CursorPosition = DocumentNumber.Text.Length;};

        Label documentTypeLabel = new Label() {
            Text = "Type:",
            X = Pos.Right(DocumentNumber) + 1,
            Y = Pos.Top(documentNumberLabel),
        };

        DocumentTypeComboBox = new ComboBox() {
            X = Pos.Right(documentTypeLabel) + 1,
            Y = Pos.Top(documentNumberLabel),
            Width = 10,
            Height = 4,
        };
        DocumentTypeComboBox.SetSource(new List<string>() {"Paspoort", "ID"});

        DocumentTypeComboBox.SelectedItem = user.DocumentType != null ? DocumentTypeComboBox.Source.ToList().IndexOf(user.DocumentType) : 0;

        ExpireDateLabel = new Label() {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel) + 2,
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ComboBox exipreDayComboBox = new ComboBox(){
            X = Pos.Left(EmailText) + 2,
            Y = Pos.Top(ExpireDateLabel),
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

        if (user.DocumentNumber != null) {
            expireYearComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Year) - 1;
            expireMonthComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Month) - 1;
            exipreDayComboBox.SelectedItem = Convert.ToInt32(user.ExpirationDate?.Day) - 1;
        }


        Add(documentNumberLabel, DocumentNumber, documentTypeLabel, DocumentTypeComboBox);
        Add(ExpireDateLabel, exipreDayComboBox, expireMonthComboBox, expireYearComboBox);
        #endregion
    }
}

public class EditUserInfo : UserInfo
{
    public EditUserInfo()
    {
        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(ExpireDateLabel) + 1,
        };

        Button exitButton = new Button() {
            Text = "Annuleren",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };

        exitButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(editButton, exitButton);
    }
}


public class EditUserInfoAdmin : UserInfo
{
    public EditUserInfoAdmin(User user) : base(user)
    {
        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(ExpireDateLabel) + 1,
        };

        Button deleteButton = new Button() {
            Text = "Verwijderen",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };

        Button exitButton = new Button() {
            Text = "Annuleren",
            X = Pos.Right(deleteButton) + 1,
            Y = Pos.Top(editButton),
        };

        exitButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(editButton, deleteButton, exitButton);
    }
}