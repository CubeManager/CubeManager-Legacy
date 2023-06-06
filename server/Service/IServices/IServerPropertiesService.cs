namespace Service.IServices;

using Domain;
using Service.InputModels;

public interface IServerPropertiesService
{
    /// <summary>
    ///     This method will change the sever properties for a given Minecraft Server
    /// </summary>
    /// <param name="serverInputProperties">The Properties you want to update</param>
    /// <param name="serverName">The server you want to change the properties of</param>
    public void ChangeServerProperties(ServerPropertiesInputModel serverInputProperties, string serverName);
    public ServerProperties ParseServerProperties(string filePath);
}
