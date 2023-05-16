namespace Service.IServices;

using Service.InputModels;

public interface IServerReadService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public ServerViewModel GetServer(string serverName);
}
