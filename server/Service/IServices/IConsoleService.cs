namespace Service.IServices;

using System.Net.WebSockets;

public interface IConsoleService
{
    /// <summary>
    /// Redirects the output stream of a running server to the a websocket
    /// </summary>
    /// <param name="webSocket">The websocket to redirect to</param>
    /// <param name="serverName">The server to redirext from</param>
    /// <returns></returns>ddf
    public Task RedirectConsoleStream(WebSocket webSocket, string serverName);
}
