using System.Net;
using System.Net.Mail;
using Terminal.Gui;

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
}