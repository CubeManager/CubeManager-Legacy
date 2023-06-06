using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Service.Hubs;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class ServerOutputSenderService : BackgroundService
{
    private readonly IHubContext<ConsoleHub> _hubContext;
    private readonly Process _serverProcess;
    private readonly string _serverName;

    public ServerOutputSenderService(IHubContext<ConsoleHub> hubContext, Process serverProcess, string serverName)
    {
        _hubContext = hubContext;
        _serverProcess = serverProcess;
        _serverName = serverName;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var outputStreamReader = _serverProcess.StandardOutput)
        {
            while (!outputStreamReader.EndOfStream)
            {
                if (stoppingToken.IsCancellationRequested)
                    break;

                var output = await outputStreamReader.ReadLineAsync();
                await _hubContext.Clients.All.SendAsync(WebSocketActions.MESSAGE_RECEIVED, _serverName, output);
            }
        }
    }
}
