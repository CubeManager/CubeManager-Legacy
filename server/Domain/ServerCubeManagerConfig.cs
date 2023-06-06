namespace Domain;

public class ServerCubeManagerConfig
{
    public string? jarFile { get; set; }
    public int maxMemory { get; set; }
    
    public ServerCubeManagerConfig(string jarFile, int maxMemory)
    {
        this.jarFile = jarFile;
        this.maxMemory = maxMemory;
    }
}
