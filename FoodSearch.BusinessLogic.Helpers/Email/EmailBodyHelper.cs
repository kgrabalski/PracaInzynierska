﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FoodSearch.BusinessLogic.Helpers.Email
{
    public class EmailBodyHelper
    {
        private static readonly string _contentPath = "FoodSearch.BusinessLogic.Helpers.Email.Content.{0}.{1}";

        public static EmailBody Registration(string id, string userName)
        {
            return GetBody(MailHelperMessageType.Registration, new { Id = id, UserName = userName });
        }

        public static EmailBody PasswordResetRequest(string request, string userName)
        {
            return GetBody(MailHelperMessageType.PasswordResetRequest, new { RequestId = request, UserName = userName });
        }

        public static EmailBody PasswordReset(string newPassword, string userName)
        {
            return GetBody(MailHelperMessageType.PasswordReset, new { NewPassword = newPassword, UserName = userName });
        }

        private static EmailBody GetBody(MailHelperMessageType messageType, dynamic parameters)
        {
            string html = GetContentFromStream(messageType, "html");
            string plain = GetContentFromStream(messageType, "txt");

            Type t = parameters.GetType();
            var props = t.GetProperties();
            html = props.Aggregate(html, (current, p) => current.Replace(string.Format("[{0}]", p.Name), p.GetValue(parameters)));
            plain = props.Aggregate(plain, (current, p) => current.Replace(string.Format("[{0}]", p.Name), p.GetValue(parameters)));

#if DEBUG
            html = html.Replace("openkort.pl", "openkorttest.azurewebsites.net");
            plain = plain.Replace("openkort.pl", "openkorttest.azurewebsites.net");
#endif
            return new EmailBody()
            {
                Html = html,
                PlainText = plain
            };
        }

        private static string GetContentFromStream(MailHelperMessageType messageType, string format)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(EmailBodyHelper));
            string streamPath = string.Format(_contentPath, messageType.ToString(), format);
            using (BinaryReader reader = new BinaryReader(assembly.GetManifestResourceStream(streamPath)))
            {
                byte[] buff = new byte[reader.BaseStream.Length];
                reader.BaseStream.Read(buff, 0, (int)reader.BaseStream.Length);
                return Encoding.UTF8.GetString(buff);
            }
        }
    }
}