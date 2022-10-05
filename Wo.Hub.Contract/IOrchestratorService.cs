using System.ServiceModel;
using WO.Hub.Contract;

namespace WO.Hub.Contract;

[ServiceContract]
public interface IOrchestratorService
{
    public ValueTask<Result> ApplyAsync(ApplyRequest request);
}