using ProtoBuf.Grpc.Server;
using WO.Hub.Orchestrator;
using WO.Hub.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
const string ALLOW_ALL = "AllowAll";

builder.Services.AddLogging();

builder.Services.AddControllers();

builder.Services.AddCors(o => o.AddPolicy(ALLOW_ALL, builder => {
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("Grpc-Status", "Grpc-Message");
}));

builder.Services.AddSingleton<AgentService>();

builder.Services.AddCodeFirstGrpc(config =>
{
    config.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseGrpcWeb();

app.UseCors(ALLOW_ALL);

app.MapGrpcService<OrchestratorService>()
    .WithMetadata()
    .EnableGrpcWeb()
    .RequireCors(ALLOW_ALL);

app.MapControllers();
app.UseSwagger();

await app.RunAsync();
