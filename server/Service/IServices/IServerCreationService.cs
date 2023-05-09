namespace Service.IServices;

using Service.InputModels;

public interface IServerCreationService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public Task CreateServer(ServerInputModel serverInput);
}
