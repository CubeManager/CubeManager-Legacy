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
    private readonly IServerUpdateService serverUpdateService;

    public ServerController(IServerCreationService serverCreationService, IServerPropertiesService serverPropertiesService, IServerUpdateService serverUpdateService)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
        return Ok();
    }

    [HttpPut]
    public IActionResult UpdateServer([FromBody] ServerInputModel serverInput)
    {
        serverUpdateService.UpdateServer(serverInput);
        return Ok();
    }
}
