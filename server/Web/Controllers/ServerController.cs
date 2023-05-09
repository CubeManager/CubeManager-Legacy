namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Service.IServices;
using Service.InputModels;
using System.Text.RegularExpressions;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    [HttpGet]
    public async Task<ActionResult<string>> GetAll()
    {
        // Server server = new Server;
        // server.serverproperties = new ServerProperties();
        string[] ports = {"25565"};
        string[] pids = GetPIDsByPorts(ports);
        foreach (string pid in pids)
        {
            Process process = Process.GetProcessById(Int32.Parse(pid));
            string path = "C:\\Users\\taawajac\\Minecraft Server";
            DirectoryInfo info = new DirectoryInfo(path);
            string informations = process.ProcessName + " CPU: " + 
            await GetCpuUsageForProcess(process) + "%" + " Memory: " + 
            process.WorkingSet64 / 1024 / 1024 + "MB"  + " Storage: " + 
            info.EnumerateFiles( "*", SearchOption.AllDirectories).Sum(file => file.Length) / 1024 / 1024 + "MB";
            return informations;
        }
            return "error lol";
    }

    public class Server
    {
        public Server(string Name)
        {
            this.Name = Name;
            this.Description = "Desc";
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    private readonly IServerCreationService serverCreationService;
    private readonly IServerPropertiesService serverPropertiesService;

    public ServerController(IServerCreationService serverCreationService, IServerPropertiesService serverPropertiesService)
    {
        this.serverCreationService = serverCreationService;
        this.serverPropertiesService = serverPropertiesService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateServer([FromBody] ServerInputModel serverInput)
    {
        await serverCreationService.CreateServer(serverInput);
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
    public static string[] GetPIDsByPorts(string[] ports) {
        string[] pids = new string[ports.Length];
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
            foreach (string port in ports) {
                string row = Array.FindAll(rows, s => s.Contains(port))[0];
                string[] tokens = Regex.Split(row, "\\s+");
                pids[index] = (tokens[5]);
                index++;
            }
        }
        return pids;
    } 
}

