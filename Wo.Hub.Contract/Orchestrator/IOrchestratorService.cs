using ProtoBuf.Grpc;
using System.ServiceModel;

namespace WO.Hub.Contract.Orchestrator;

[ServiceContract]
public interface IOrchestratorService
{
    [OperationContract]
    public ValueTask<Response> ApplyAsync(ApplyRequest request);

    [OperationContract]
    IAsyncEnumerable<SubscribtionResponse> Subscribe(CallContext context = default);
}