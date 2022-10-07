using System.Runtime.CompilerServices;
using WO.Agent.Config;
using WO.Hub.Contract.Orchestrator;

namespace Wo.Agent.HubClient;

public class HubClient : BackgroundService
{
    private readonly HubConfig _hubConfig;
    private readonly ILogger<HubClient> _logger;
    private readonly IOrchestratorService orchestratorService;

    private ConfiguredCancelableAsyncEnumerable<SubscribtionResponse> _subscribtion;

    public HubClient(IConfiguration configuration, ILogger<HubClient> logger, IOrchestratorService orchestratorService)
    {
        _hubConfig = new HubConfig();
        configuration.GetSection("Hub").Bind(_hubConfig);
        _logger = logger;
        this.orchestratorService = orchestratorService;

        if (_hubConfig.Url == null)
        {
            logger.LogWarning("no hub configuration found");
            return;
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if(_hubConfig.Url == null)
            return;

        try
        {
            _logger.LogInformation($"Connecting to hub {_hubConfig.Url}...");

            _subscribtion = orchestratorService
                .Subscribe()
                .WithCancellation(stoppingToken);

            await foreach(var r in _subscribtion)
            {
                if (orchestratorService != null && r.Command != null)
                {
                    switch (r.Command)
                    {
                        default:
                            _logger.LogWarning($"Command not found: {r.Command}");
                            break;
                    }
                }
            }

            /*if (await Connect())
                _logger.LogInformation("Hub connected.");
            else
                _logger.LogWarning("No hub configuration found.");*/
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex.StackTrace);
        }
    }
}
