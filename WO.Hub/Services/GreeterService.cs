using WO.Hub.Contract;

namespace WO.Hub.Services;

public class GreeterService : IGreeterService
{
    private readonly ILogger<GreeterService> _logger;
    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public Task<HelloReply> SayHello(HelloRequest request)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}