﻿using Service.InputModels;
using Service.IServices;
using Service.Services.Util;
using Service.Services.Util.Util;

namespace Service.Services;

public class ServerUpdateService : IServerUpdateService
{
    IServerPropertiesService serverPropertiesService;
    public ServerUpdateService(IServerPropertiesService serverPropertiesService)
    {
        this.serverPropertiesService = serverPropertiesService;
    }

    public void UpdateServer(ServerInputModel serverInput)
    {
        // Override CubeManager config
        var serverCubeManagerConfig = CubeManagerConfigUtil.CreateServerCubeManagerConfig(PersistenceUtil.GetJarFileName(serverInput), serverInput.maxMemory);
        CubeManagerConfigUtil.SetCubeManagerConfig(serverCubeManagerConfig, serverInput.serverName);

        // Override server.properties
        serverPropertiesService.ChangeServerProperties(serverInput.serverProperties, serverInput.serverName);
    }
}
