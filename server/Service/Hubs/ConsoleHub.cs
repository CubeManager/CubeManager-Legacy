namespace Service.Hubs;

using Microsoft.AspNetCore.SignalR;
using Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ConsoleHub : Hub
{
    private readonly IProcessManagementService processManagementService;
    static readonly Dictionary<string, string> Users = new Dictionary<string, string>();

    public ConsoleHub(IProcessManagementService processManagementService) {
        this.processManagementService = processManagementService;
    }

    public async Task Register(string server)
    {
        if (Users.ContainsKey(server))
        {
            Users.Add(server, this.Context.ConnectionId);
        }

        await Clients.All.SendAsync(WebSocketActions.USER_JOINED, server);
    }

    public async Task Leave(string server)
    {
        Users.Remove(server);
        await Clients.All.SendAsync(WebSocketActions.USER_LEFT, server);
    }

    public async Task Send(string server, string message)
    {
        await Clients.All.SendAsync(WebSocketActions.MESSAGE_RECEIVED, server, message);
    }

    [HubMethodName("SendMessage")]
    public async Task ReceiveMessage(string serverName, string message)
    {
        var server = processManagementService.ActiveServers[serverName];

        if (server != null)
        {
            await server.StandardInput.WriteLineAsync(message);
        }
    }
}

public struct WebSocketActions
{
    public static readonly string MESSAGE_RECEIVED = "messageReceived";
    public static readonly string USER_LEFT = "userLeft";
    public static readonly string USER_JOINED = "userJoined";
}
