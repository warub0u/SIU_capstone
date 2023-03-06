using MimeKit;

namespace EmailAPI.Models
{
    public class EmailMessage
    {
        // DATA MODEL FOR EMAIL MESSAGE

        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public EmailMessage(IEnumerable<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
        }

    }
}
