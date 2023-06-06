namespace Service.IServices;

using System.Net.WebSockets;

public interface IServerLogService
{
    List<string> GetLatestServerLog(string serverName);
}
