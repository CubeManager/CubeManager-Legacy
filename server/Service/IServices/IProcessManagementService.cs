using System.Diagnostics;

namespace Service.IServices;

public interface IProcessManagementService
{
    public Dictionary<string, Process> ActiveServers { get; set; }
    public Process Start(string serverName);
    public void StopAll();
    public void Stop(string serverName);
    public void Restart(string serverName);
    public void RestartAll();
}
