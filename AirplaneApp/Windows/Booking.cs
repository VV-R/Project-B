using System;
using Newtonsoft.Json;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class Booking : Toplevel
{
    public User User { get; set; }
    public Flight Flight { get; set; }
    public Seat[] SeatNumber { get; set; }
    private User user;
    public TextField FirstnameText;
    public TextField PrepositionText;
    public TextField LastnameText;
    public TextField EmailText;
    public ComboBox dayComboBox;
    public ComboBox monthComboBox;
    public ComboBox yearComboBox;
    public ComboBox DialCodesComboBox;
    public TextField PhoneText;
    public ComboBox NationalityComboBox;
    public TextField DocumentNumber;
    public ComboBox DocumentTypeComboBox;
    public Label ExpireDateLabel;
    public ComboBox exipreDayComboBox;
    public ComboBox expireMonthComboBox;
    public ComboBox expireYearComboBox;

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

    public Booking(Flight flight, Seat[] seatNumber)
    {
        WindowManager.CurrentUser = user;
        Flight = flight;
        SeatNumber = seatNumber;
        if (user != null)
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

            dayComboBox = new ComboBox(){
                X = Pos.Left(EmailText),
                Y = Pos.Top(dateOfBirthLabel),
                Height = 4,
                Width = 8,
            };

            dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

            monthComboBox = new ComboBox(){
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

            yearComboBox = new ComboBox(){
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

            exipreDayComboBox = new ComboBox(){
                X = Pos.Left(EmailText) + 2,
                Y = Pos.Top(ExpireDateLabel),
                Height = 4,
                Width = 8,
            };

            exipreDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

            expireMonthComboBox = new ComboBox(){
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

            expireYearComboBox = new ComboBox(){
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

            Button flightScheduleButton = new Button() {
                Text = "Vlucht Schemas",
                Y = Pos.Bottom(ExpireDateLabel) + 1,
            };
            flightScheduleButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanel(WindowManager.Flights)); };

            Button backButton = new Button() {
                Text = "Terug",
                X = Pos.Right(flightScheduleButton),
                Y = Pos.Top(flightScheduleButton)
            };

            backButton.Clicked += () => { WindowManager.GoBackOne(this); };

            Add(flightScheduleButton, backButton);
        }
        else
        {
            #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
        };

        FirstnameText = new TextField("") {
            X = Pos.Right(firstnameLabel) + 1,
            Width = 22,
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };
        Label PrepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };

        PrepositionText = new TextField("") {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(PrepositionText) + 1
        };

        LastnameText = new TextField("") {
            X = Pos.Right(lastnameLabel) + 1,
            Width = 22,
        };

        Label attentionLabel = new Label {
            Text = "*voornaam zoals op het paspoort",
            X = Pos.Right(LastnameText) + 2,
        };

        #endregion
        Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);

        #region User
        Label emailLabel = new Label() {
            Text = "E-mailadres:",
            Y = Pos.Bottom(firstnameLabel) + 1,
        };

        EmailText = new TextField() {
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
            Y = Pos.Bottom(EmailText) + 1,
        };
        dialCodesComboBox = new ComboBox() {
            X = Pos.Left(EmailText),
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
            X = Pos.Left(phoneText),
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
            X = Pos.Right(nationalityLabel) + 7,
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
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(nationalityLabel) + 1,
        };

        documentNumber = new TextField("") {
            X = Pos.Right(documentNumberLabel) + 3,
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
            Y = Pos.Top(documentNumber),
            Width = 10,
            Height = 4,
        };
        documentTypeComboBox.SetSource(new List<string>() {"Paspoort", "ID"});

        Label expireDateLabel = new Label() {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ComboBox exipreDayComboBox = new ComboBox(){
            X = Pos.Right(expireDateLabel) + 2,
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

        Add(documentNumberLabel, documentNumber, documentTypeComboBox, documentTypeLabel);
        Add(expireDateLabel, exipreDayComboBox, expireMonthComboBox, expireYearComboBox);
        #endregion
        
        // Button flightScheduleButton = new Button() {
        //     Text = "Vlucht Schemas",
        //     Y = Pos.Bottom(ExpireDateLabel) + 1,
        // };
        // flightScheduleButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanel(WindowManager.Flights)); };

        Button backButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(exipreDayComboBox),
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(backButton);
        }
    }
    
//     public Booking(Flight flight, Seat[] seatNumber)
//     {
//         Flight = flight;
//         SeatNumber = seatNumber;
//         #region Name
//         Label firstnameLabel = new Label() {
//             Text = "Voornaam*:",
//         };

//         FirstnameText = new TextField("") {
//             X = Pos.Right(firstnameLabel) + 1,
//             Width = 22,
//         };

//         Label prepositionLabel = new Label() {
//             Text = "Tussenvoegsel:",
//             X = Pos.Right(FirstnameText) + 1
//         };
//         Label PrepositionLabel = new Label() {
//             Text = "Tussenvoegsel:",
//             X = Pos.Right(FirstnameText) + 1
//         };

//         PrepositionText = new TextField("") {
//             X = Pos.Right(prepositionLabel) + 1,
//             Width = 10,
//         };

//         Label lastnameLabel = new Label() {
//             Text = "Achternaam:",
//             X = Pos.Right(PrepositionText) + 1
//         };

//         LastnameText = new TextField("") {
//             X = Pos.Right(lastnameLabel) + 1,
//             Width = 22,
//         };

//         Label attentionLabel = new Label {
//             Text = "*voornaam zoals op het paspoort",
//             X = Pos.Right(LastnameText) + 2,
//         };

//         #endregion
//         Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);

//         #region User
//         Label emailLabel = new Label() {
//             Text = "E-mailadres:",
//             Y = Pos.Bottom(firstnameLabel) + 1,
//         };

//         EmailText = new TextField() {
//             X = Pos.Right(emailLabel) + 8,
//             Y = Pos.Top(emailLabel),
//             Width = Dim.Percent(20),
//         };

//         Add(emailLabel, EmailText);
//         #endregion


//         #region Phonenumber
//         StreamReader dialcodesReader = new StreamReader("dial_codes.json");
//         string dialcodesFile = dialcodesReader.ReadToEnd();

//         Label phoneLabel = new Label() {
//             Text = "Telefoonnummer:",
//             X = Pos.Left(emailLabel),
//             Y = Pos.Bottom(EmailText) + 1,
//         };
//         dialCodesComboBox = new ComboBox() {
//             X = Pos.Left(EmailText),
//             Y = Pos.Top(phoneLabel),
//             Width = 7,
//             Height = 4,
//         };

//         dialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));

//         phoneText = new TextField("") {
//             X = Pos.Right(dialCodesComboBox) + 1,
//             Y = Pos.Top(phoneLabel),
//             Width = 39
//         };

//         phoneText.TextChanged += (text) => {
//         if (!int.TryParse(phoneText.Text == "" ? "0" : (string)phoneText.Text, out _))
//             phoneText.Text = text == "" ? "" : text;
//         else if (phoneText.Text.Length > 10)
//             phoneText.Text = text;
//         phoneText.CursorPosition = phoneText.Text.Length;};

//         Add(phoneLabel, dialCodesComboBox, phoneText);
//         #endregion

//         #region Date of birth
//         int[] differentDays = {4, 6, 9, 11};
//         int increaseDay = 1;

//         Label dateOfBirthLabel = new Label() {
//             Text = "Geboortedatum:",
//             X = Pos.Left(emailLabel),
//             Y = Pos.Bottom(phoneLabel) + 1,
//         };

//         ComboBox dayComboBox = new ComboBox(){
//             X = Pos.Left(phoneText),
//             Y = Pos.Top(dateOfBirthLabel),
//             Height = 4,
//             Width = 8,
//         };

//         dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

//         ComboBox monthComboBox = new ComboBox(){
//             X = Pos.Right(dayComboBox) + 1,
//             Y = Pos.Top(dayComboBox),
//             Height = 4,
//             Width = 8,
//         };

//         monthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

//         monthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
//             dayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
//             } else if (differentDays.Contains(month)) {
//                 dayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
//              }
//             else {
//                 dayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
//             } dayComboBox.SelectedItem = 0; };

//         ComboBox yearComboBox = new ComboBox(){
//             X = Pos.Right(monthComboBox) + 1 ,
//             Y = Pos.Top(monthComboBox),
//             Height = 4,
//             Width = 8,
//         };

//         yearComboBox.SetSource(Enumerable.Range(1960, 46).ToList());

//         yearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
//             if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
//                 increaseDay = 1;
//                 monthComboBox.SelectedItem = 0;
//             }
//             else increaseDay = 0;
//             };

//         yearComboBox.SelectedItem = 0;
//         monthComboBox.SelectedItem = 0;
//         dayComboBox.SelectedItem = 0;

//         Add(dateOfBirthLabel, dayComboBox, monthComboBox, yearComboBox);
//         #endregion

//         #region Nationality
//         StreamReader reader = new StreamReader("countries.json");
//         string countriesFile = reader.ReadToEnd();

//         Label nationalityLabel = new Label() {
//             Text = "Nationaliteit:",
//             X = Pos.Left(emailLabel),
//             Y = Pos.Bottom(dateOfBirthLabel) + 1,
//         };

//         nationalityComboBox = new ComboBox() {
//             X = Pos.Right(nationalityLabel) + 7,
//             Y = Pos.Top(nationalityLabel),
//             Width = 47,
//             Height = 8,
//         };

//         nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

//         Add(nationalityLabel, nationalityComboBox);
//         #endregion

//         #region Document Information
        
//         Label documentNumberLabel = new Label() {
//             Text = "Document nummer:",
//             X = Pos.Left(emailLabel),
//             Y = Pos.Bottom(nationalityLabel) + 1,
//         };

//         documentNumber = new TextField("") {
//             X = Pos.Right(documentNumberLabel) + 3,
//             Y = Pos.Top(documentNumberLabel),
//             Width = Dim.Percent(20) - 2,
//         };

//         documentNumber.TextChanged += (text) => {
//         if (!int.TryParse(documentNumber.Text == "" ? "0" : (string)documentNumber.Text, out _))
//             documentNumber.Text = text == "" ? "" : text;
//         else if (documentNumber.Text.Length > 9)
//             documentNumber.Text = text;
//         documentNumber.CursorPosition = documentNumber.Text.Length;};

//         Label documentTypeLabel = new Label() {
//             Text = "Type:",
//             X = Pos.Right(documentNumber) + 1,
//             Y = Pos.Top(documentNumberLabel),
//         };

//         documentTypeComboBox = new ComboBox() {
//             X = Pos.Right(documentTypeLabel) + 1,
//             Y = Pos.Top(documentNumber),
//             Width = 10,
//             Height = 4,
//         };
//         documentTypeComboBox.SetSource(new List<string>() {"Paspoort", "ID"});

//         Label expireDateLabel = new Label() {
//             Text = "Verval datum:",
//             X = Pos.Left(emailLabel),
//             Y = Pos.Bottom(documentNumberLabel) + 1,
//         };

//         ComboBox exipreDayComboBox = new ComboBox(){
//             X = Pos.Right(expireDateLabel) + 2,
//             Y = Pos.Top(expireDateLabel),
//             Height = 4,
//             Width = 8,
//         };

//         exipreDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());

//         ComboBox expireMonthComboBox = new ComboBox(){
//             X = Pos.Right(exipreDayComboBox) + 1,
//             Y = Pos.Top(exipreDayComboBox),
//             Height = 4,
//             Width = 8,
//         };

//         expireMonthComboBox.SetSource(Enumerable.Range(1, 12).ToList());

//         expireMonthComboBox.SelectedItemChanged += (e) => { int month = Convert.ToInt32(e.Value); if (month == 2) {
//             exipreDayComboBox.SetSource(Enumerable.Range(1, 28 + increaseDay).ToList());
//             } else if (differentDays.Contains(month)) {
//                 exipreDayComboBox.SetSource(Enumerable.Range(1, 30).ToList());
//              }
//             else {
//                 exipreDayComboBox.SetSource(Enumerable.Range(1, 31).ToList());
//             } exipreDayComboBox.SelectedItem = 0; };

//         ComboBox expireYearComboBox = new ComboBox(){
//             X = Pos.Right(expireMonthComboBox) + 1 ,
//             Y = Pos.Top(expireMonthComboBox),
//             Height = 4,
//             Width = 8,
//         };

//         expireYearComboBox.SetSource(Enumerable.Range(DateTime.Now.Year, 10).ToList());

//         expireYearComboBox.SelectedItemChanged += (e) => {int year = Convert.ToInt32(e.Value);
//             if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
//                 increaseDay = 1;
//                 expireMonthComboBox.SelectedItem = 0;
//             }
//             else increaseDay = 0;
//             };

//         expireYearComboBox.SelectedItem = 0;
//         expireMonthComboBox.SelectedItem = 0;
//         exipreDayComboBox.SelectedItem = 0;

//         Add(documentNumberLabel, documentNumber, documentTypeComboBox, documentTypeLabel);
//         Add(expireDateLabel, exipreDayComboBox, expireMonthComboBox, expireYearComboBox);
//         #endregion
        
//         // Button flightScheduleButton = new Button() {
//         //     Text = "Vlucht Schemas",
//         //     Y = Pos.Bottom(ExpireDateLabel) + 1,
//         // };
//         // flightScheduleButton.Clicked += () => { WindowManager.GoForwardOne(new FlightPanel(WindowManager.Flights)); };

//         Button backButton = new Button() {
//             Text = "Terug",
//             Y = Pos.Bottom(exipreDayComboBox),
//         };

//         backButton.Clicked += () => { WindowManager.GoBackOne(this); };

//         Add(backButton);
//     }
// }
public class UserSearch : Toplevel
{
    public UserSearch(bool isReservation = false)
    {
        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };
        TextField searchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        // Want a list of users from the database
        List<User> users = new List<User>() {new User(1, "Levi", "van", "Daalen", "1234", new MailAddress("2004levi@gmail.com"), "+31|613856964", new DateTime(2004, 1, 19), "Nederland") {
            Reservations = new List<Ticket>() { new Ticket(WindowManager.Flights.First(), 1, "B3", DateTime.Now.AddDays(2))}},
            new User(2, "Steyn", "", "Hellendoorn", "password", new MailAddress("idk@gmail.com"), "+31|012345678", DateTime.Now.AddYears(-21), "Nederlands")
        };



        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(users);
        if (isReservation)
            usersView.OpenSelectedItem += (item) => {  WindowManager.GoForwardOne(new SearchReservationn((User)item.Value)); };
        else
            usersView.OpenSelectedItem += (item) => { WindowManager.GoForwardOne(new SearchReservationn((User)item.Value)); };

        searchBox.TextChanged += (text) => {usersView.SetSource(users.FindAll((x) => {
            if (x.ToString().ToLower().Contains((string)searchBox.Text.ToLower()))
                return true;
            else return false;
        }));};

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(usersView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, searchBox, usersView, goBackButton);
    }
}
public class SearchReservationn : Toplevel
{
    public SearchReservationn(User user)
    {
        List<Ticket> reservations = user.Reservations;

        if (reservations == null)
            WindowManager.GoBackOne(this);

        Label searchBoxLabel = new Label() {
            Text = "Zoeken:"
        };

        TextField searchBox = new TextField("") {
            Width = 20,
            X = Pos.Right(searchBoxLabel) + 1,
        };

        ListView usersView = new ListView() {
            Y = Pos.Bottom(searchBox) + 1,
            Height = 5,
            Width = Dim.Fill(),
        };

        usersView.SetSource(reservations);

        usersView.OpenSelectedItem += (item) => { if (reservations != null)
            WindowManager.GoForwardOne(new ReservationPanell(user, reservations[item.Item])); };

        searchBox.TextChanged += (text) => {usersView.SetSource(reservations?.FindAll((x) => {
            if (x.ToString().ToLower().Contains((string)searchBox.Text.ToLower()))
                return true;
            else return false;
        }));};

        Button goBackButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(usersView) + 1,
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(searchBoxLabel, searchBox, usersView, goBackButton);
    }
}

public class ReservationPanell : Toplevel
{
    public User CurrentUser;
    public Ticket CurrentTicket;
    public TextField SeatnumberText;
    public ReservationPanell(User user, Ticket ticket)
    {
        CurrentUser = user;
        CurrentTicket = ticket;
        Label flightLabel = new Label() {
            Text = $"Vlucht:\n{ticket.Flight.ToNewLineString()}",
        };

        Label userLabel = new Label() {
            Text = $"User:\n{user.ToNewLineString()}",
            Y = Pos.Bottom(flightLabel) + 1,
        };

        Label seatnumberLabel = new Label() {
            Text = "Ticket:\nStoelnummer:",
            Y = Pos.Bottom(userLabel) + 1,
        };

        SeatnumberText = new TextField(ticket.SeatNumber) {
            X = Pos.Right(seatnumberLabel) + 3,
            Y = Pos.Top(seatnumberLabel) + 1,
            Width = 5,
        };

        Label boardingTimeLabel = new Label() {
            Text = $"Boarding tijd: {ticket.BoardingTime}",
            Y = Pos.Bottom(seatnumberLabel),
        };

        Add(flightLabel, userLabel, seatnumberLabel, SeatnumberText, boardingTimeLabel);

        Button backButton = new Button() {
            Text = "Terug",
            Y = Pos.Bottom(boardingTimeLabel)
        };

        backButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(backButton);
    }
}
}

