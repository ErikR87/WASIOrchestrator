using WO.Hub.Contract;

namespace WO.Hub.Orchestrator;

public class Orchestrator : IOrchestrator
{
    public async ValueTask ApplyAsync(ApplyRequest request)
    {
        var result = new Result
        {
            Status = 1
        };

        //return await Task.FromResult(result);
    }
}
