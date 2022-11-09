namespace CompKing.API.Services
{
    public class LocalMailSevice : IMailSevice
    {
        private string _mailTo = "admin@compking.com";
        private string _mailFrom = "noreply@compking.com";

        public void Send(string subject, string message)
        {
            Console.WriteLine($"Mail from {_mailFrom} to {_mailTo}, " + $"with {nameof(LocalMailSevice)}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Message: {message}");

        }
    }
}
