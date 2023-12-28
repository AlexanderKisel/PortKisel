using Microsoft.Extensions.DependencyInjection;
using PortKisel.Common;
using PortKisel.Services.AutoMappers;
using PortKisel.Shared;

namespace PortKisel.Services
{
    public class ServiceModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IServiceAnchor>(ServiceLifetime.Scoped);
            service.RegisterAutoMapperProfile<ServiceProfile>();
        }
    }
}
