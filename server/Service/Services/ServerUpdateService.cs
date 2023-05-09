using Service.InputModels;
using Service.IServices;

namespace Service.Services;

public class ServerUpdateService : IServerUpdateService
{
    IServerCubeManagerConfigService serverCubeManagerConfigService;
    IServerPropertiesService serverPropertiesService;
    public ServerUpdateService(IServerCubeManagerConfigService serverCubeManagerConfigService, IServerPropertiesService serverPropertiesService)
    {
        this.serverCubeManagerConfigService = serverCubeManagerConfigService;
        this.serverPropertiesService = serverPropertiesService;
    }

    public void UpdateServer(ServerInputModel serverInput)
    {
        var serverPath = Util.GetServerPath(serverInput.serverName);

        // Override CubeManager config
        var serverCubeManagerConfig = serverCubeManagerConfigService.CreateServerCubeManagerConfig(Util.GetJarFileName(serverInput), serverInput.maxMemory);
        serverCubeManagerConfigService.SetCubeManagerConfig(serverCubeManagerConfig, serverInput.serverName);

        // Override server.properties
        serverPropertiesService.ChangeServerProperties(serverInput.serverProperties, serverInput.serverName);
    }
}
