namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Service.IServices;
using Service.InputModels;
using Domain;
using Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;




[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    private readonly IServerCreationService serverCreationService;
    private readonly IServerPropertiesService serverPropertiesService;
    private readonly IServerUpdateService serverUpdateService;
    private readonly IServerParameterService serverParameterService;
    private readonly IConsoleService consoleService;
    private readonly IProcessManagementService processManagementService;
    private readonly IHubContext<ConsoleHub> hubContext;

    public ServerController(
        IServerCreationService serverCreationService, 
        IServerPropertiesService serverPropertiesService,
        IServerUpdateService serverUpdateService, 
        IConsoleService consoleService,
        IProcessManagementService processManagementService,
        IServerParameterService serverParameterService,
        IHubContext<ConsoleHub> hubContext)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
        this.serverParameterService = serverParameterService;
        this.consoleService = consoleService;
        this.processManagementService = processManagementService;
        this.hubContext = hubContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Server>>> GetAll()
    {
        //TODO = return all servers and set state based on process list
        List<Server> servers = serverParameterService.CreateTestServers();
        Dictionary<string, Process> processes = processManagementService.ActiveServers;
        return await serverParameterService.getPerformance(processes, servers);

    }

    // if needed for single request
    // [HttpGet("{serverName}")]
    // public async Task<ActionResult<Server>> GetOneByName(string serverName)
    // {
    //     List<Server> servers = serverParameterService.CreateTestServers();
    //     Dictionary<string, Process> processes = processManagementService.ActiveServers;
    //     await serverParameterService.getPerformance(processes, servers);
    //     return servers.Find(server => server.name == serverName);

    // }

    [HttpGet("/ram")]
    public ActionResult<double> getRAM() {
        return serverParameterService.getPCRAM();
    }

    [HttpGet("/storage")]
    public ActionResult<double> getUsedStorage() {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),$"CubeManager");
        return new DirectoryInfo(path).EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024 / 1024;
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

