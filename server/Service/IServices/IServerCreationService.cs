using Service.InputModels;

namespace Service.IServices;

public interface IServerCreationService
{
    public void CreateServer(ServerInputModel serverInput);
}
