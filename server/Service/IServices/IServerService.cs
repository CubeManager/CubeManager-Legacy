namespace Service.IServices;

using Service.InputModels;
using Service.ViewModel;

public interface IServerService
{
    public List<ServerViewModel> GetAllServers();
    public ServerViewModel GetServer(string serverName);
}
