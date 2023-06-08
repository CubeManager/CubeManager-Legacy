namespace Service.Services;

using Service.IServices;
using Service.Services.Util;

public class ServerDeleteService : IServerDeleteService {
    public void DeleteServer(string serverName) {
        var serverPath = PersistenceUtil.GetServerPath(serverName);

        if (Directory.Exists(serverPath))
        {
            Directory.Delete(serverPath, recursive: true);
        } else
        {
            throw new Exception("Server Folder doesn't exists");
        }
    }
}