namespace Service.Services.Util.Util;

using Domain;
using System.Text.Json;

public static class CubeManagerConfigUtil
{

    public static ServerCubeManagerConfig GetCubeManagerConfig(string serverName)
    {
        string cubeManagerConfigFile = Path.Combine(PersistenceUtil.GetServerPath(serverName), "cubemanager-config.json");
        if (!File.Exists(cubeManagerConfigFile))
        {
            throw new Exception("CubeManager config file does not exist");
        }

        string jsonString = File.ReadAllText(cubeManagerConfigFile);
        if (jsonString == null || jsonString == "")
        {
            throw new Exception();
        }

        try
        {
            var serverCubeManagerConfig = JsonSerializer.Deserialize<ServerCubeManagerConfig>(jsonString);
            if (serverCubeManagerConfig != null)
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

    public static void SetCubeManagerConfig(ServerCubeManagerConfig serverCubeManagerConfig, string serverName)
    {
        string cubeManagerConfigFile = Path.Combine(PersistenceUtil.GetServerPath(serverName), "cubemanager-config.json");
        if (!File.Exists(cubeManagerConfigFile))
        {
            File.Create(cubeManagerConfigFile).Close();
        }

        File.WriteAllText(cubeManagerConfigFile, JsonSerializer.Serialize(serverCubeManagerConfig));

    }

    public static ServerCubeManagerConfig CreateServerCubeManagerConfig(string jarFile, int maxMemory)
    {
        return new ServerCubeManagerConfig(jarFile, maxMemory);
    }
}
