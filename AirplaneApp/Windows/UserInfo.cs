using System;
using System.Net.Mail;
using Newtonsoft.Json;
using Terminal.Gui;
using Managers;
using Entities;

namespace Windows;
public class UserInfoWindow : Toplevel
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
    public Label ExpireDateLabel;
    public DateTimeField ExpireDateField;

    public UserInfoWindow(User user)
    {
        #region Name
        Label firstnameLabel = new Label() {
            Text = "Voornaam*:",
        };

        FirstnameText = new TextField(user.UserInfo.FirstName) {
            X = Pos.Right(firstnameLabel) + 1,
            Width = Dim.Percent(10),
        };

        Label prepositionLabel = new Label() {
            Text = "Tussenvoegsel:",
            X = Pos.Right(FirstnameText) + 1
        };

        PrepositionText = new TextField(user.UserInfo.Preposition) {
            X = Pos.Right(prepositionLabel) + 1,
            Width = 10,
        };

        Label lastnameLabel = new Label() {
            Text = "Achternaam:",
            X = Pos.Right(PrepositionText) + 1
        };

        LastnameText = new TextField(user.UserInfo.LastName) {
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

        EmailText = new TextField(user.UserInfo.Email.Address) {
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
        DialCodesComboBox.SelectedItem = DialCodesComboBox.Source.ToList().IndexOf(user.UserInfo.PhoneNumber.Split("|")[0]);

        PhoneText = new TextField(user.UserInfo.PhoneNumber.Split("|")[1]) {
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

        Label dateOfBirthLabel = new Label() {
            Text = "Geboortedatum:",
            X = Pos.Left(emailLabel),
            Y = Pos.Bottom(phoneLabel) + 1,
        };

        DateOfBirthField = new DateTimeField(Enumerable.Range(1960, 46).ToList()) {
            X = Pos.Left(EmailText),
            Y = Pos.Top(dateOfBirthLabel),
        };

        DateOfBirthField.SetDateTime(user.UserInfo.DateOfBirth);

        Add(dateOfBirthLabel, DateOfBirthField);
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
        NationalityComboBox.SelectedItem = NationalityComboBox.Source.ToList().IndexOf(user.UserInfo.Nationality);

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

        DocumentNumber = new TextField(user.UserInfo.DocumentNumber != null ? user.UserInfo.DocumentNumber : "") {
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

        DocumentTypeComboBox.SelectedItem = user.UserInfo.DocumentType != null ? DocumentTypeComboBox.Source.ToList().IndexOf(user.UserInfo.DocumentType) : 0;

        ExpireDateLabel = new Label() {
            Text = "Verval datum:",
            X = Pos.Left(emailLabel) + 2,
            Y = Pos.Bottom(documentNumberLabel) + 1,
        };

        ExpireDateField = new DateTimeField(Enumerable.Range(DateTime.Now.Year, 10).ToList()) {
            X = Pos.Left(EmailText),
            Y = Pos.Top(ExpireDateLabel),
        };

        if (user.UserInfo.ExpirationDate != null)
            ExpireDateField.SetDateTime((DateTime)user.UserInfo.ExpirationDate);
        else
            ExpireDateField.SetDateTime(DateTime.Now);

        Add(documentNumberLabel, DocumentNumber, documentTypeLabel, DocumentTypeComboBox);
        Add(ExpireDateLabel, ExpireDateField);
        #endregion
    }

    public UserInfoWindow() : this(WindowManager.CurrentUser) {}
}

public class EditUserInfo : UserInfoWindow
{
    public EditUserInfo(User user) : base(user)
    {
        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(ExpireDateLabel) + 1,
        };

        editButton.Clicked += () =>
        {
            User? currentUser = WindowManager.CurrentUser;
            if (currentUser != null)
            {
                User? user = EditInfo(currentUser, DateOfBirthField.GetDateTime(), ExpireDateField.GetDateTime());
                if (user != null) {
                    WindowManager.GoBackOne(this);
                }
            }
        };

        Button exitButton = new Button() {
            Text = "Annuleren",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };
        exitButton.Clicked += () => { WindowManager.GoBackOne(this); };
        Add(editButton, exitButton);
    }

    private User? EditInfo(User user, DateTime dateOfBirth, DateTime expireDate)
    {
        if (FirstnameText.Text == "" || LastnameText.Text == "" || PrepositionText.Text == "" ||
        EmailText.Text == "" || PhoneText.Text == "" || DialCodesComboBox.Text == "" || NationalityComboBox.Text == "") {
            MessageBox.ErrorQuery("Aanpassen", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        MailAddress address;
        bool isValid = false;
        try {
            address = new MailAddress((string)EmailText.Text);
            isValid = (address.Address == (string)EmailText.Text);
        } catch (FormatException) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist email", "Ok");
            return null;
        }

        if (!isValid) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist email", "Ok");
            return null;
        }

        if (PhoneText.Text.Length < 9) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist telefoonnummer", "Ok");
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

        if (DocumentTypeComboBox.Text == "" && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Aanpassen", "Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Aanpassen", $"Uw {DocumentTypeComboBox.Text} is vervallen", "Ok");
            return null;
        } else if (DocumentNumber.Text != "") {
            user.UserInfo.ExpirationDate = expireDate;
            user.UserInfo.DocumentNumber = (string)DocumentNumber.Text;
            user.UserInfo.DocumentType = (string)DocumentTypeComboBox.Text;
        }
        string phonenumber = $"{DialCodesComboBox.Text}|{PhoneText.Text}";
        user.UserInfo.FirstName = (string)FirstnameText.Text;
        user.UserInfo.Preposition = (string)PrepositionText.Text;
        user.UserInfo.LastName = (string)LastnameText.Text;
        user.UserInfo.Email = new MailAddress((string)EmailText.Text);
        user.UserInfo.PhoneNumber = phonenumber;
        user.UserInfo.DateOfBirth = dateOfBirth;
        user.UserInfo.Nationality = (string)NationalityComboBox.Text;
        return user;
    }
}


public class EditUserInfoAdmin : UserInfoWindow
{
    public EditUserInfoAdmin(User user) : base(user)
    {
        Label roleLabel = new Label() {
            Text = "Rol:",
            Y = Pos.Bottom(ExpireDateLabel) + 2,
        };

        ComboBox roleComboBox = new ComboBox() {
            Y = Pos.Top(roleLabel),
            X = Pos.Left(EmailText),
            Width = 12,
            Height = 3,
        };

        roleComboBox.SetSource(new List<string>() {"Customer", "Admin"});
        roleComboBox.SelectedItem = roleComboBox.Source.ToList().IndexOf(user.Role);

        Add(roleLabel, roleComboBox);

        Button editButton = new Button() {
            Text = "Aanpassen",
            Y = Pos.Bottom(roleLabel) + 4,
        };

        editButton.Clicked += () => {
                User? userChanged = EditInfo(user, DateOfBirthField.GetDateTime(), ExpireDateField.GetDateTime(), (string)roleComboBox.Text);
                if (userChanged != null) {
                    WindowManager.GoBackOne(this);
                }
        };

        Button deleteButton = new Button() {
            Text = "Verwijderen",
            X = Pos.Right(editButton) + 1,
            Y = Pos.Top(editButton),
        };

        deleteButton.Clicked += () => { if (DeleteUser(user)) WindowManager.GoBackOne(this); };

        Button exitButton = new Button() {
            Text = "Annuleren",
            X = Pos.Right(deleteButton) + 1,
            Y = Pos.Top(editButton),
        };

        exitButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(editButton, deleteButton, exitButton);
    }

    private User? EditInfo(User user, DateTime dateOfBirth, DateTime expireDate, string role)
    {
        if (FirstnameText.Text == "" || LastnameText.Text == "" || PrepositionText.Text == "" ||
        EmailText.Text == "" || PhoneText.Text == "" || DialCodesComboBox.Text == "" || NationalityComboBox.Text == "") {
            MessageBox.ErrorQuery("Aanpassen", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        MailAddress address;
        bool isValid = false;
        try {
            address = new MailAddress((string)EmailText.Text);
            isValid = (address.Address == (string)EmailText.Text);
        } catch (FormatException) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist email", "Ok");
            return null;
        }

        if (!isValid) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist email", "Ok");
            return null;
        }

        if (PhoneText.Text.Length < 9) {
            MessageBox.ErrorQuery("Aanpassen", "Onjuist telefoonnummer", "Ok");
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

        if (DocumentTypeComboBox.Text == "" && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Aanpassen", "Document type niet ingevuld", "Ok");
            return null;
        } else if (currentDate > expireDate && DocumentNumber.Text != "") {
            MessageBox.ErrorQuery("Aanpassen", $"Uw {DocumentTypeComboBox.Text} is vervallen", "Ok");
            return null;
        } else if (DocumentNumber.Text != "") {
            user.UserInfo.ExpirationDate = expireDate;
            user.UserInfo.DocumentNumber = (string)DocumentNumber.Text;
            user.UserInfo.DocumentType = (string)DocumentTypeComboBox.Text;
        }
        string phonenumber = $"{DialCodesComboBox.Text}|{PhoneText.Text}";
        user.UserInfo.FirstName = (string)FirstnameText.Text;
        user.UserInfo.Preposition = (string)PrepositionText.Text;
        user.UserInfo.LastName = (string)LastnameText.Text;
        user.UserInfo.Email = new MailAddress((string)EmailText.Text);
        user.UserInfo.PhoneNumber = phonenumber;
        user.UserInfo.DateOfBirth = dateOfBirth;
        user.UserInfo.Nationality = (string)NationalityComboBox.Text;
        user.Role = role;
        return user;
    }

    private bool DeleteUser(User user)
    {
        int n = MessageBox.ErrorQuery("Verwijderen", $"Weet u zeker dat u klant {user.FullName()} wilt verwijderen", "Ja", "Nee");
        if (n == 1)
            return false;
        // ToDo:
        // Remove from the database
        // Should be a few lines
        return true;

    }
}
