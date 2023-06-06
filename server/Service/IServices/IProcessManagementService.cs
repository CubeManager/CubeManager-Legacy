using Microsoft.AspNetCore.SignalR;
using Service.Hubs;
using System.Diagnostics;

namespace Service.IServices;

public interface IProcessManagementService
{
    public Dictionary<string, Process> ActiveServers { get; set; }
    public Task<Process> Start(string serverName);
    public void StopAll();
    public void Stop(string serverName);
    public void Restart(string serverName);
    public void RestartAll();
}
