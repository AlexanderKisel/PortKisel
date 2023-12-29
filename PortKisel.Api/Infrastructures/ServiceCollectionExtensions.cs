using PortKisel.Common;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context;
using PortKisel.Repositories;
using PortKisel.Services;
using PortKisel.Shared;

namespace PortKisel.Api.Infrastructures
{
    static internal class ServiceCollectionExtensions
    {
        public static void AddDependencies(this IServiceCollection service)
        {
            service.RegisterAutoMapperProfile<ApiAutoMapperProfile>();
            service.AddTransient<IDateTimeProvider, DateTimeProvider>();
            service.AddTransient<IDbWriterContext, DbWriterContext>();

            service.RegisterModule<ServiceModule>();
            service.RegisterModule<ContextModule>();
            service.RegisterModule<RepositoriyModule>();

            service.RegisterAutoMapper();
        }
    }
}
