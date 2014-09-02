using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FoodSearch.BusinessLogic.Helpers.Email
{
    public class EmailHelper
    {
        public static void Send(MailHelperSendFrom from, IList<string> to, string subject, EmailBody body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(GetEmailAdress(from), "OpenKort.pl");
            foreach (var x in to)
            {
                message.Bcc.Add(x);
            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.Normal;
            message.Body = body.Html;
            message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body.PlainText, Encoding.UTF8, "text/plain"));
            message.BodyEncoding = Encoding.UTF8;

            SmtpClient smtpClient = new SmtpClient("smtp.googlemail.com", 587);
            smtpClient.Credentials = new NetworkCredential("admin@openkort.pl", "GrabalskiP@ssw0rd");
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        public static void Send(string from, IList<string> to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from, "OpenKort.pl");
            foreach (var x in to)
            {
                message.Bcc.Add(x);
            }
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.googlemail.com", 587);
            smtpClient.Credentials = new NetworkCredential("admin@openkort.pl", "GrabalskiP@ssw0rd");
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        private static string GetEmailAdress(MailHelperSendFrom from)
        {
            switch (from)
            {
                case MailHelperSendFrom.NoReply:
                    return "no-reply@openkort.pl";
                case MailHelperSendFrom.Help:
                    return "pomoc@openkort.pl";
                case MailHelperSendFrom.Admin:
                    return "admin@openkort.pl";
                case MailHelperSendFrom.Contact:
                    return "kontakt@openkort.pl";
                default:
                    return "no-reply@openkort.pl";
            }
        }
    }
}
