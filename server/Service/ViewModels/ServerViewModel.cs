namespace Service.InputModels;

using Domain;
using Domain.Enums;

public class ServerViewModel
{
    public ServerViewModel(string serverName, ServerType serverType, string exactVersion, int maxMemory, ServerProperties serverProperties)
    {
        this.serverName = serverName;
        this.serverType = serverType;
        this.exactVersion = exactVersion;
        this.maxMemory = maxMemory;
        this.serverProperties = serverProperties;
    }

    public string serverName { get; set; }
    public ServerType serverType { get; set; }
    public string exactVersion { get; set; }
    public int maxMemory { get; set; }
    public ServerProperties serverProperties { get; set; }
}
