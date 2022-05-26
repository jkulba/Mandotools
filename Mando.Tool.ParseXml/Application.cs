using Microsoft.Extensions.Logging;

namespace Mando.Tool.ParseXml
{
    public class Application : IApplication
    {
        public readonly ILogger<Application> _logger;

        public Application(ILogger<Application> logger)
        {
            _logger = logger;
        }
        public void Run()
        {
            Console.WriteLine("Hello Joe");
        }

    }
}