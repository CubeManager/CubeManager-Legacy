namespace Web.Controllers;

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
    private readonly IServerUpdateService serverUpdateService;
    private readonly IServerDeleteService serverDeleteService;
    private readonly IServerParameterService serverParameterService;
    private readonly IProcessManagementService processManagementService;
    private readonly IServerService serverService;


    public ServerController(
        IServerCreationService serverCreationService, 
        IServerUpdateService serverUpdateService, 
        IServerDeleteService serverDeleteService,
        IProcessManagementService processManagementService,
        IServerParameterService serverParameterService,
        IServerService serverService)
    {
        this.serverCreationService = serverCreationService;
        this.serverUpdateService = serverUpdateService;
        this.serverDeleteService = serverDeleteService;
        this.processManagementService = processManagementService;
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

    [HttpPut]
    public IActionResult UpdateServer([FromBody] ServerInputModel serverInput)
    {
        serverUpdateService.UpdateServer(serverInput);
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

    [HttpPost("{serverName}/start")]
    public async Task<IActionResult> StartServer(string serverName)
    {
        var process = await processManagementService.Start(serverName);
        var hubContext = HttpContext.RequestServices.GetService<IHubContext<ConsoleHub>>();
        ServerBackgroundServiceManager.StartNewBackgroundService(hubContext!, process, serverName);
        return Ok();
    }

    [HttpDelete("{serverName}/stop")]
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

