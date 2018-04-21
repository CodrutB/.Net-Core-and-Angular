namespace CoreAngularPoC.Services
{
    public interface IEmailService
    {
        void SendMessage(string to, string subject, string body);
    }
}