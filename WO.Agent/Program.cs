using ProtoBuf.Grpc.ClientFactory;
using Wo.Agent.HubClient;
using WO.Agent;
using WO.Hub.Contract.Orchestrator;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCodeFirstGrpcClient<IOrchestratorService>(o =>
        {
            o.Address = new Uri("https://localhost:11001/");
        });
        services.AddHostedService<HubClient>();
        services.AddHostedService<Worker>();
    })
    .Build();

ProtoBuf.Grpc.Client.GrpcClientFactory.AllowUnencryptedHttp2 = true;

await host.RunAsync();
