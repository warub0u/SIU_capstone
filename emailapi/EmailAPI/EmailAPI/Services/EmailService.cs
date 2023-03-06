using EmailAPI.Models;
using MimeKit;
using MailKit.Net.Smtp;


namespace EmailAPI.Services
{
    public class EmailService : IEmailService
    {
        // DECLARE emailConfig VARIABLE
        public readonly EmailConfiguration emailConfig;

        // INJECT DEPENDENCY OF emailConfig VIA CONSTRUCTOR
        public EmailService(EmailConfiguration emailConfig)
        {
            this.emailConfig = emailConfig;
        }

        // SEND EMAIL METHOD
        public void SendEmail(EmailMessage emailMessage)
        {
            var message = CreateEmailMessage(emailMessage);
            Send(message);
        }

        // CREATE EMAIL MESSAGE METHOD
        private MimeMessage CreateEmailMessage(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("email", emailConfig.From));
            message.To.AddRange(emailMessage.To);
            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = emailMessage.Content
            };

            return message;
        }

        // SEND METHOD
        private void Send(MimeMessage mailMessage)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(emailConfig.SmtpServer, emailConfig.Port, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(emailConfig.Username, emailConfig.Password);

                client.Send(mailMessage);
            }
            catch
            {
                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }

        }
    }
}
