namespace Web.Extensions;

using Service.IServices;
using Service.Services;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCubeManagerServices(this IServiceCollection services)
    {
        services.AddSingleton<IProcessManagementService, ProcessManagementService>();
        services.AddScoped<IServerPropertiesService, ServerPropertiesService>();
        services.AddScoped<IServerCubeManagerConfigService, ServerCubeManagerConfigService>();
        services.AddScoped<IServerCreationService, ServerCreationService>();
        services.AddScoped<IServerService, ServerService>();
        services.AddScoped<IServerUpdateService, ServerUpdateService>();
        services.AddScoped<IServerLogService, ServerLogService>();
        services.AddScoped<IServerDeleteService, ServerDeleteService>();


        return services;
    }

    public static IServiceCollection AddCubeManagerCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        });
        return services;
    }

    /*public static IServiceCollection AddCubeManagerHangfire(this IServiceCollection services)
    {
        services.AddHangfire(config => );

    }*/
}
