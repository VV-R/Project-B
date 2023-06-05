using System.Net;
using System.Net.Mail;
using Terminal.Gui;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Entities;

namespace Managers;
public static class EmailManager
{
    private static MailAddress _fromMailaddress = new MailAddress("rotterdamairline@outlook.com");
    private static string _fromMailaddressPass = "Team1Admin1234";

    private static SmtpClient SetupSmtp() {
        return new SmtpClient() {
            Host = "smtp.office365.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_fromMailaddress.Address, _fromMailaddressPass)
        };
    }

    public static void SendOneEmail(string subject, string body, MailAddress toAddress) {
        SmtpClient client = SetupSmtp();
        Application.MainLoop.Invoke(async () => {
            using (MailMessage message = new MailMessage(_fromMailaddress, toAddress) {
                Subject = subject,
                Body = body,
            })  {
                await client.SendMailAsync(message);
            }
        });
    }

    public static void SendEmails(string subject, string body, List<MailAddress> emails) {
        SmtpClient client = SetupSmtp();
        Application.MainLoop.Invoke(async () => {
            foreach (var email in emails) {
                using (MailMessage message = new MailMessage(_fromMailaddress, email) {
                    Subject = subject,
                    Body = body,
                })  {
                    await client.SendMailAsync(message);
                }
            }
        });
    }

    public static void SendInvoice(UserInfo userInfo, decimal totalAmount) {
        string fullName =  $"{userInfo.FirstName}{(userInfo.Preposition != "" ? $" {userInfo.Preposition}" : "")} {userInfo.LastName}";
        string date = DateTime.Now.ToString("dd-MM-yyyy");

        Document document = new Document();
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream("factuur.pdf", FileMode.Create));
        document.Open();
        
        Font bedrijfsnaamFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
        Font adressFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        Font phoneFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 32);


        Paragraph header = new Paragraph("Factuur", headerFont);
        Paragraph companyParagraph = new Paragraph("Rotterdam Airline", bedrijfsnaamFont) {Alignment = Element.ALIGN_RIGHT};;
        Paragraph companyAddress = new Paragraph("Rotterdamn", adressFont) {Alignment = Element.ALIGN_RIGHT};;
        Paragraph companyPhone = new Paragraph("06 123456789", phoneFont) {Alignment = Element.ALIGN_RIGHT};

        companyParagraph.Add(companyAddress);
        companyParagraph.Add(companyPhone);
        header.Add(new Paragraph("Same row") { Alignment = Element.ALIGN_LEFT });
        header.Add(companyParagraph);
        document.Add(header);

        document.Close();
    }
}
