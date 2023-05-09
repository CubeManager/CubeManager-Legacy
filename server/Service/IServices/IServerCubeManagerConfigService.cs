namespace Service.IServices;

using Domain;
using Service.InputModels;

public interface IServerCubeManagerConfigService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public ServerCubeManagerConfig GetCubeManagerConfig(string serverName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public void SetCubeManagerConfig(ServerCubeManagerConfig serverCubeManagerConfig, string serverName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jarFile"></param>
    /// <param name="maxMemory"></param>
    /// <returns></returns>
    public ServerCubeManagerConfig CreateServerCubeManagerConfig(string jarFile, int maxMemory);
}
