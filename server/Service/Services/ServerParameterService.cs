using System.Diagnostics;
using System.Text.RegularExpressions;
using Domain;
using Service.IServices;

namespace Service.Services;

public class ServerParameterService : IServerParameterService
{

   public double getPCRAM() {
        var info = new ProcessStartInfo();
        info.FileName = "wmic";
        info.Arguments = "OS get TotalVisibleMemorySize /Value";
        info.RedirectStandardOutput = true;
        var process = Process.Start(info);
        string output = process.StandardOutput.ReadToEnd();
        var totalMemoryParts = output.Substring(output.IndexOf('=') + 1);
        return Math.Round((double.Parse(totalMemoryParts) / 1024), 0);
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

    public List<Server> CreateTestServers() {
     List<Server> servers = new List<Server>();
     Server server = new Server();
            server.running = true;
            server.name = "string";
            server.serverProperties = new ServerProperties();
            server.serverProperties.queryPort = 25565;
            servers.Add(server);

            Server server2 = new Server();
            server2.running = false;
            server2.name = "string3";
            server2.serverProperties = new ServerProperties();
            server2.serverProperties.queryPort = 25566;
            servers.Add(server2);
            return servers;
    }

    public async Task<List<Server>> getPerformance(Dictionary<string, Process> processes, List<Server> servers)
    {
        int index = 0;
        foreach (KeyValuePair<string, Process> process in processes)
        {
            //doesnt work perfectly yet, server and process have to be combined somehow not by index
            servers[index].cpu = Math.Round(await GetCpuUsageForProcess(process.Value), 4);
            servers[index].ram = process.Value.WorkingSet64 / 1024 / 1024;
            index++;
        }

        if (processes.Count == 0) {
            foreach (Server server in servers) {
                server.cpu = 0;
                server.ram = 0;
            }
        }

        return servers;
    }
}