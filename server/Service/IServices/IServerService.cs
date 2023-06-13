namespace Service.IServices;

using Service.InputModels;
using Service.ViewModel;

public interface IServerService
{
    public Task<List<ServerViewModel>> GetAllServers();
    public Task<ServerViewModel> GetServer(string serverName);
}
