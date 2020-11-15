using Microsoft.Extensions.Logging;
using System;

namespace ConsoleWorkFlow.Services
{
    public class HelloService : IHelloService
    {
        private readonly ILogger<HelloService>_logger;

        public HelloService(ILogger<HelloService> logger)
        {
            _logger = logger;
        }

        public void SayHello()
        {
            Console.WriteLine("Hello world!");
            _logger.LogInformation("HelloService executed SayHello");
        }
    }
}
