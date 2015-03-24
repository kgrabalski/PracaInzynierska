using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FoodSearch.BusinessLogic.Helpers.Email
{
    public class EmailHelper
    {
        private static readonly string EmailAddress = "kgfoodsearch@gmail.com";
        private static readonly string EmailPassword = "fsP@ssw0rd";

        public static void Send(MailHelperSendFrom from, IList<string> to, string subject, EmailBody body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(EmailAddress);
            foreach (var x in to)
            {
                message.Bcc.Add(x);
            }
            message.Subject = subject;
            //message.IsBodyHtml = true;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;
            //message.Body = body.Html;
            //message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body.PlainText, Encoding.UTF8, "text/plain"));
            message.Body = body.PlainText;
            message.BodyEncoding = Encoding.GetEncoding("windows-1250");

            SmtpClient smtpClient = new SmtpClient("smtp.googlemail.com", 587);
            smtpClient.Credentials = new NetworkCredential(EmailAddress, EmailPassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }

        public static void Send(string from, IList<string> to, string subject, string body)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(from, "FoodSearch.pl");
            foreach (var x in to)
            {
                message.Bcc.Add(x);
            }
            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Priority = MailPriority.Normal;
            message.Body = body;

            SmtpClient smtpClient = new SmtpClient("smtp.googlemail.com", 587);
            smtpClient.Credentials = new NetworkCredential(EmailAddress, EmailPassword);
            smtpClient.EnableSsl = true;
            smtpClient.Send(message);
        }
    }
}
