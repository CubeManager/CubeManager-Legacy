using Service.Services.Util;

namespace Web.Controllers;

using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;
using Service.Hubs;
using Service.InputModels;
using Service.IServices;
using Service.ViewModel;
using System;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    private readonly IServerCreationService serverCreationService;
    private readonly IServerPropertiesService serverPropertiesService;
    private readonly IServerUpdateService serverUpdateService;
    private readonly IServerParameterService serverParameterService;
    private readonly IConsoleService consoleService;
    private readonly IProcessManagementService processManagementService;
    private readonly IServerService serverService;

    private readonly IHubContext<ConsoleHub> hubContext;

    public ServerController(
        IServerCreationService serverCreationService, 
        IServerPropertiesService serverPropertiesService,
        IServerUpdateService serverUpdateService, 
        IConsoleService consoleService,
        IProcessManagementService processManagementService,
        IHubContext<ConsoleHub> hubContext,
        IServerParameterService serverParameterService,
        IServerService serverService)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
        this.consoleService = consoleService;
        this.processManagementService = processManagementService;
        this.hubContext = hubContext;
        this.serverParameterService = serverParameterService;
        this.serverService = serverService;
    }

    [HttpGet]
    public ActionResult<List<ServerViewModel>> GetAll()
    {
        return serverService.GetAllServers();
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
        return Ok();
    }

    [HttpGet("{serverName}")]
    public ActionResult<ServerViewModel> Get(string serverName)
    {
        return serverService.GetServer(serverName);
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
        string path = $"{PersistenceUtil.GetApplicationPath()}serverjars";
        string[] files = Directory.GetFiles(path);
        List<string> filenames = new List<string>();

        foreach (string file in files)
        {
            string filename = Path.GetFileName(file);
            filenames.Add(filename);
        }

        return filenames;
    }

    [HttpPost("start/{serverName}")]
    public async Task<IActionResult> StartServer(string serverName)
    {
        var process = await processManagementService.Start(serverName);
        var hubContext = HttpContext.RequestServices.GetService<IHubContext<ConsoleHub>>();
        BackgroundServiceManager.StartNewBackgroundService(hubContext!, process, serverName);
        return Ok();
    }

    [HttpDelete("stop/{serverName}")]
    public IActionResult StopServer(string serverName)
    {
        processManagementService.Stop(serverName);
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

