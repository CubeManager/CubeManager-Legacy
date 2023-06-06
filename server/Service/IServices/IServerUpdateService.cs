namespace Service.IServices;

using Service.InputModels;

public interface IServerUpdateService
{
    public void UpdateServer(ServerInputModel serverInput);
}
