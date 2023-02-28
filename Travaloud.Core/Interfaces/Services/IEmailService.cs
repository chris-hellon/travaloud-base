namespace Travaloud.Core.Interfaces.Services
{
    public interface IEmailService
    {
        Task Send(string to, string subject, string html, string displayName = null, string from = null, string[] bccAddresses = null);

        void SendMailMessage(string to, string subject, string html, string displayName = null, System.Net.Mail.MailAddress from = null, string[] bccAddresses = null);
    }
}

