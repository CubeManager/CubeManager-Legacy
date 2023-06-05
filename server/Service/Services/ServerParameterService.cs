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

    public string[] GetPIDsByPorts(List<int> ports) {
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
                string? row = Array.Find(rows, s => s.Contains(port.ToString()));
                if (row == null) {
                    pids = pids.Skip(1).ToArray();  
                } else {
                    string[] tokens = Regex.Split(row, "\\s+");
                    pids[index] = (tokens[5]);
                }
                index++;
            }
        }
        return pids;
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

    public async Task<List<Server>> ProcessesById(string[] pids, List<Server> servers)
    {
        int index = 0;
        foreach (string pid in pids)
        {
            Process process = Process.GetProcessById(Int32.Parse(pid));
            servers[index].cpu = Math.Round(await GetCpuUsageForProcess(process), 4);
            servers[index].ram = process.WorkingSet64 / 1024 / 1024;
            index++;
        }

        if (pids.Length == 0) {
            foreach (Server server in servers) {
                server.cpu = 0;
                server.ram = 0;
            }
        }

        return servers;
    }
}