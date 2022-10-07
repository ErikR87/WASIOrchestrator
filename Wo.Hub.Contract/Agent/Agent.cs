using System.Collections.Concurrent;
using System.Runtime.Serialization;

namespace WO.Hub.Contract.Agent;

[DataContract]
public class Agent
{
    [DataMember(Order = 1)]
    public string Hostname { get; set; }

    [DataMember(Order = 2)]
    public ConcurrentQueue<string> Commands { get; set; } = new ConcurrentQueue<string>();

    public Agent(string hostname)
    {
        Hostname = hostname;
    }
}
