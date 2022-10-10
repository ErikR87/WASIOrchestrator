using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace WO.Hub.Contract.Agent;

[DataContract]
public class Agent
{
    [DataMember(Order = 1)]
    public string? Hostname { get; set; }
}
