namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.InputModels;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    private readonly IServerCreationService serverCreationService;

    public ServerController(IServerCreationService serverCreationService)
    {
        this.serverCreationService = serverCreationService;
    }

    [HttpGet]
    public ActionResult<string> GetAll()
    {
        return Ok("ServerList");
    }

    [HttpPost]
    public ActionResult CreateServer([FromBody] ServerInputModel serverInput)
    {
        serverCreationService.CreateServer(serverInput);
        return Ok();
    }
}
