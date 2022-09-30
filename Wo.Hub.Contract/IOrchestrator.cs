using System.ServiceModel;
using WO.Hub.Contract;

namespace WO.Hub.Contract;

[ServiceContract]
public interface IOrchestrator
{
    public ValueTask ApplyAsync(ApplyRequest request);
}