using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Service.Hubs;
using System.Diagnostics;
using static Service.Hubs.PerformanceHub;

public class PerfomanceSenderService : BackgroundService
{
    private readonly IHubContext<PerformanceHub> _hubContext;
    private readonly Process _serverProcess;
    private readonly string _serverName;

    public PerfomanceSenderService(IHubContext<PerformanceHub> hubContext, Process serverProcess, string serverName)
    {
        _hubContext = hubContext;
        _serverProcess = serverProcess;
        _serverName = serverName;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Run(async () =>
        {

            double cpu = Math.Round(await GetCpuUsageForProcess(_serverProcess), 4);
            long ram = _serverProcess.WorkingSet64 / 1024 / 1024;

            await _hubContext.Clients.All.SendAsync(WebSocketPerformanceActions.PERFORMANCE_RECEIVED, _serverName, cpu, ram);

        });
    }

    private async Task<double> GetCpuUsageForProcess(Process process)
    {
        var startTime = DateTime.UtcNow;
        var startCpuUsage = process.TotalProcessorTime; await Task.Delay(500);
        var endTime = DateTime.UtcNow;
        var endCpuUsage = process.TotalProcessorTime; 
        var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
        var totalMsPassed = (endTime - startTime).TotalMilliseconds; 
        var cpuUsageTotal = cpuUsedMs / (Environment.ProcessorCount * totalMsPassed);
        return cpuUsageTotal;
    }
}
