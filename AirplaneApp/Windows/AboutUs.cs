using System;
using System.Net;
using System.Net.Mail;
using Terminal.Gui;
using Managers;

namespace Windows;
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
            Text = "Over Rotterdam Airlines\n\n" +
                "Rotterdam Airlines is een innovatieve en ambitieuze luchtvaartorganisatie die gespecialiseerd is in het aanbieden van vluchten binnen Europa.\n" +
                "Onze missie is om hoogwaardige vluchtdiensten aan te bieden die voldoen aan de behoeften en verwachtingen van reizigers binnen Europa.\n" +
                "Door gebruik te maken van moderne technologieën en best practices binnen de luchtvaartindustrie, streven we ernaar om een vlotte en comfortabele reiservaring te bieden aan al onze passagiers.\n\n" +
                "Bij Rotterdam Airlines geloven we dat vliegen meer is dan alleen van punt A naar punt B reizen.\n" +
                "We willen een gevoel van verbondenheid en ontdekking inspireren bij onze passagiers.\n" +
                "Of het nu gaat om zakelijke reizen, vakanties of familiebezoeken, wij begrijpen dat elke reis uniek is.\n" +
                "Daarom stellen we alles in het werk om aan individuele behoeften te voldoen en persoonlijke service van hoge kwaliteit te bieden.\n\n" +
                "Bedankt voor uw interesse in Rotterdam Airlines.\n" +
                "We kijken ernaar uit om u aan boord te verwelkomen en u een aangename reis te bieden binnen Europa.",
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
                nameText.Text = string.Empty;
                subjectComboBox.Text = string.Empty;
                emailText.Text = string.Empty;
                questionText.Text = string.Empty;
                nameText.SetNeedsDisplay();
                questionText.SetNeedsDisplay();
                emailText.SetNeedsDisplay();
                questionText.SetNeedsDisplay();
            }
        };

        Button goBackButton = new Button()
        {
            Text = "Terug",
            Y = Pos.Bottom(questionLabel) + 11
        };

        goBackButton.Clicked += () => { WindowManager.GoBackOne(this); };

        Add(aboutUs, headerInfo, contactDetails, headerQuestions, nameLabel, nameText, subjectLabel, subjectComboBox, emailLabel, emailText, questionLabel, questionText, sendButton, goBackButton);
    }

    private void SendEmail(string name, string Subject, string email, string question)
    {
        var toAddress = new MailAddress("rotterdamairline@outlook.com", "Recipient Name");
        string subject = Subject;
        string body = $"{question}\n\nCorrespondentie e-mailadres: {email}";
        EmailManager.SendOneEmail(subject, body, toAddress);
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