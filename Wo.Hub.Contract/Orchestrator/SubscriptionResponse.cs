using System.Runtime.Serialization;

namespace WO.Hub.Contract.Orchestrator;

[DataContract]
public class SubscribtionResponse : Response
{
    [DataMember(Order = 100)]
    public string? Command { get; set; }
}
