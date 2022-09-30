using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.ClientFactory;
using WO.Hub.Contract;
using WO.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

GrpcClientFactory.AllowUnencryptedHttp2 = true;

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});

builder.Services.AddScoped(services =>
{
    // Erstelle neuen HttpClient mit GrpcWebHandler
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));

    // Konfiguriere die Server URL
    var channel = GrpcChannel.ForAddress("http://localhost:1518",
        new GrpcChannelOptions { HttpClient = httpClient });

    // Erstelle einen gRPC Service, der zur Kommunkation genutzt wird
    return channel.CreateGrpcService<IGreeterService>();
});

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});

await builder.Build().RunAsync();
