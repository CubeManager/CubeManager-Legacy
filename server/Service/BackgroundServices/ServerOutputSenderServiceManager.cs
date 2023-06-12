namespace Service.BackgroundServices;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Service.Hubs;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public static class ServerOutputSenderServiceManager
{
    private static readonly ConcurrentDictionary<string, IHostedService> BackgroundServices =
    new ConcurrentDictionary<string, IHostedService>();

    public static void StartNewBackgroundService(IHubContext<ConsoleHub> hubContext, Process serverProcess, string serverName)
    {
        var serverOutputSenderService = new ServerOutputSenderService(
            hubContext: hubContext,
            serverProcess: serverProcess,
            serverName: serverName
        );
        AddBackgroundService(serverOutputSenderService, serverName);
    }

        public static async Task StartPerformanceBackgroundService(IHubContext<PerformanceHub> hubContext, Process serverProcess, string serverName)
    {
        var perfomanceSenderService = new PerfomanceSenderService(
            hubContext: hubContext,
            serverProcess: serverProcess,
            serverName: serverName
        );
        await AddBackgroundService(perfomanceSenderService, serverName);
    }

    public static void AddBackgroundService(IHostedService service, string serverName)
    {
        BackgroundServices.TryAdd(serverName, service);
    }

    public static void RemoveBackgroundService(string serverName)
    {
        BackgroundServices.TryRemove(serverName, out _);
    }

    public static async Task StopAllBackgroundServicesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var backgroundService in BackgroundServices.Values)
        {
            await backgroundService.StopAsync(cancellationToken);
        }
    }
}

