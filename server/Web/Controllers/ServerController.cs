namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Service.IServices;
using Service.InputModels;
using System.Text.RegularExpressions;
using Domain;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    private List<Server> servers = new List<Server>();
    [HttpGet]
    public async Task<ActionResult<List<Server>>> GetAll()
    {
        //testserver
        if (servers.Count == 0) {
            Server server = new Server();
            server.running = true;
            server.name = "string";
            server.location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),$"CubeManager\\{server.name}\\");;
            server.serverProperties = new ServerProperties();
            server.serverProperties.queryPort = 25565;
            servers.Add(server);
        } 
        List<int> ports = new List<int>();
        foreach (Server server in servers) {
            if (server.running == true) ports.Add(server.serverProperties.queryPort);
        }

        string[] pids = GetPIDsByPorts(ports);
        int index = 0;
        foreach (string pid in pids)
        {
            Process process = Process.GetProcessById(Int32.Parse(pid));
            DirectoryInfo info = new DirectoryInfo(servers[index].location);
            servers[index].cpu = Math.Round(await GetCpuUsageForProcess(process), 4);
            servers[index].ram = process.WorkingSet64 / 1024 / 1024;
            servers[index].storage = info.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length) / 1024 / 1024;
            index++;
        }
        return servers;

    }

    [HttpGet("/ram")]
    public ActionResult<double> getRAM() {
        return getPCRAM();
    }

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
    private async Task<double> GetCpuUsageForProcess(Process process)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime; await Task.Delay(500);
        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime; 
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds; 
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal;
    }

    // help source: https://gist.github.com/cheynewallace/5971686
    private static string[] GetPIDsByPorts(List<int> ports) {
        string[] pids = new string[ports.Count];
        using (Process p = new Process()) {
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.Arguments = "-a -n -o";
            ps.FileName = "netstat.exe";
            ps.UseShellExecute = false;
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            ps.RedirectStandardInput = true;
            ps.RedirectStandardOutput = true;
            ps.RedirectStandardError = true;
            p.StartInfo = ps;
            p.Start();
            string[] rows = Regex.Split(p.StandardOutput.ReadToEnd(), "\r\n");
            int index = 0;
            foreach (int port in ports) {
                string row = Array.FindAll(rows, s => s.Contains(port.ToString()))[0];
                string[] tokens = Regex.Split(row, "\\s+");
                pids[index] = (tokens[5]);
                index++;
            }
        }
        return pids;
    } 

    private static double getPCRAM() {
        var info = new ProcessStartInfo();
        info.FileName = "wmic";
        info.Arguments = "OS get FreePhysicalMemory,TotalVisibleMemorySize /Value";
        info.RedirectStandardOutput = true;
        string output = "";
        using(var process = Process.Start(info))
        {                
            output = process.StandardOutput.ReadToEnd();
        }
 
        var lines = output.Trim().Split("\n");
        var freeMemoryParts = lines[0].Split("=", StringSplitOptions.RemoveEmptyEntries);
        var totalMemoryParts = lines[1].Split("=", StringSplitOptions.RemoveEmptyEntries);
        return Math.Round(double.Parse(totalMemoryParts[1]) / 1024, 0);
    }
}

