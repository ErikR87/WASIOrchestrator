using WO.Hub.Contract;
using WO.Hub.Contract.Agent;

namespace WO.Hub.Services;

public class AgentService
{
    private readonly IDictionary<string, Agent> _agents = new Dictionary<string, Agent>();

    public async ValueTask<Response> RegisterAsync(string hostname, Agent data)
    {
        var response = new Response();

        if(!_agents.ContainsKey(hostname)) 
        {
            _agents[hostname] = data;
            response.Status = ResultStatus.Success;
            response.AddEntity(data);
        } 
        else
        {
            response.Status = ResultStatus.Error;
            response.AddError("Agend already registred.");
        }

        return await Task.FromResult(response);
    }

    public async ValueTask<Response> UnregisterAsync(string hostname)
    {
        var response = new Response();

        if(_agents.ContainsKey(hostname))
        {
            _agents.Remove(hostname);
            response.Status = ResultStatus.Success;
        }
        else
        {
            response.Status= ResultStatus.Error;
            response.AddError("Agent not registred.");
        }

        return response;
    }

    public async ValueTask<Response> List()
    {
        var response = new Response();

        var list = _agents.Values.ToList();

        foreach(var agent in list)
        {
            response.Entities.Add(agent);
        }

        return await Task.FromResult(response);
    }
}
