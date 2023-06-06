namespace Service.Hubs;

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class PerformanceHub : Hub
{
    public async Task Send(string serverName, double cpu, long ram)
    {
        await Clients.All.SendAsync(WebSocketPerformanceActions.PERFORMANCE_RECEIVED, serverName, cpu, ram);
    }

        public struct WebSocketPerformanceActions
    {
        public static readonly string PERFORMANCE_RECEIVED = "performanceReceived";
    }
}
