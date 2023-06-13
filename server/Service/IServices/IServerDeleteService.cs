namespace Service.IServices;

public interface IServerDeleteService
{
    /// <summary>
    /// Delete Server
    /// </summary>
    /// <param name="serverInput"></param>
    public void DeleteServer(string serverName);
}
