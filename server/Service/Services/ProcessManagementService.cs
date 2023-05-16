using Service.IServices;
using Service.Services.Util;
using System.Diagnostics;

namespace Service.Services;

public class ProcessManagementService : IProcessManagementService
{
    public Dictionary<string, Process> ActiveServers { get; set; }

    public ProcessManagementService()
    {
        ActiveServers = new Dictionary<string, Process>();
    }

    public Process Start(string serverName)
    {
        if (ActiveServers.ContainsKey(serverName))
        {
            throw new Exception($"Server \"{serverName}\"is already running");
        }

        var serverConfig = CubeManagerConfigUtil.GetCubeManagerConfig(serverName);
        var processStartInfo = ServerProcessUtil.CreateServerProcessStartInfo(
            PersistenceUtil.GetServerPath(serverName), 
            serverConfig.jarFile!, 
            serverConfig.maxMemory);
        var process = ServerProcessUtil.StartServerProcess(processStartInfo);

        ActiveServers.Add(serverName, process);

        return process;
    }

    public async void Restart(string serverName)
    {
        var process = ActiveServers[serverName];
        await KillProcess(process);
        process.Start();
    }

    public async void RestartAll()
    {
        foreach (Process process in ActiveServers.Values)
        {
            await KillProcess(process);
            process.Start();
        }

    }

    public async void Stop(string serverName)
    {
        var process = ActiveServers[serverName];
        await KillProcess(process);
        ActiveServers.Remove(serverName);
    }

    public async void StopAll()
    {
        foreach((string serverName, Process process) in ActiveServers)
        {
            await KillProcess(process);
            ActiveServers.Remove(serverName);
        }

    }

    private async Task KillProcess(Process process)
    {
        process.Kill();
        await process.WaitForExitAsync();
        return;
    }
}
