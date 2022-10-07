using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.ClientFactory;
using WO.Hub.Contract.Orchestrator;
using WO.WebApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

GrpcClientFactory.AllowUnencryptedHttp2 = true;

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:11001/")//builder.HostEnvironment.BaseAddress) 
});

builder.Services.AddCodeFirstGrpcClient<IOrchestratorService>(o =>
{
    o.Address = new Uri("https://localhost:11001/");
});

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
});

await builder.Build().RunAsync();
