using ProtoBuf.Grpc;
using System.ServiceModel;
using WO.Hub.Contract.Agent;

namespace WO.Hub.Contract.Orchestrator;

[ServiceContract]
public interface IOrchestratorService
{
    [OperationContract]
    public ValueTask<Response> ApplyAsync(ApplyRequest request);

    [OperationContract]
    IAsyncEnumerable<SubscribtionResponse> Subscribe(Agent.Agent agent, CallContext context = default);
}