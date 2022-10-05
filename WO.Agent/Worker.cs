using WO.Hub.Contract;

namespace WO.Agent
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOrchestratorService _orchestrator;

        public Worker(ILogger<Worker> logger, IOrchestratorService orchestrator)
        {
            _logger = logger;
            _orchestrator = orchestrator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                var result = await _orchestrator.ApplyAsync(new ApplyRequest
                {
                    Method = "Blub"
                });
                _logger.LogInformation(result.Status.ToString());
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}