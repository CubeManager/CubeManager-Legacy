namespace Service.IServices;

using Domain;
public interface IServerParameterService
{
    /// <summary>
    /// 
    /// </summary>
    public double getPCRAM();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="process"></param>
    public  string[] GetPIDsByPorts(List<int> ports);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pids"></param>
    /// <param name="servers"></param>
    public Task<List<Server>> ProcessesById(string[] pids, List<Server> servers);




}
