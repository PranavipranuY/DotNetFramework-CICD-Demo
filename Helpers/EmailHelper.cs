using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace DocumentManager.API.Helpers
{
    public class EmailHelper
    {
        public static void SendRegistrationEmail(
            string userEmail,
            string firstName)
        {
            string senderEmail =
                ConfigurationManager.AppSettings["SenderEmail"];

            string senderPassword =
                ConfigurationManager.AppSettings["SenderPassword"];

            string loginUrl =
                "https://pranavi.com/login";

            MailMessage mail =
                new MailMessage();

            mail.From =
                new MailAddress(senderEmail);

            mail.To.Add(userEmail);

            mail.Subject =
                "Registration Successful";

            mail.Body =
                "Hello " + firstName +
                "<br/><br/>" +
                "Thank you for registering." +
                "<br/><br/>" +
                "Please use the below login URL." +
                "<br/><br/>" +
                loginUrl;

            mail.IsBodyHtml = true;

            SmtpClient smtp =
                new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials =
                new NetworkCredential(
                    senderEmail,
                    senderPassword);

            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}