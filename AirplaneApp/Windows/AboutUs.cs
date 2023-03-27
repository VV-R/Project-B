using System;
using System.Net;
using System.Net.Mail;
using Terminal.Gui;

public class AboutUs : Toplevel
{
    TextField emailText;
    TextField nameText;
    ComboBox subjectComboBox;
    TextView questionText;
    public AboutUs()
    {
        Label aboutUs = new Label()
        {
            Text = "Over Ons:\nLorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris tincidunt euismod neque, in aliquam orci fermentum ac. \nDonec tortor diam, semper lacinia lorem placerat, venenatis aliquet augue. Etiam in ipsum dapibus, faucibus augue ac, semper dolor. \nAenean est est, mattis eu blandit sit amet, aliquet sit amet nulla. Sed dapibus dapibus lacus, in condimentum orci tempus sed. \nNullam vel arcu in ante sollicitudin sollicitudin et at lorem. Maecenas ultricies accumsan justo ac laoreet. Quisque a hendrerit mauris, \nvitae pretium erat. Aenean cursus, metus ac vehicula bibendum, quam ligula sollicitudin diam, id bibendum leo eros vel ex. \nVivamus cursus eget diam ac posuere. Donec vel lacus magna.",
        };

        Label headerInfo = new Label()
        {
            Text = "Contactgegevens:\n",
            Y = Pos.Bottom(aboutUs) + 1,
        };

        Label contactDetails = new Label()
        {
            Text = "Postadres:      Driemanssteeweg 107, 3011 WN in Rotterdam \nTelefoonnummer: +31 6 59455109",
            X = Pos.Right(headerInfo) + 4,
            Y = Pos.Bottom(aboutUs) + 1
        };

        Label headerQuestions = new Label()
        {
            Text = "Vragenformulier:",
            Y = Pos.Bottom(contactDetails) + 1
        };

        Label nameLabel = new Label()
        {
            Text = "Naam:",
            X = Pos.Right(headerQuestions) + 4,
            Y = Pos.Bottom(contactDetails) + 1
        };

        nameText = new TextField("")
        {
            X = Pos.Right(nameLabel) + 24,
            Y = Pos.Bottom(contactDetails) + 1,
            Width = 30
        };

        Label subjectLabel = new Label()
        {
            Text = "Onderwerp van de vraag:",
            X = Pos.Right(headerQuestions) + 4,
            Y = Pos.Bottom(nameLabel) + 1
        };

        subjectComboBox = new ComboBox()
        {
            X = Pos.Right(subjectLabel) + 6,
            Y = Pos.Bottom(nameLabel) + 1,
            Width = 20,
            Height = 7
        };
        subjectComboBox.SetSource(new List<string>() { "Registratie", "Inloggen", "Vluchtinformatie", "Inchecken", "Annulering", "Omboeken" });

        Label emailLabel = new Label()
        {
            Text = "Correspondentie e-mailadres:",
            X = Pos.Right(headerQuestions) + 4,
            Y = Pos.Bottom(subjectLabel) + 1
        };

        emailText = new TextField("")
        {
            X = Pos.Right(emailLabel) + 1,
            Y = Pos.Bottom(subjectLabel) + 1,
            Width = Dim.Percent(30)
        };

        Label questionLabel = new Label()
        {
            Text = "Uw vraag:",
            X = Pos.Right(headerQuestions) + 4,
            Y = Pos.Bottom(emailLabel) + 1
        };

        questionText = new TextView()
        {
            X = Pos.Right(questionLabel) + 20,
            Y = Pos.Bottom(emailLabel) + 1,
            Width = Dim.Percent(30),
            Height = 11,
            WordWrap = true
        };

        Button sendButton = new Button()
        {
            Text = "Versturen",
            X = Pos.Right(questionLabel) + 20,
            Y = Pos.Bottom(questionLabel) + 11
        };

        sendButton.Clicked += () =>
        {
            if (CheckMail() != null)
            {
                string name = (string)nameText.Text;
                string subject = (string)subjectComboBox.Text;
                string email = (string)emailText.Text;
                string question = (string)questionText.Text;
                SendEmail(name, subject, email, question);
                MessageBox.Query("Bevestiging", "Uw vraag is opgestuurd en zal binnenkort worden beantwoordt.", "Ok");
            }
        };

        Button goBackButton = new Button()
        {
            Text = "Terug",
            Y = Pos.Bottom(questionLabel) + 11
        };

        goBackButton.Clicked += () => { WindowManager.SetWindow(this, new LoginMenu()); };

        Add(aboutUs, headerInfo, contactDetails, headerQuestions, nameLabel, nameText, subjectLabel, subjectComboBox, emailLabel, emailText, questionLabel, questionText, sendButton, goBackButton);
    }

    private void SendEmail(string name, string Subject, string email, string question)
    {
        var fromAddress = new MailAddress("rotterdamairline@outlook.com", name);
        var toAddress = new MailAddress("rotterdamairline@outlook.com", "Recipient Name");
        const string fromPassword = "Team1Admin1234";
        string subject = Subject;
        string body = $"{question}\n\nCorrespondentie e-mailadres: {email}";

        var smtp = new SmtpClient
        {
            Host = "smtp.office365.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            smtp.Send(message);
        }
    }

    private string? CheckMail()
    {
        if (nameText.Text == "" || subjectComboBox.Text == "" || emailText.Text == "" || questionText.Text == "")
        {
            MessageBox.ErrorQuery("Registreren", "Sommige velden zijn niet ingevuld.", "Ok");
            return null;
        }

        MailAddress address;
        bool isValid = false;
        try
        {
            address = new MailAddress((string)emailText!.Text);
            isValid = (address.Address == (string)emailText.Text);
        }
        catch (FormatException)
        {
            MessageBox.ErrorQuery("Registreren", "Onjuist email", "Ok");
            return null;
        }
        if (!isValid)
        {
            MessageBox.ErrorQuery("Registreren", "Onjuist email", "Ok");
            return null;
        }
        else return "GOOD";
    }
}