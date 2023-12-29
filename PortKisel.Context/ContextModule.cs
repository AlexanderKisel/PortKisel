using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PortKisel.Common;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts;

namespace PortKisel.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.TryAddScoped<IPortContext>(provider => provider.GetRequiredService<PortContext>());
            service.TryAddScoped<IDbRead>(provider => provider.GetRequiredService<PortContext>());
            service.TryAddScoped<IDbWriter>(provider => provider.GetRequiredService<PortContext>());
            service.TryAddScoped<IUnitOfWork>(provider => provider.GetRequiredService<PortContext>());
        }
    }
}
