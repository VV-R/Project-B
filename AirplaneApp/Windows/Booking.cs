using System;
using Newtonsoft.Json;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;
using System.Drawing;

namespace Windows;
public class Booking : Toplevel
{
    public Flight Flight { get; set; }
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

    public Booking(Flight flight)
    {
        var n = MessageBox.Query("Passagiers", "Met hoeveel personen reist u?", "1", "2", "3", "4", "5", "6", "7", "8"); 
        Flight = flight;
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
            if (!int.TryParse(DocumentNumber.Text == "" ? "0" : (string)DocumentNumber.Text, out _))
                DocumentNumber.Text = text == "" ? "" : text;
            else if (DocumentNumber.Text.Length > 9)
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

        Button forwardButton = new Button()
        {
            Text = "Volgende",
            Y = Pos.Bottom(expireDateLabel) + 1
        };

        forwardButton.Clicked += () =>
        {
            try {
                DateTime dateOfBirth = DateOfBirthField.GetDateTime();
                DateTime expireDate = ExpireDateField.GetDateTime();
                User? user = CheckUser(dateOfBirth, expireDate);
                if (user != null) {
                    WindowManager.CurrentUser = user;
                     if (n == 0)
                    {
                        WindowManager.GoForwardOne(new SeattingPlan());
                    }
                    else
                    {
                        WindowManager.GoForwardOne(new BookingProcess(n, flight));
                    }
                }
            } catch (FormatException e) {
                Console.WriteLine(e.Message);
            }
        };

        Button backButton = new Button()
        {
            Text = "Terug",
            X = Pos.Right(forwardButton) + 1,
            Y = Pos.Bottom(expireDateLabel) + 1,
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(forwardButton, backButton);

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
    private User? CheckUser(DateTime dateOfBirth, DateTime expireDate)
    {
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

        if (DocumentTypeComboBox.Text == "" && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk","Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Verder gaan niet mogelijk",$"Uw {DocumentTypeComboBox.Text} is vervallen", "Ok");
            return null;
        }
        string phonenumber = $"{DialCodesComboBox.Text}|{PhoneText.Text}";
        User user = new User(0, new Entities.UserInfo((string)FirstnameText.Text, (string)PrepositionText.Text, (string)LastnameText.Text, new MailAddress((string)EmailText.Text), phonenumber, dateOfBirth, (string)NationalityComboBox.Text, (string)DocumentNumber.Text, (string)DocumentTypeComboBox.Text, expireDate), "");


        if (DocumentNumber.Text != "") {
            user.UserInfo.DocumentNumber = (string)DocumentNumber.Text;
            user.UserInfo.DocumentType = (string)DocumentTypeComboBox.Text;
            user.UserInfo.ExpirationDate = expireDate;
        }
        return user;
    }
}

public class BookingProcess : Toplevel
{
    Toplevel currentWindow;
    public BookingProcess(int seatsCount, Flight flight)
    {
        currentWindow = new ExtraBooking(seatsCount);
        Add(currentWindow);
    
        Button nextButton = new Button() {
            Text = "Volgende",
            Y = 22,
        };
        Button goBack = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(nextButton)
        };
        nextButton.Clicked += () => {if (seatsCount == 1) {WindowManager.GoForwardOne(new SeattingPlan(flight)); return; } seatsCount--; ChangeUserInfo(seatsCount);/* Get info check if not null... dan ga je verder*/ Remove(currentWindow); currentWindow = new ExtraBooking(seatsCount) {Height = 20, ColorScheme = WindowManager.CurrentColor}; Add(currentWindow);};

        goBack.Clicked += () => {WindowManager.GoBackOne(this);};
        Add(nextButton, goBack);
    }

    public void ChangeUserInfo(int seats)
    {
            ExtraBooking? extraBooking = currentWindow as ExtraBooking;
            UserInfo? userInfo = extraBooking.GetUserInfo();
            if (userInfo != null)
            {
                DateTime currentDate = DateTime.Today;
                if (userInfo.FirstName == "" || userInfo.LastName == ""||
                    userInfo.Nationality == "") {
                    MessageBox.ErrorQuery("Verder gaan niet mogelijk","Sommige velden zijn niet ingevuld.", "Ok");
                }
                 if (userInfo.DocumentType == "" && userInfo.DocumentNumber != "") {
                    MessageBox.ErrorQuery("Verder gaan niet mogelijk","Document type niet ingevuld", "Ok");
                } if (currentDate > userInfo.ExpirationDate && userInfo.DocumentNumber != "") {
                    MessageBox.ErrorQuery("Verder gaan niet mogelijk",$"Uw {userInfo.DocumentType} is vervallen", "Ok");
                }
            }

    }
}

public class ExtraBooking : Toplevel
{
    TextField firstnameText;
    TextField prepositionText;
    TextField lastnameText;
    Label? dateOfBirthLabel;
    DateTime dateOfBirthField;
    ComboBox nationalityComboBox;
    TextField documentNumber;
    ComboBox documentTypeComboBox;
    public Label? ExpireDateLabel;
    public ComboBox? exipreDayComboBox;
    public ComboBox? expireMonthComboBox;
    public ComboBox? expireYearComboBox;

    public ExtraBooking(int stoelen)
    {
        #region Name
        Label extraPassenger = new Label() {
            Text = $"Extra Passagier {stoelen}:",
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

         DateTimeField dateofBirthField = new DateTimeField(Enumerable.Range(1960, 46).ToList()) {
            X = Pos.Right(dateOfBirthLabel) + 10,
            Y = Pos.Top(dateOfBirthLabel),
        };

        Add(dateOfBirthLabel, dateofBirthField);
        #endregion

        #region Nationality
        StreamReader reader = new StreamReader("countries.json");
        string countriesFile = reader.ReadToEnd();

        Label nationalityLabel = new Label() {
            Text = "Nationaliteit:",
            Y = Pos.Bottom(dateOfBirthLabel) + 1,
        };

        nationalityComboBox = new ComboBox() {
            X = Pos.Left(dateofBirthField),
            Y = Pos.Top(nationalityLabel),
            Width = 47,
            Height = 8,
        };

        nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

        Add(nationalityLabel, nationalityComboBox);
        #endregion

        #region Document Information
        int[] differentDays = {4, 6, 9, 11};
        int increaseDay = 1;

        Label documentNumberLabel = new Label() {
            Text = "Document nummer:",
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Left(dateofBirthField),
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

        ComboBox exipreDayComboBox = new ComboBox(){
            X = Pos.Left(dateofBirthField),
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
    }
    public UserInfo? GetUserInfo()
    {
        User? user = WindowManager.CurrentUser;
        user.UserInfo.FirstName = (string)firstnameText.Text;
        user.UserInfo.Preposition = (string)prepositionText.Text;
        user.UserInfo.LastName = (string)lastnameText.Text;
        user.UserInfo.DateOfBirth = (DateTime)dateOfBirthField;
        user.UserInfo.Nationality = (string)nationalityComboBox.Text;
        user.UserInfo.DocumentNumber = (string)documentNumber.Text;
        user.UserInfo.DocumentType = (string)documentTypeComboBox.Text;
        return user.UserInfo;
    }   
}


//  private UserInfo? CheckUser(DateTime dateOfBirth, DateTime expireDate)
//     {
//         DateTime currentDate = DateTime.Today;
        // if (firstnameText.Text == "" || lastnameText.Text == ""||
        //     nationalityComboBox.Text == "") {
        //     MessageBox.ErrorQuery("Verder gaan niet mogelijk","Sommige velden zijn niet ingevuld.", "Ok");
        //     return null;
        // }
//         if (documentTypeComboBox.Text == "" && documentNumber.Text != "") {
//             MessageBox.ErrorQuery("Verder gaan niet mogelijk","Document type niet ingevuld", "Ok");
//             return null;
//         } else if (currentDate > expireDate && documentNumber.Text != "") {
//             MessageBox.ErrorQuery("Verder gaan niet mogelijk",$"Uw {documentTypeComboBox.Text} is vervallen", "Ok");
//             return null;
//         }
//         return null;
//     }

   