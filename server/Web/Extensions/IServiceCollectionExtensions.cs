namespace Web.Extensions;

using Service.IServices;
using Service.Services;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCubeManagerServices(this IServiceCollection services)
    {
        services.AddScoped<IServerCreationService, ServerCreationService>();
        return services;
    }
}
