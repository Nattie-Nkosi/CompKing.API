namespace CompKing.API.Services
{
    public interface IMailSevice
    {
        void Send(string subject, string message);
    }
}