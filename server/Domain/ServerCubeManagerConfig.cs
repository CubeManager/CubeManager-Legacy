using Domain.Enums;

namespace Domain;

public class ServerCubeManagerConfig
{
    public ServerCubeManagerConfig(string jarFile, int maxMemory)
    {
        this.jarFile = jarFile;
        this.maxMemory = maxMemory;
    }

    public string? jarFile { get; set; }
    public int maxMemory { get; set; }
}
