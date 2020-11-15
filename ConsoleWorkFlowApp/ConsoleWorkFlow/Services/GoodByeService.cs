using Microsoft.Extensions.Logging;
using System;

namespace ConsoleWorkFlow.Services
{
    public class GoodByeService : IGoodByeService
    {
        private readonly ILogger<GoodByeService> _logger;

        public GoodByeService(ILogger<GoodByeService> logger)
        {
            _logger = logger;
        }

        public void SayGoodBye()
        {
            Console.WriteLine("GoodBye world!");
            _logger.LogInformation("GoodBye Service executed SayGoodBye");
        }
    }
}
