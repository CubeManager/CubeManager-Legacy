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
    private readonly IServerDeleteService serverDeleteService;
    private readonly IServerParameterService serverParameterService;
    private readonly IConsoleService consoleService;
    private readonly IProcessManagementService processManagementService;
    private readonly IServerService serverService;

    private readonly IHubContext<ConsoleHub> hubContext;

    public ServerController(
        IServerCreationService serverCreationService, 
        IServerPropertiesService serverPropertiesService,
        IServerUpdateService serverUpdateService, 
        IServerDeleteService serverDeleteService,
        IConsoleService consoleService,
        IProcessManagementService processManagementService,
        IHubContext<ConsoleHub> hubContext,
        IServerParameterService serverParameterService,
        IServerService serverService)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
        this.serverUpdateService = serverUpdateService;
        this.serverDeleteService = serverDeleteService;
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

    [HttpDelete("{serverName}")]
    public IActionResult DeleteServer(string serverName)
    {
        processManagementService.Stop(serverName);
        serverDeleteService.DeleteServer(serverName);
        return Ok();
    }
}

