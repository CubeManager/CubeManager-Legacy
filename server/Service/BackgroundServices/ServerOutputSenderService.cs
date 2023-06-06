using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Service.Hubs;
using System.Diagnostics;

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
        await Task.Run(async () =>
        {
            using (var outputStreamReader = _serverProcess.StandardOutput)
            {
                while (!outputStreamReader.EndOfStream)
                {
                    var output = await outputStreamReader.ReadLineAsync();
                    Debug.WriteLine(output);

                    // Send the output to the WebSocket
                    await _hubContext.Clients.All.SendAsync(WebSocketActions.MESSAGE_RECEIVED, _serverName, output);
                }
            }
        });
    }
}
