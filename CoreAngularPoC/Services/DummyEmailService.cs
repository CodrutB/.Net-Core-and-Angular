using Microsoft.Extensions.Logging;

namespace CoreAngularPoC.Services
{
    public class DummyEmailService : IEmailService
    {
        private readonly ILogger<DummyEmailService> _logger;

        public DummyEmailService(ILogger<DummyEmailService> logger)
        {
            _logger = logger;
        }

        public void SendMessage(string to, string subject, string body)
        {
            _logger.LogInformation($"To : {to} Subject : {subject} Body :{body}");
        }
    }
}
