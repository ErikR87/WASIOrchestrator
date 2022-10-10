using System.Collections.Concurrent;

namespace WO.Hub.Contract.Agent;

public class AgentWithCommands : Agent
{
    public ConcurrentQueue<string> Commands { get; set; } = new ConcurrentQueue<string>();
}
