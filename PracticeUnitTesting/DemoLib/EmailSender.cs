using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
    public class EmailSender : IEmailSender
    {
        public int GetEmailSeen(string campaignName)
        {
            throw new NotImplementedException();
        }

        public void Send(string email)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "info@devskill.com"));
            message.To.Add(new MailboxAddress(email, email));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

                I just wanted to let you know that Monica and I were going to go play some paintball, you in?

                -- Joey"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.friends.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate("joey", "password");

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
