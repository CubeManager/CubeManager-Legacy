using Service.IServices;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Text;

namespace Service.Services;

public class ConsoleService : IConsoleService
{
    private IProcessManagementService processManagementService;
    public ConsoleService(IProcessManagementService processManagementService)
    {
        this.processManagementService = processManagementService;
    }

    public Task RedirectConsoleStream(WebSocket webSocket, string serverName)
    {
        throw new NotImplementedException();
    }
}
