using Microsoft.Extensions.DependencyInjection;
using PortKisel.Common;
using PortKisel.Shared;

namespace PortKisel.Repositories
{
    public class RepositoriyModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AssemblyInterfaceAssignableTo<IRepositoryAnchor>(ServiceLifetime.Scoped);
        }
    }
}
