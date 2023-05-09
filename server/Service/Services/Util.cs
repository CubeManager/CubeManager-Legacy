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
}
