namespace Service.BackgroundServices;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Service.Hubs;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

public static class BackgroundServiceManager
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
        AddBackgroundService(serverOutputSenderService);
        //serverOutputSenderService.(CancellationToken.None);
    }

    public static void AddBackgroundService(IHostedService service)
    {
        BackgroundServices.TryAdd(service.GetType().Name, service);
    }

    public static void RemoveBackgroundService(IHostedService service)
    {
        BackgroundServices.TryRemove(service.GetType().Name, out _);
    }

    public static async Task StopAllBackgroundServicesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var backgroundService in BackgroundServices.Values)
        {
            await backgroundService.StopAsync(cancellationToken);
        }
    }
}

