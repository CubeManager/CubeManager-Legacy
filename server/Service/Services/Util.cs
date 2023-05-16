using Service.InputModels;

namespace Service.Services;

public static class Util
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
}
