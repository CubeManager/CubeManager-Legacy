namespace Service.IServices;
using Service.ViewModel;

public interface IServerReadService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public ServerViewModel GetServer(string serverName);
}
