using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Oderlion_Service
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _log;
        private readonly IConfiguration _config;

        public MailService(ILogger<MailService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }

        public void Run()
        {
            _log.LogInformation("Mail Service Starting");
        }

    }
}