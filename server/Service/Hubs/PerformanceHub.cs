﻿namespace Service.Hubs;

using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PerformanceHub : Hub
{
    static readonly Dictionary<string, string> Performance = new Dictionary<string, string>();
    public async Task Send(string serverName, string message)
    {
        await Clients.All.SendAsync(WebSocketActions.MESSAGE_RECEIVED, serverName, message);
    }
}
