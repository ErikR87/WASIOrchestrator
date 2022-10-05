using WO.Hub.Contract;

namespace WO.Hub.Orchestrator;

public class OrchestratorService : IOrchestratorService
{
    public async ValueTask<Result> ApplyAsync(ApplyRequest request)
    {
        var result = new Result
        {
            Status = 1
        };

        return await Task.FromResult(result);
    }
}
