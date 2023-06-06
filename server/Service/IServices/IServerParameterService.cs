namespace Service.IServices;

using System.Diagnostics;
using Domain;
using Service.ViewModel;

public interface IServerParameterService
{
    /// <summary>
    /// 
    /// </summary>
    public double getPCRAM();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pids"></param>
    /// <param name="servers"></param>
    public Task<List<Server>> getPerformance(Dictionary<string, Process> processes, List<ServerViewModel> servers);




}
