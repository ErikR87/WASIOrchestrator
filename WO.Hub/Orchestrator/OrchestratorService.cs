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

    public async IAsyncEnumerable<SubscribtionResponse> Subscribe(Agent agent, CallContext context = default)
    {
        Console.Write("Subscribe");

        // register agent
        var registerResponse = await agentService.RegisterAsync(agent.Hostname, agent);

        if(registerResponse == null)
        {
            throw new NullReferenceException("Register response is null.");
        }

        if(registerResponse.HasError)
        {
            throw new SubscribeException(registerResponse.ErrorText);
        }

        var agentWithCommands = registerResponse.GetEntity<AgentWithCommands>();
        Console.WriteLine("register " + agent.Hostname);
        
        while (context.ServerCallContext != null && !context.ServerCallContext.CancellationToken.IsCancellationRequested)
        {
            Console.WriteLine("subRun...");
            Console.WriteLine(agentWithCommands!.Hostname);
            Console.WriteLine(agentWithCommands!.Commands.FirstOrDefault());
            
            if (agentWithCommands != null && agentWithCommands.Commands.Any())
            {
                string? command = null;

                while(agentWithCommands.Commands.TryDequeue(out command))
                {
                    Console.Write("send to " + agentWithCommands.Hostname);

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
                await agentService.UnregisterAsync(agent.Hostname);
            }
        }

        Console.WriteLine("Unsubscribe");
        await agentService.UnregisterAsync(agent.Hostname);
    }
}
