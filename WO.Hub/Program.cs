using ProtoBuf.Grpc.Server;
using WO.Hub.Contract;
using WO.Hub.Orchestrator;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
const string ALLOW_ALL = "AllowAll";

builder.Services.AddCors(o => o.AddPolicy(ALLOW_ALL, builder => {
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message");
}));

builder.Services.AddCodeFirstGrpc(config =>
{
    config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
});

var app = builder.Build();

app.UseGrpcWeb(new GrpcWebOptions
{
    DefaultEnabled = true,
});

app.UseCors(ALLOW_ALL);

app.MapGrpcService<OrchestratorService>()
    .WithMetadata()
    .EnableGrpcWeb()
    .RequireCors(ALLOW_ALL);

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.MapPost("Orchestrator", (ApplyRequest request) =>
{
    return new Result
    {
        Status = 23
    };
});

await app.RunAsync();
