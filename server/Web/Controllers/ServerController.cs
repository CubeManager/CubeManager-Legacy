namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.InputModels;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    private readonly IServerCreationService serverCreationService;
    private readonly IServerPropertiesService serverPropertiesService;

    public ServerController(IServerCreationService serverCreationService, IServerPropertiesService serverPropertiesService)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
    }

    [HttpGet]
    public ActionResult<string> GetAll()
    {
        return Ok("ServerList");
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
        return Ok();
    }

    [HttpPut("properties")]
    public ActionResult ChangeProperties([FromBody] ServerInputModel serverInput)
    {
        
        serverCreationService.CreateServer(serverInput);
        return Ok();
    }
}
