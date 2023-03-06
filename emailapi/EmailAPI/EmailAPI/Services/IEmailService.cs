using EmailAPI.Models;
using System.Net.Mail;

namespace EmailAPI.Services

{
    public interface IEmailService
    {
        void SendEmail(EmailMessage emailMessage);
    }
}
