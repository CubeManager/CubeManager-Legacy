using Service.Hubs;
using Web.Extensions;

namespace Web.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseCubeManagerWebSockets(this WebApplication app)
    {
        app.UseWebSockets(new WebSocketOptions
        {
            KeepAliveInterval = TimeSpan.FromSeconds(120),
        });
        return app;
    }

    public static WebApplication UseCubeManagerEndpoints(this WebApplication app)
    {
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<ConsoleHub>("/hub/console");
        });
        return app;
    }
}
