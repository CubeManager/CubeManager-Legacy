namespace Service.InputModels;

using Domain;
using Domain.Enums;

public class ServerInputModel
{
    public int serverId { get; set; }
    public string serverName { get; set; }
    public ServerType serverType { get; set; }
    public string exactVersion { get; set; }
    public int imageURL { get; set; }
    public int maxMemory { get; set; }
    public ServerProperties serverProperties { get; set; }
}
