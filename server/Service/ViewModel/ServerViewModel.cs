using Domain;
using Domain.Enums;

namespace Service.ViewModel;

public class ServerViewModel 
{ 
    public string? serverName { get; set; }
    public ServerType serverType { get; set; }
    public string? exactVersion { get; set; }
    public int maxMemory { get; set; }
    public double cpu { get; set; }
    public long memory { get; set; }
    public bool isRunning { get; set; }
    public ServerProperties serverProperties { get; set; }
}
