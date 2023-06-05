namespace Service.InputModels;

using Domain.Enums;

public class ServerInputModel
{
    public string serverName { get; set; }
    public ServerType serverType { get; set; }
    public string exactVersion { get; set; }
    public int maxMemory { get; set; }
    public ServerPropertiesInputModel? serverProperties { get; set; }
}
