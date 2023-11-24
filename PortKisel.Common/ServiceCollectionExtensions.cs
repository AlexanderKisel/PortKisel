﻿using Microsoft.Extensions.DependencyInjection;

namespace PortKisel.Common
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterModule<TModule>(this IServiceCollection services) where TModule : Common.Module
        {
            var type = typeof(TModule);
            var instance = Activator.CreateInstance(type) as Common.Module;
            instance?.CreateModule(services);
        }
    }
}
