using FirstDemo.Infrastructure.BusinessObjects;
using MailKit.Net.Smtp;
using MimeKit;

namespace FirstDemo.Infrastructure.Services
{
    public class HtmlEmailServive : IEmailService
    {
        private readonly Smtp _emailSettings;
        public HtmlEmailServive(Smtp emailSettins)
        {
            _emailSettings = emailSettins;
        }
        public async Task SendSingleEmail(string receiverName,string receiverEmail, string subject, string body)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_emailSettings.SenderName, 
                _emailSettings.SenderEmail));

            message.To.Add(new MailboxAddress(receiverName, receiverEmail));

            message.Subject = subject;

            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, _emailSettings.UseSSL);
                // Note: only needed if the SMTP server requires authentication
                client.AuthenticateAsync(_emailSettings.Username,_emailSettings.Password);

                client.SendAsync(message);
                client.DisconnectAsync(true);
            }
        }
    }
}
