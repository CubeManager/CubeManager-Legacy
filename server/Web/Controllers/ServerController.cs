﻿namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using Service;
using Service.IServices;
using Service.InputModels;
using System.Text.RegularExpressions;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    [HttpGet]
    public async Task<string> GetAll()
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
            Console.WriteLine(process.ProcessName + " CPU: " + 
            await GetCpuUsageForProcess(process) + "%" + " Memory: " + 
            process.WorkingSet64 / 1024 / 1024 + "MB"  + " Storage: " + 
            info.EnumerateFiles( "*", SearchOption.AllDirectories).Sum(file => file.Length) / 1024 / 1024 + "MB");
        }
            return string.Join("/n", (object[])(typeof(Process).GetProperties()));
    }
    private readonly IServerCreationService serverCreationService;

    public ServerController(IServerCreationService serverCreationService)
    {
        this.serverCreationService = serverCreationService;
    }

    [HttpPost]
    public ActionResult CreateServer([FromBody] ServerInputModel serverInput)
    {
        serverCreationService.CreateServer(serverInput);
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


    // private static DateTime lastTime;
    // private static TimeSpan lastTotalProcessorTime;
    // private static DateTime curTime;
    // private static TimeSpan curTotalProcessorTime;

    // private void cpuUsageCounter(Process p){
    //         if (lastTime == null || lastTime == new DateTime())
    //         {
    //             lastTime = DateTime.Now;
    //             lastTotalProcessorTime = p.TotalProcessorTime;
    //         }
    //         else
    //         {
    //             curTime = DateTime.Now;
    //             curTotalProcessorTime = p.TotalProcessorTime;

    //             double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
    //             Console.WriteLine("{0} CPU: {1}%", p.ProcessName, CPUUsage * 100);

    //             lastTime = curTime;
    //             lastTotalProcessorTime = curTotalProcessorTime;
    //         }
    //     }

    // help source: https://gist.github.com/cheynewallace/5971686
    public static string[] GetPIDsByPorts(string[] ports) {
        string[] pids = new string[ports.Length];
        using (Process p = new Process()) {
            ProcessStartInfo ps = new ProcessStartInfo();
            ps.Arguments = "-a -n -o -p";
            ps.FileName = "netstat";
            ps.UseShellExecute = false;
            ps.WindowStyle = ProcessWindowStyle.Hidden;
            ps.RedirectStandardInput = true;
            ps.RedirectStandardOutput = true;
            ps.RedirectStandardError = true;
            p.StartInfo = ps;
            p.Start();
            string[] rows = Regex.Split(p.StandardOutput.ReadToEnd(), "\n");
            int index = 0;
            foreach (string port in ports) {
                string row = Array.FindAll(rows, s => s.Contains(port))[0];
                string[] tokens = Regex.Split(row, "\\s+");
                //pids[index] = (tokens[5]); --> different on Linux netstat
                index++;
            }
        }
        return pids;
    } 
}
