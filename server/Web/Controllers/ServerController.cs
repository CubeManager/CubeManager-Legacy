namespace Web.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

[ApiController]
[Route("servers")]
public class ServerController : ControllerBase { 

    [HttpGet]
    public async Task<string> GetAll()
    {
        
        Process[] processCollection = Process.GetProcesses();
        foreach (Process p in processCollection)
        {
            Console.WriteLine(p.ProcessName);
        }
        Console.WriteLine(Process.GetProcessesByName("Toolkit")[0].ProcessName + " " + await GetCpuUsageForProcess(Process.GetProcessesByName("Toolkit")[0]) + "%");
        lol("Toolkit");
        
            return string.Join("/n", (object[])(typeof(Process).GetProperties()));
    }

    private async Task<double> GetCpuUsageForProcess(Process process)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime; await Task.Delay(500);
        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime; var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds; var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        Console.WriteLine(cpuUsageTotal * 100);
        return cpuUsageTotal * 100;
    }


    private static DateTime lastTime;
    private static TimeSpan lastTotalProcessorTime;
    private static DateTime curTime;
    private static TimeSpan curTotalProcessorTime;

    private void lol(string processName)
    {

        Console.WriteLine("Press the any key to stop...\n");
        while (!Console.KeyAvailable)
        {
            Process[] pp = Process.GetProcessesByName(processName);
            if (pp.Length == 0)
            {
                Console.WriteLine(processName + " does not exist");
            }
            else
            {
                Process p = pp[0];
                if (lastTime == null || lastTime == new DateTime())
                {
                    lastTime = DateTime.Now;
                    lastTotalProcessorTime = p.TotalProcessorTime;
                }
                else
                {
                    curTime = DateTime.Now;
                    curTotalProcessorTime = p.TotalProcessorTime;

                    double CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                    Console.WriteLine("{0} CPU: {1:0.0}%", processName, CPUUsage * 100);

                    lastTime = curTime;
                    lastTotalProcessorTime = curTotalProcessorTime;
                }
            }

            Thread.Sleep(250);
        }
    }

}
