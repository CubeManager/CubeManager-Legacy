﻿namespace Web.Extensions;

using Service.IServices;
using Service.Services;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCubeManagerServices(this IServiceCollection services)
    {
        services.AddSingleton<IProcessManagementService, ProcessManagementService>();

        services.AddScoped<IServerCreationService, ServerCreationService>();
        services.AddScoped<IServerParameterService, ServerParameterService>();
        services.AddScoped<IServerPropertiesService, ServerPropertiesService>();
        services.AddScoped<IServerCubeManagerConfigService, ServerCubeManagerConfigService>();
        services.AddScoped<IServerUpdateService, ServerUpdateService>();
        services.AddScoped<IConsoleService, ConsoleService>();

        return services;
    }

    /*public static IServiceCollection AddCubeManagerHangfire(this IServiceCollection services)
    {
        services.AddHangfire(config => );

    }*/
}
