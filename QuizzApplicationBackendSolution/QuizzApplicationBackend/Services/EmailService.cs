using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;

namespace QuizzApplicationBackend.Services
{
    
        public class SmtpSettings
        {
            public string Server { get; set; }
            public int Port { get; set; }
            public string SenderEmail { get; set; }
            public string SenderName { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public bool EnableSsl { get; set; }
        }

        public class EmailService
        {
            private readonly SmtpSettings _smtpSettings;
            private readonly ILogger<EmailService> _logger;

            public EmailService(IOptions<SmtpSettings> smtpSettings, ILogger<EmailService> logger)
            {
                _smtpSettings = smtpSettings.Value;
                _logger = logger;
            }

            public async Task SendEmailAsync(string toEmail, string subject, string body)
            {
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                email.To.Add(MailboxAddress.Parse(toEmail));
                email.Subject = subject;

                // Set the email body with plain text or HTML
                var builder = new BodyBuilder { HtmlBody = body };
                email.Body = builder.ToMessageBody();

                using var smtp = new SmtpClient();
                try
                {
                    await smtp.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, _smtpSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                    await smtp.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await smtp.SendAsync(email);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Email sending failed: {ex.Message}");
                    throw new Exception("An error occurred while sending the email.", ex);
                }
                finally
                {
                    await smtp.DisconnectAsync(true);
                }
            }
        
    }
}
