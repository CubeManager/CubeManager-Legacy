namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;
using Service.Hubs;
using Service.InputModels;
using Service.IServices;
using Service.ViewModel;
using System;
using System.Diagnostics;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    private readonly IServerCreationService serverCreationService;
    private readonly IServerUpdateService serverUpdateService;
    private readonly IProcessManagementService processManagementService;
    private readonly IServerService serverService;

    public ServerController(
        IServerCreationService serverCreationService, 
        IServerUpdateService serverUpdateService, 
        IProcessManagementService processManagementService,
        IServerService serverService)
    {
        this.serverCreationService = serverCreationService;
        this.serverUpdateService = serverUpdateService;
        this.processManagementService = processManagementService;
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
    public async Task<ActionResult<ServerViewModel>> Get(string serverName)
    {
        return serverService.GetServer(serverName);
    }

    [HttpGet("/ram")]
    public ActionResult<double> getRAM() {
        var info = new ProcessStartInfo();
        info.FileName = "wmic";
        info.Arguments = "OS get TotalVisibleMemorySize /Value";
        info.RedirectStandardOutput = true;
        var process = Process.Start(info);
        string output = process.StandardOutput.ReadToEnd();
        var totalMemoryParts = output.Substring(output.IndexOf('=') + 1);
        return Math.Round((double.Parse(totalMemoryParts) / 1024), 0);
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
        var hubContext2 = HttpContext.RequestServices.GetService<IHubContext<PerformanceHub>>();
        await ServerBackgroundServiceManager.StartPerformanceBackgroundService(hubContext2!, process, serverName);
        return Ok();
    }

    [HttpDelete("{serverName}/stop")]
    public IActionResult StopServer(string serverName)
    {
        processManagementService.Stop(serverName);
        return Ok();
    }
}

