using Microsoft.AspNetCore.Mvc;
using WO.Hub.Contract.Agent;
using WO.Hub.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace WO.Hub.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AgentController : ControllerBase
{
    private readonly AgentService agentService;

    public AgentController(AgentService agentService)
    {
        this.agentService = agentService;
    }

    [HttpGet("[action]")]
    public async ValueTask<ActionResult<Agent[]>> List()
    {
        var response = await agentService.List();
        
        if (response.HasError)
        {
            return BadRequest(response.ErrorText);
        }
        else
        {
            return Ok(response.Entities);
        }
    }
}
