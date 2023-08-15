namespace sunstealer.mvc.odata.Services
{
    // ajm: interface
    public interface IApplication
    {
    }

    // ajm: service
    public class Application : IApplication, IHostedService
    {
        public const bool Auth = false;

        private readonly Microsoft.Extensions.Logging.ILogger<Application> logger;

        public Application(Microsoft.Extensions.Logging.ILogger<Application> logger) {
            this.logger = logger;    
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ApplicationService.StopAsync()");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("ApplicationService.StopAsync()");
            return Task.CompletedTask;
        }
    }
}