using Travaloud.Core.Interfaces.Services;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Travaloud.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IErrorLoggerService _errorLoggerService;
        private readonly string _username;
        private readonly string _password;
        private readonly string _smtpServer;
        private readonly int _port;

        public EmailService(IConfiguration configuration, IErrorLoggerService errorLoggerService)
        {
            _errorLoggerService = errorLoggerService;
            _username = configuration["EmailConfig:Username"];
            _password = configuration["EmailConfig:Password"];
            _smtpServer = configuration["EmailConfig:SmtpServer"];
            _port = int.Parse(configuration["EmailConfig:Port"]);
        }

        public async Task Send(string to, string subject, string html, string displayName = null, string from = null, string[] bccAddresses = null)
        {
            try
            {
                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress(displayName ?? _username, from ?? _username));
                mail.Sender = new MailboxAddress(displayName ?? _username, from ?? _username);
                mail.To.Add(MailboxAddress.Parse(to));
                if (bccAddresses != null && bccAddresses.Any())
                {
                    foreach (var bccAddress in bccAddresses)
                    {
                        mail.Bcc.Add(MailboxAddress.Parse(bccAddress));
                    }
                }

                var body = new BodyBuilder();
                mail.Subject = subject;
                body.HtmlBody = html;
                mail.Body = body.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                await smtp.ConnectAsync(_smtpServer, _port);

                if (_port != 25)
                    await smtp.AuthenticateAsync(_username, _password);

                await smtp.SendAsync(mail);
                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }
        }

        public void SendMailMessage(string to, string subject, string html, string displayName = null, MailAddress from = null, string[] bccAddresses = null)
        {
            try
            {
                var smtpClient = new SmtpClient();
                smtpClient.EnableSsl = true;
                smtpClient.Host = _smtpServer;
                smtpClient.Port = _port;

                var mailMessage = new MailMessage();
                var fromAddress = from ?? new MailAddress(_username, displayName ?? _username);

                mailMessage.From = fromAddress;
                mailMessage.Subject = subject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = html;

                mailMessage.To.Add(to);

                if (bccAddresses != null && bccAddresses.Any())
                {
                    foreach (var bccAddress in bccAddresses)
                    {
                        mailMessage.Bcc.Add(bccAddress);
                    }
                }

                smtpClient.Send(mailMessage);
            }
            catch (Exception e)
            {
                _errorLoggerService.LogError(e);
            }
        }
    }
}

