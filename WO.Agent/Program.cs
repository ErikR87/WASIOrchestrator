using Grpc.Net.Client;
using Grpc.Net.ClientFactory;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.ClientFactory;
using WO.Agent;
using WO.Hub.Contract;


IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddCodeFirstGrpcClient<IOrchestratorService>(o =>
        {
            o.Address = new Uri("https://localhost:11001/");
        });
        services.AddHostedService<Worker>();
    })
    .Build();

ProtoBuf.Grpc.Client.GrpcClientFactory.AllowUnencryptedHttp2 = true;

await host.RunAsync();
