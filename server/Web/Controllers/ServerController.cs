namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using Service.IServices;
using Service.InputModels;
using Service.Hubs;
using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;
using Domain;
using Service.Services.Util;

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
        IHubContext<ConsoleHub> hubContext)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
        this.consoleService = consoleService;
        this.processManagementService = processManagementService;
        this.hubContext = hubContext;
        this.serverParameterService = serverParameterService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Server>>> GetAll()
    {
        List<int> ports = new List<int>();
        List<Server> servers = serverParameterService.CreateTestServers();
        foreach (Server server in servers) if (server.running == true) ports.Add(server.serverProperties.queryPort);
        string[] pids = serverParameterService.GetPIDsByPorts(ports);
        return await serverParameterService.ProcessesById(pids, servers);

    }

    [HttpGet("/ram")]
    public ActionResult<double> getRAM() {
        return serverParameterService.getPCRAM();
    }

    [HttpGet("/storage")]
    public ActionResult<double> getUsedStorage() {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),$"CubeManager");
        return new DirectoryInfo(path).EnumerateFiles("*", SearchOption.AllDirectories).Sum(fi => fi.Length) / 1024 / 1024;
    }

    [HttpGet("/serverjars")]
    public ActionResult<List<string>> getServerJars()
    {
        string path = $"{PersistenceUtil.GetAppData}\\serverjars";
        return Directory.GetFiles(path).ToList();
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
        return Ok();
    }

    [HttpPost("start/{serverName}")]
    public async Task<IActionResult> StartServer(string serverName)
    {
        var process = await processManagementService.Start(serverName);

        //var hubContext = HttpContext.RequestServices.GetService<IHubContext<ConsoleHub>>();
        //BackgroundServiceManager.StartNewBackgroundService(hubContext!, process, serverName);

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

    [HttpPut]
    public IActionResult UpdateServer([FromBody] ServerInputModel serverInput)
    {
        serverUpdateService.UpdateServer(serverInput);
        return Ok();
    } 
}

