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

    public List<ServerViewModel> GetAllServers()
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

                var serverViewModel = new ServerViewModel
                {
                    serverName = serverName,
                    serverProperties = serverProperties,
                    isRunning = processManagementService.ActiveServers.ContainsKey(serverName)
                };

                serverViewModels.Add(serverViewModel);
            }
        }

        return serverViewModels;
    }

    public ServerViewModel GetServer(string serverName)
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
}
