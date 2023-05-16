namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Service.IServices;
using Service.InputModels;
using Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;




[ApiController]
[Route("servers")]
public class ServerController : ControllerBase
{
    private readonly IServerCreationService serverCreationService;
    private readonly IServerPropertiesService serverPropertiesService;
    private readonly IServerUpdateService serverUpdateService;
    private readonly IConsoleService consoleService;
    private readonly IProcessManagementService processManagementService;
    private readonly IHubContext<ConsoleHub> hubContext;

    public ServerController(
        IServerCreationService serverCreationService, 
        IServerPropertiesService serverPropertiesService,
        IServerUpdateService serverUpdateService, 
        IConsoleService consoleService,
        IProcessManagementService processManagementService,
        IHubContext<ConsoleHub> hubContext)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
        this.consoleService = consoleService;
        this.processManagementService = processManagementService;
        this.hubContext = hubContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
        return Ok();
    }

    [HttpPost("start/{serverName}")]
    public IActionResult StartServer(string serverName)
    {
        var process = processManagementService.Start(serverName);

        var hubContext = HttpContext.RequestServices.GetService<IHubContext<ConsoleHub>>();
        BackgroundServiceManager.StartNewBackgroundService(hubContext!, process, serverName);

        return Ok();
    }

    [Route("console/{serverName}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> GetConsole(string serverName)
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await consoleService.RedirectConsoleStream(webSocket, serverName);
            return Ok();
        }
        else
        {
            return BadRequest();
        }
    }
}

