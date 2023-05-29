using System;
using Newtonsoft.Json;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;
using System.Drawing;

namespace Windows;
public class MainBooking : Toplevel
{
    public TextField FirstnameText;
    public TextField PrepositionText;
    public TextField LastnameText;
    public TextField EmailText;
    public DateTimeField DateOfBirthField;
    public ComboBox DialCodesComboBox;
    public TextField PhoneText;
    public ComboBox NationalityComboBox;
    public TextField DocumentNumber;
    public ComboBox DocumentTypeComboBox;
    public DateTimeField ExpireDateField;

    public MainBooking()
    {
        #region Name
        Label firstnameLabel = new Label()
        {
            Text = "Voornaam*:",
        };

        FirstnameText = new TextField("")
        {
            X = Pos.Right(firstnameLabel) + 1,
            Width = 22,
        };

        Label prepositionLabel = new Label()
        {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };
        Label PrepositionLabel = new Label()
        {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };

        PrepositionText = new TextField("")
        {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label()
        {
            Text = "Achternaam:",
            X = Pos.Right(PrepositionText) + 1
        };

        LastnameText = new TextField("")
        {
            X = Pos.Right(lastnameLabel) + 1,
            Width = 22,
        };

        Label attentionLabel = new Label
        {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(LastnameText) + 2,
        };

        #endregion
        Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);

        #region User
        Label emailLabel = new Label()
        {
            Text = "E-mailadres:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        EmailText = new TextField()
        {
            X = Pos.Right(emailLabel) + 8,
            Y = Pos.Top(emailLabel),
            Width = Dim.Percent(20),
        };

        Add(emailLabel, EmailText);
        #endregion


        #region Phonenumber
        StreamReader dialcodesReader = new StreamReader("dial_codes.json");
        string dialcodesFile = dialcodesReader.ReadToEnd();

        Label phoneLabel = new Label()
        {
            Text = "Telefoonnummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(EmailText) + 1,
        };
        DialCodesComboBox = new ComboBox()
        {
            X = Pos.Left(EmailText),
            Y = Pos.Top(phoneLabel),
            Width = 7,
            Height = 4,
        };

        DialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));

        PhoneText = new TextField("")
        {
            X = Pos.Right(DialCodesComboBox) + 1,
            Y = Pos.Top(phoneLabel),
            Width = 39
        };

        PhoneText.TextChanged += (text) =>
        {
            if (!int.TryParse(PhoneText.Text == "" ? "0" : (string)PhoneText.Text, out _))
                PhoneText.Text = text == "" ? "" : text;
            else if (PhoneText.Text.Length > 10)
                PhoneText.Text = text;
            PhoneText.CursorPosition = PhoneText.Text.Length;
        };

        Add(phoneLabel, DialCodesComboBox, PhoneText);
        #endregion

        #region Date of birth
        Label dateOfBirthLabel = new Label()
        {
            Text = "Geboortedatum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(phoneLabel) + 1,
        };

        DateOfBirthField = new DateTimeField(Enumerable.Range(1960, 46).ToList())
        {
            X = Pos.Right(dateOfBirthLabel) + 6,
            Y = Pos.Top(dateOfBirthLabel),
        };

        Add(dateOfBirthLabel, DateOfBirthField);
        #endregion

        #region Nationality
        StreamReader reader = new StreamReader("countries.json");
        string countriesFile = reader.ReadToEnd();

        Label nationalityLabel = new Label()
        {
            Text = "Nationaliteit:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        NationalityComboBox = new ComboBox()
        {
            X = Pos.Right(nationalityLabel) + 6,
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        NationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

        Add(nationalityLabel, NationalityComboBox);
        #endregion

        #region Document Information

        Label documentNumberLabel = new Label()
        {
            Text = "Document nummer:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        DocumentNumber = new TextField("")
        {
            X = Pos.Right(documentNumberLabel) + 4,
            Y = Pos.Top(documentNumberLabel),
            Width = Dim.Percent(20) - 2,
        };

        DocumentNumber.TextChanged += (text) =>
        {
            if (DocumentNumber.Text.Length > 12)
                DocumentNumber.Text = text;
            DocumentNumber.CursorPosition = DocumentNumber.Text.Length;
        };

        Label documentTypeLabel = new Label()
        {
            Text = "Type:",
            X = Pos.Right(DocumentNumber) + 1,
            Y = Pos.Top(documentNumberLabel),
        };

        DocumentTypeComboBox = new ComboBox()
        {
            X = Pos.Right(documentTypeLabel) + 1,
            Y = Pos.Top(DocumentNumber),
            Width = 10,
            Height = 4,
        };
        DocumentTypeComboBox.SetSource(new List<string>() { "Paspoort", "ID" });

        Label expireDateLabel = new Label()
        {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ExpireDateField = new DateTimeField(Enumerable.Range(DateTime.Now.Year, 10).ToList())
        {
            X = Pos.Right(expireDateLabel) + 7,
            Y = Pos.Top(expireDateLabel),
        };

        Add(documentNumberLabel, DocumentNumber, DocumentTypeComboBox, documentTypeLabel);
        Add(expireDateLabel, ExpireDateField);
        #endregion
        if (WindowManager.CurrentUser != null) {
            User user = WindowManager.CurrentUser;
            FirstnameText.Text = user.UserInfo.FirstName;
            PrepositionText.Text = user.UserInfo.Preposition;
            LastnameText.Text = user.UserInfo.LastName;
            EmailText.Text = user.UserInfo.Email.Address;
            DateOfBirthField.SetDateTime(user.UserInfo.DateOfBirth);
            DialCodesComboBox.SelectedItem = DialCodesComboBox.Source.ToList().IndexOf(user.UserInfo.PhoneNumber.Split("|")[0]);
            PhoneText.Text = user.UserInfo.PhoneNumber.Split("|")[1];
            NationalityComboBox.SelectedItem = NationalityComboBox.Source.ToList().IndexOf(user.UserInfo.Nationality);
            DocumentNumber.Text = user.UserInfo.DocumentNumber != null ? user.UserInfo.DocumentNumber : "";
            DocumentTypeComboBox.SelectedItem = user.UserInfo.DocumentType != null ? DocumentTypeComboBox.Source.ToList().IndexOf(user.UserInfo.DocumentType) : 0;
            ExpireDateField.SetDateTime(user.UserInfo.ExpirationDate != null ? (DateTime)user.UserInfo.ExpirationDate : DateTime.Now);
        }
    }
    public UserInfo? CheckUserInfo()
    {
        DateTime dateOfBirth = DateOfBirthField.GetDateTime();
        DateTime expireDate = ExpireDateField.GetDateTime();
        if (FirstnameText.Text == "" || LastnameText.Text == ""||
            EmailText.Text == "" || PhoneText.Text == "" || DialCodesComboBox.Text == "" || NationalityComboBox.Text == "") {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        MailAddress address;
        bool isValid = false;
        try {
            address = new MailAddress((string)EmailText.Text);
            isValid = (address.Address == (string)EmailText.Text);
        } catch (FormatException) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Onjuist email", "Ok");
            return null;
        }
        if (!isValid) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Onjuist email", "Ok");
            return null;
        }

        if (PhoneText.Text.Length < 9) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Onjuist telefoonnummer", "Ok");
            return null;
        }

        DateTime currentDate = DateTime.Today;
        int age =  currentDate.Year - dateOfBirth.Year;
        if (currentDate < dateOfBirth.AddYears(age))
            age--;

        if (age < 18) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Niet oud genoeg", "Ok");
            return null;
        }
        if (DocumentNumber.Text.Length != 9) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Document nummer niet correct ingevuld", "Ok");
            return null;
        }
        if (DocumentTypeComboBox.Text == "") {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate) {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk",$"Uw {DocumentTypeComboBox.Text} is vervallen", "Ok");
            return null;
        }
        string phonenumber = $"{DialCodesComboBox.Text}|{PhoneText.Text}";
        UserInfo userInfo = new UserInfo((string)FirstnameText.Text, (string)PrepositionText.Text, (string)LastnameText.Text, new MailAddress((string)EmailText.Text), phonenumber, dateOfBirth, (string)NationalityComboBox.Text, (string)DocumentNumber.Text, (string)DocumentTypeComboBox.Text, expireDate);
        // userInfo.IBAN = "iets";
        return userInfo;
    }
}

public class BookingProcess : Toplevel
{
    List<UserInfo> userInfos = new List<UserInfo>();
    Toplevel currentWindow;
    int seatsCount;
    int maxSeats;
    Flight currentFlight;
    public BookingProcess(int seats, Flight flight)
    {
        currentFlight = flight;
        maxSeats = seats;
        SeatManager.MaxSeats = maxSeats;
        seatsCount = seats;
        currentWindow = new MainBooking() { ColorScheme = WindowManager.CurrentColor };
        Add(currentWindow);
    
        Button nextButton = new Button() {
            Text = "Volgende",
            Y = 22,
        };
        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(nextButton)
        };
        nextButton.Clicked += () => { ChangeWindow(); };

        goBack.Clicked += () => {WindowManager.GoBackOne(this);};
        Add(nextButton, goBack);
    }

    private void ChangeWindow()
    {
        UserInfo? userInfo = null;
        if (currentWindow.GetType() == typeof(MainBooking))
            userInfo = (currentWindow as MainBooking).CheckUserInfo();
        else if (currentWindow.GetType() == typeof(ExtraBooking))
            userInfo = (currentWindow as ExtraBooking).CheckUserInfo();

        if (userInfo == null)
            return;

        userInfos.Add(userInfo);

        if (seatsCount == 1) {
            // Do something with userInfos
            WindowManager.GoForwardOne(new SeattingPlan(currentFlight));
            return;
        }
        seatsCount--;
        Remove(currentWindow);
        currentWindow = new ExtraBooking(maxSeats - seatsCount) {Height = 20, ColorScheme = WindowManager.CurrentColor};
        Add(currentWindow);
    }
}

public class ExtraBooking : Toplevel
{
    TextField firstnameText;
    TextField prepositionText;
    TextField lastnameText;
    DateTimeField dateOfBirthField;
    ComboBox nationalityComboBox;
    TextField documentNumber;
    ComboBox documentTypeComboBox;
    DateTimeField expireDateField;
    public ExtraBooking(int seatNum)
    {
        #region Name
        Label extraPassenger = new Label() {
            Text = $"Extra Passagier {seatNum}:",
        };

        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
            Y = Pos.Bottom(extraPassenger) + 1
        };

        firstnameText = new TextField("") {
            X = Pos.Right(firstnameLabel) + 1,
            Y = Pos.Top(firstnameLabel),
            Width = 22,
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(firstnameText) + 1,
            Y = Pos.Top(firstnameLabel),
        };

        prepositionText = new TextField("") {
            X = Pos.Right(prepositionLabel) + 1,
            Y = Pos.Top(firstnameLabel),
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(prepositionText) + 1,
            Y = Pos.Top(firstnameLabel),
        };

        lastnameText = new TextField("") {
            X = Pos.Right(lastnameLabel) + 1,
            Y = Pos.Top(firstnameLabel),
            Width = 22,
        };

        Label attentionLabel = new Label {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(lastnameText) + 2,
            Y = Pos.Top(firstnameLabel),
        };

        Add(extraPassenger, firstnameLabel, firstnameText, prepositionLabel, prepositionText, lastnameLabel, lastnameText, attentionLabel);
        #endregion

        #region Date of birth

        Label dateOfBirthLabel = new Label() {
            Text = "Geboortedatum:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        dateOfBirthField = new DateTimeField(Enumerable.Range(1960, 46).ToList()) {
            X = Pos.Right(dateOfBirthLabel) + 10,
            Y = Pos.Top(dateOfBirthLabel),
        };

        Add(dateOfBirthLabel, dateOfBirthField);
        #endregion

        #region Nationality
        StreamReader reader = new StreamReader("countries.json");
        string countriesFile = reader.ReadToEnd();

        Label nationalityLabel = new Label() {
            Text = "Nationaliteit:",
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        nationalityComboBox = new ComboBox() {
            X = Pos.Left(dateOfBirthField),
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

        Add(nationalityLabel, nationalityComboBox);
        #endregion

        #region Document Information
        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Left(dateOfBirthField),
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
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        expireDateField = new DateTimeField(Enumerable.Range(DateTime.Now.Year, 10).ToList()) {
            X = Pos.Left(dateOfBirthField),
            Y = Pos.Top(expireDateLabel),
        };

        Add(documentNumberLabel, documentNumber, documentTypeLabel, documentTypeComboBox);
        Add(expireDateLabel, expireDateField);
        #endregion
    }
    public UserInfo? CheckUserInfo()
    {
        return new UserInfo((string)firstnameText.Text, (string)prepositionText.Text, (string)lastnameText.Text, dateOfBirthField.GetDateTime(), (string)nationalityComboBox.Text, (string)documentNumber.Text, expireDateField.GetDateTime(), (string)documentTypeComboBox.Text);
    }   
}