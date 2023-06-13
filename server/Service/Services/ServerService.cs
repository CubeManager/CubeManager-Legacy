using System.Diagnostics;
using Domain;
using Service.IServices;
using Service.Services.Util;
using Service.ViewModel;

namespace Service.Services;

public class ServerService : IServerService
{
    private readonly IServerPropertiesService serverPropertiesService;
    private readonly IProcessManagementService processManagementService;


    public ServerService(IServerPropertiesService serverPropertiesService, IProcessManagementService processManagementService)
    {
        this.serverPropertiesService = serverPropertiesService;
        this.processManagementService = processManagementService;
    }

    public async Task<List<ServerViewModel>> GetAllServers()
    {
        var serversDirectory = PersistenceUtil.GetServerPath();

        var serverViewModels = new List<ServerViewModel>();

        if (Directory.Exists(serversDirectory))
        {
            var subdirectories = Directory.GetDirectories(serversDirectory);

            foreach (var subdirectory in subdirectories)
            {
                var serverName = subdirectory.Split("\\").Last();
                var serverProperties = serverPropertiesService.ParseServerProperties($"{subdirectory}\\server.properties");
                var isRunning = processManagementService.ActiveServers.ContainsKey(serverName);
                var serverViewModel = new ServerViewModel
                {
                    serverName = serverName,
                    serverProperties = serverProperties,
                    isRunning = isRunning,
                    cpu = isRunning ? Math.Round(await GetCpuUsageForProcess(processManagementService.ActiveServers[serverName]), 2) : 0,
                    memory = isRunning ? Process.GetProcessById(processManagementService.ActiveServers[serverName].Id).WorkingSet64 / 1024 / 1024 : 0
                };

                serverViewModels.Add(serverViewModel);
            }
        }

        return serverViewModels;
    }

    public async Task<ServerViewModel> GetServer(string serverName)
    {
        var serversDirectory = PersistenceUtil.GetServerPath();
        var serverViewModel = new ServerViewModel();

        if (Directory.Exists(serversDirectory))
        {
            var subdirectories = Directory.GetDirectories(serversDirectory);
            var match = subdirectories.Where(sd => sd.EndsWith($"\\{serverName}")).FirstOrDefault();

            if (match != null)
            {
                var serverProperties = serverPropertiesService.ParseServerProperties($"{match}\\server.properties");
                serverViewModel.serverName = serverName;
                serverViewModel.serverProperties = serverProperties;
                serverViewModel.isRunning = processManagementService.ActiveServers.ContainsKey(serverName);
                serverViewModel.cpu = serverViewModel.isRunning ? Math.Round(await GetCpuUsageForProcess(processManagementService.ActiveServers[serverName]), 2) : 0;
                serverViewModel.memory = serverViewModel.isRunning ? Process.GetProcessById(processManagementService.ActiveServers[serverName].Id).WorkingSet64 / 1024 / 1024 : 0;
            }
            else
            {
                throw new Exception("No server with that name exists.");
            }
        }
        else
        {
            throw new Exception("No servers exist yet.");
        }

        return serverViewModel;
    }

       private async Task<double> GetCpuUsageForProcess(Process process)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime;
        await Task.Delay(500);

        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime;
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds;
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal * 100;
    }
}
