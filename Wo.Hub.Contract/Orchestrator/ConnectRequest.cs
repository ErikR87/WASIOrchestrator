using System.Runtime.Serialization;

namespace WO.Hub.Contract.Orchestrator;

[DataContract]
public class ConnectRequest
{
    [DataMember(Order = 1)]
    public string Hostname { get; set; }

    [DataMember(Order = 2)]
    public string MacAddress { get; set; }
}
