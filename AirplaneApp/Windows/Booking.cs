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
    public DateTimeField DateOfBirthField;
    public ComboBox DialCodesComboBox;
    public TextField PhoneText;
    public ComboBox NationalityComboBox;
    public TextField DocumentNumber;
    public ComboBox DocumentTypeComboBox;
    public Label ExpireDateLabel;
    public DateTimeField ExpireDateField;

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

    public Booking() // Flight flight
    {
        User user = WindowManager.CurrentUser;
        //Flight = flight;
        if (user != null)
        {
            #region Name
            Label firstnameLabel = new Label()
            {
                Text = "Voornaam*:",
            };

            FirstnameText = new TextField(user.FirstName)
            {
                X = Pos.Right(firstnameLabel) + 1,
                Width = Dim.Percent(10),
            };

            Label prepositionLabel = new Label()
            {
                Text = "Tussenvoegsel:",
                X = Pos.Right(FirstnameText) + 1
            };

            PrepositionText = new TextField(user.Preposition)
            {
                X = Pos.Right(prepositionLabel) + 1,
                Width = 10,
            };

            Label lastnameLabel = new Label()
            {
                Text = "Achternaam:",
                X = Pos.Right(PrepositionText) + 1
            };

            LastnameText = new TextField(user.LastName)
            {
                X = Pos.Right(lastnameLabel) + 1,
                Width = Dim.Percent(10),
            };

            Label attentionLabel = new Label
            {
                Text = "*voornaam zoals op het paspoort",
                X = Pos.Right(LastnameText) + 2,
            };
            Add(firstnameLabel, FirstnameText, prepositionLabel, PrepositionText, lastnameLabel, LastnameText, attentionLabel);
            #endregion

            #region User
            Label emailLabel = new Label()
            {
                Text = "E-mailadres:",
                Y = Pos.Bottom(firstnameLabel) + 1,
            };

            EmailText = new TextField(user.Email.Address)
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
                Y = Pos.Bottom(emailLabel) + 1,
            };
            DialCodesComboBox = new ComboBox()
            {
                X = Pos.Left(EmailText),
                Y = Pos.Top(phoneLabel),
                Width = 7,
                Height = 4,
            };

            DialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));
            DialCodesComboBox.SelectedItem = DialCodesComboBox.Source.ToList().IndexOf(user.PhoneNumber.Split("|")[0]);

            PhoneText = new TextField(user.PhoneNumber.Split("|")[1])
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
            int[] differentDays = { 4, 6, 9, 11 };
            int increaseDay = 1;

            Label dateOfBirthLabel = new Label()
            {
                Text = "Geboortedatum:",
                X = Pos.Left(emailLabel),
                Y = Pos.Bottom(phoneLabel) + 1,
            };

            DateOfBirthField = new DateTimeField(Enumerable.Range(1960, 46).ToList())
            {
                X = Pos.Right(dateOfBirthLabel) + 6,
                Y = Pos.Bottom(phoneLabel) + 1
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
                X = Pos.Left(DialCodesComboBox),
                Y = Pos.Bottom(dateOfBirthLabel) + 1,
                Width = 47,
                Height = 8,
            };

            NationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));
            NationalityComboBox.SelectedItem = NationalityComboBox.Source.ToList().IndexOf(user.Nationality);

            Add(nationalityLabel, NationalityComboBox);
            #endregion

            #region Document Information
            Label documentNumberLabel = new Label()
            {
                Text = "Document nummer:",
                X = Pos.Left(nationalityLabel),
                Y = Pos.Bottom(nationalityLabel) + 1,
            };

            DocumentNumber = new TextField(user.DocumentNumber != null ? user.DocumentNumber : "")
            {
                X = Pos.Left(EmailText),
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
                Y = Pos.Top(documentNumberLabel),
                Width = 10,
                Height = 4,
            };
            DocumentTypeComboBox.SetSource(new List<string>() { "Paspoort", "ID" });

            DocumentTypeComboBox.SelectedItem = user.DocumentType != null ? DocumentTypeComboBox.Source.ToList().IndexOf(user.DocumentType) : 0;

            ExpireDateLabel = new Label()
            {
                Text = "Verval datum:",
                X = Pos.Left(emailLabel),
                Y = Pos.Bottom(documentNumberLabel) + 1,
            };

            ExpireDateField = new DateTimeField(Enumerable.Range(1960, 46).ToList())
            {
                X = Pos.Left(EmailText),
                Y = Pos.Top(ExpireDateLabel),
            };

            Add(documentNumberLabel, DocumentNumber, documentTypeLabel, DocumentTypeComboBox);
            Add(ExpireDateLabel, ExpireDateField);
            #endregion

            Button backButton = new Button()
            {
                Text = "Terug",
                X = 0,
                Y = Pos.Bottom(ExpireDateLabel) + 4
            };

            backButton.Clicked += () => { WindowManager.GoBackOne(this); };

            Add(backButton);
        }
        else
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
            dialCodesComboBox = new ComboBox()
            {
                X = Pos.Left(EmailText),
                Y = Pos.Top(phoneLabel),
                Width = 7,
                Height = 4,
            };

            dialCodesComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(dialcodesFile));

            phoneText = new TextField("")
            {
                X = Pos.Right(dialCodesComboBox) + 1,
                Y = Pos.Top(phoneLabel),
                Width = 39
            };

            phoneText.TextChanged += (text) =>
            {
                if (!int.TryParse(phoneText.Text == "" ? "0" : (string)phoneText.Text, out _))
                    phoneText.Text = text == "" ? "" : text;
                else if (phoneText.Text.Length > 10)
                    phoneText.Text = text;
                phoneText.CursorPosition = phoneText.Text.Length;
            };

            Add(phoneLabel, dialCodesComboBox, phoneText);
            #endregion

            #region Date of birth
            int[] differentDays = { 4, 6, 9, 11 };
            int increaseDay = 1;

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

            nationalityComboBox = new ComboBox()
            {
                X = Pos.Right(nationalityLabel) + 6,
                Y = Pos.Top(nationalityLabel),
                Width = 47,
                Height = 8,
            };

            nationalityComboBox.SetSource(JsonConvert.DeserializeObject<List<string>>(countriesFile));

            Add(nationalityLabel, nationalityComboBox);
            #endregion

            #region Document Information

            Label documentNumberLabel = new Label()
            {
                Text = "Document nummer:",
                X = Pos.Left(emailLabel),
                Y = Pos.Bottom(nationalityLabel) + 1,
            };

            documentNumber = new TextField("")
            {
                X = Pos.Right(documentNumberLabel) + 4,
                Y = Pos.Top(documentNumberLabel),
                Width = Dim.Percent(20) - 2,
            };

            documentNumber.TextChanged += (text) =>
            {
                if (!int.TryParse(documentNumber.Text == "" ? "0" : (string)documentNumber.Text, out _))
                    documentNumber.Text = text == "" ? "" : text;
                else if (documentNumber.Text.Length > 9)
                    documentNumber.Text = text;
                documentNumber.CursorPosition = documentNumber.Text.Length;
            };

            Label documentTypeLabel = new Label()
            {
                Text = "Type:",
                X = Pos.Right(documentNumber) + 1,
                Y = Pos.Top(documentNumberLabel),
            };

            documentTypeComboBox = new ComboBox()
            {
                X = Pos.Right(documentTypeLabel) + 1,
                Y = Pos.Top(documentNumber),
                Width = 10,
                Height = 4,
            };
            documentTypeComboBox.SetSource(new List<string>() { "Paspoort", "ID" });

            Label expireDateLabel = new Label()
            {
                Text = "Verval datum:",
                X = Pos.Left(emailLabel),
                Y = Pos.Bottom(documentNumberLabel) + 1,
            };

            ExpireDateField = new DateTimeField(Enumerable.Range(1960, 46).ToList())
            {
                X = Pos.Right(expireDateLabel) + 7,
                Y = Pos.Top(expireDateLabel),
            };

            Add(documentNumberLabel, documentNumber, documentTypeComboBox, documentTypeLabel);
            Add(expireDateLabel, ExpireDateField);
            #endregion

            Button forwardButton = new Button()
            {
                Text = "Volgende",
                Y = Pos.Bottom(expireDateLabel) + 1
            };

            forwardButton.Clicked += () =>
            {

            };

            Button backButton = new Button()
            {
                Text = "Terug",
                X = Pos.Right(forwardButton) + 1,
                Y = Pos.Bottom(expireDateLabel) + 1,
            };

            backButton.Clicked += () => { WindowManager.GoBackOne(this); };

            Add(forwardButton, backButton);
        }
    }
}


