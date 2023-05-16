namespace Service.Services.Util;

using Service.InputModels;
using Service.Services.Util.Util;

public static class PersistenceUtil
{
    public static string GetServerPath(string serverName)
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"CubeManager\\{serverName}\\"
        );
    }

    public static string GetJarFileName(ServerInputModel serverInput)
    {
        return $"{serverInput.serverType.ToString().ToLower()}-{serverInput.exactVersion}.jar";
    }

    public static string GetJarFileName(string serverName)
    {
        var cubeManagerConfig = CubeManagerConfigUtil.GetCubeManagerConfig(serverName);
        if (!string.IsNullOrEmpty(cubeManagerConfig.jarFile))
        {
            return cubeManagerConfig.jarFile;
        }
        throw new Exception("No jar filename found");
    }
}
