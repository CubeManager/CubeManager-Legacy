namespace Service.IServices;

using Service.InputModels;

public interface IServerUpdateService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serverInput"></param>
    public void UpdateServer(ServerInputModel serverInput);
}
