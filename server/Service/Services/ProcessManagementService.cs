using Microsoft.AspNetCore.SignalR;
using Service.BackgroundServices;
using Service.Hubs;
using Service.IServices;
using Service.Services.Util;
using System.Diagnostics;

namespace Service.Services;

public class ProcessManagementService : IProcessManagementService
{
    private readonly IHubContext<ConsoleHub> hubContext;

    public Dictionary<string, Process> ActiveServers { get; set; }

    public ProcessManagementService(IHubContext<ConsoleHub> hubContext)
    {
        this.hubContext = hubContext;
        ActiveServers = new Dictionary<string, Process>();
    }

    public async Task<Process> Start(string serverName)
    {
        if (ActiveServers.ContainsKey(serverName))
        {
            throw new Exception($"Server \"{serverName}\" is already running");
        }

        var serverConfig = CubeManagerConfigUtil.GetCubeManagerConfig(serverName);

        var processStartInfo = ServerProcessUtil.CreateServerProcessStartInfo(
            PersistenceUtil.GetServerPath(serverName), 
            serverConfig.jarFile!, 
            serverConfig.maxMemory);

        var process = ServerProcessUtil.StartServerProcess(processStartInfo);

        ActiveServers.Add(serverName, process);

        var backgroundService = new ServerOutputSenderService(hubContext, process, serverName);
        ServerOutputSenderServiceManager.AddBackgroundService(backgroundService, serverName);
        await backgroundService.StartAsync(CancellationToken.None);
        return process;
    }


    public void Restart(string serverName)
    {
        var process = ActiveServers[serverName];
        KillProcess(process);
        process.Start();
        ServerOutputSenderServiceManager.RemoveBackgroundService(serverName);
    }

    public void RestartAll()
    {
        foreach ((string serverName, Process process) in ActiveServers)
        {
            KillProcess(process);
            process.Start();
            ServerOutputSenderServiceManager.RemoveBackgroundService(serverName);
        }

    }

    public void Stop(string serverName)
    {
        var process = ActiveServers[serverName];
        KillProcess(process);
        ActiveServers.Remove(serverName);
        ServerOutputSenderServiceManager.RemoveBackgroundService(serverName);
    }

    public void StopAll()
    {
        foreach((string serverName, Process process) in ActiveServers)
        {
            KillProcess(process);
            ActiveServers.Remove(serverName);
            ServerOutputSenderServiceManager.RemoveBackgroundService(serverName);
        }

    }

    private async void KillProcess(Process process)
    {
        if (!process.HasExited)
        {
            process.StandardInput.WriteLine("stop");
            await process.WaitForExitAsync();
            process.Close();
        }
    }
}
