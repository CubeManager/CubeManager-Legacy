using Service.InputModels;

namespace Service.IServices;

public interface IServerService
{
    public void CreateServer(ServerInputModel serverInput);
}
