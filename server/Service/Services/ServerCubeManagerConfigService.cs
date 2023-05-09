using Domain;

using Service.IServices;
using System.Text.Json;

namespace Service.Services;

public class ServerCubeManagerConfigService : IServerCubeManagerConfigService
{
    public ServerCubeManagerConfig GetCubeManagerConfig(string serverName)
    {
        string cubeManagerConfigFile = Path.Combine(Util.GetServerPath(serverName), "cubemanager-config.json");
        if (!File.Exists(cubeManagerConfigFile))
        {
            throw new Exception("CubeManager config file does not exist");
        }
        
        string jsonString = File.ReadAllText(cubeManagerConfigFile);
        if(jsonString == null || jsonString == "")
        {
            throw new Exception();
        }

        try
        {
            var serverCubeManagerConfig = JsonSerializer.Deserialize<ServerCubeManagerConfig>(jsonString);
            if(serverCubeManagerConfig != null)
            {
                return serverCubeManagerConfig;
            }
            else
            {
                throw new Exception();
            }

        }
        catch
        {
            throw new Exception();
        }
    }

    public void SetCubeManagerConfig(ServerCubeManagerConfig serverCubeManagerConfig, string serverName)
    {
        string cubeManagerConfigFile = Path.Combine(Util.GetServerPath(serverName), "cubemanager-config.json");
        if (!File.Exists(cubeManagerConfigFile))
        {
            File.Create(cubeManagerConfigFile).Close();
        }

        File.WriteAllText(cubeManagerConfigFile, JsonSerializer.Serialize(serverCubeManagerConfig));

    }
}
