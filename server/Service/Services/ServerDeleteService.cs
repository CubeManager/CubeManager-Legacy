namespace Service.Services;

using Service.IServices;
using Service.Services.Util;

public class ServerDeleteService : IServerDeleteService {

    private IProcessManagementService processManagementService;


    public ServerDeleteService(IProcessManagementService processManagementService)
    {
        this.processManagementService = processManagementService;
    }

    public void DeleteServer(string serverName) {
        var serverPath = PersistenceUtil.GetServerPath(serverName);

        if(processManagementService.ActiveServers.ContainsKey(serverPath))
        {
            processManagementService.Stop(serverName);
        }

        if (Directory.Exists(serverPath))
        {
            Directory.Delete(serverPath, recursive: true);
        } else
        {
            throw new Exception("Server Folder doesn't exists");
        }
    }
}
