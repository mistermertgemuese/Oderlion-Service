using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Oderlion_Service
{
    public class FTPService : IFTPService
    {
        private readonly ILogger<FTPService> _log;
        private readonly IConfiguration _config;

        public FTPService(ILogger<FTPService> log, IConfiguration config)
        {
            _log = log;
            _config = config;
        }
        public void Run()
        {
            _log.LogInformation("FTP Service Starting");
        }
    }
}