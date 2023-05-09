﻿namespace Web.Extensions;

using Service.IServices;
using Service.Services;


public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddCubeManagerServices(this IServiceCollection services)
    {
        services.AddScoped<IServerCreationService, ServerCreationService>();
        services.AddScoped<IServerPropertiesService, ServerPropertiesService>();
        services.AddScoped<IServerCubeManagerConfigService, ServerCubeManagerConfigService>();
        services.AddScoped<IServerUpdateService, ServerUpdateService>();
        return services;
    }
}
