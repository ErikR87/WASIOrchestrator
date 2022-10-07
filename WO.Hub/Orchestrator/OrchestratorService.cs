using ProtoBuf.Grpc;
using WO.Hub.Contract;
using WO.Hub.Contract.Agent;
using WO.Hub.Contract.Orchestrator;
using WO.Hub.Exceptions;
using WO.Hub.Services;

namespace WO.Hub.Orchestrator;

public class OrchestratorService : IOrchestratorService
{
    private readonly ILogger<OrchestratorService> logger;
    private readonly AgentService agentService;

    public OrchestratorService(ILogger<OrchestratorService> logger, AgentService agentService)
    {
        this.logger = logger;
        this.agentService = agentService;
    }

    public async ValueTask<Response> ApplyAsync(ApplyRequest request)
    {
        var result = new Response
        {
            Status = ResultStatus.Success,
        };

        return await Task.FromResult(result);
    }

    public async IAsyncEnumerable<SubscribtionResponse> Subscribe(CallContext context = default)
    {
        Console.Write("Subscribe");

        var host = context.ServerCallContext?.Host;

        if (host == null)
            throw new Exception("Hostname is missing");

        // register agent
        var registerResponse = await agentService.RegisterAsync(host, new Agent(host));

        if(registerResponse.HasError)
        {
            throw new SubscribeException(registerResponse.ErrorText);
        }

        var agent = registerResponse.GetEntity<Agent>();
        logger.LogInformation(agent == null ? "nulllll" : agent.Hostname);
        Console.WriteLine("register " + host);
        
        while (context.ServerCallContext != null && !context.ServerCallContext.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("subRun...");
            Console.WriteLine(host);
            Console.WriteLine(agent!.Commands.FirstOrDefault());
            
            if (agent != null && agent.Commands.Any())
            {
                string? command = null;

                while(agent.Commands.TryDequeue(out command))
                {
                    Console.Write("send to " + host);

                    if (string.IsNullOrEmpty(command))
                    {
                        continue;
                    }

                    yield return new SubscribtionResponse
                    {
                        Command = command
                    };
                }
            }

            try
            {
                await Task.Delay(TimeSpan.FromSeconds(10), context.CancellationToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unsubscribe - connection lost");
                await agentService.UnregisterAsync(host);
            }
        }

        Console.WriteLine("Unsubscribe");
        await agentService.UnregisterAsync(host);
    }
}
