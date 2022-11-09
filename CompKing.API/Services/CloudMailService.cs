namespace CompKing.API.Services
{
    public class CloudMailService : IMailSevice
    {
        private string _mailTo = "admin@compking.com";
        private string _mailFrom = "noreply@compking.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(CloudMailService)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");
        }
    }
}

