using Microsoft.Extensions.DependencyInjection;
using PortKisel.Common;
using PortKisel.Context.Contracts;

namespace PortKisel.Context
{
    public class ContextModule : Module
    {
        public override void CreateModule(IServiceCollection service)
        {
            service.AddScoped<IPortContext, PortContext>();
        }
    }
}
