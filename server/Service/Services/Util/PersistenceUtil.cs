namespace Service.Services.Util;

using Service.InputModels;

public static class PersistenceUtil
{
    public static string GetApplicationPath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"CubeManager\\"
        );
    }

    public static string GetServerPath()
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"CubeManager\\servers\\"
        );
    }

    public static string GetServerPath(string serverName)
    {
        return Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            $"CubeManager\\servers\\{serverName}\\"
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
