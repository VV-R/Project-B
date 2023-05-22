using System;
using Newtonsoft.Json;
using System.Net.Mail;
using Terminal.Gui;
using Managers;
using Entities;

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
}