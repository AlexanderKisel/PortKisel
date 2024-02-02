using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context;
using PortKisel.Context.Contracts;
using Xunit;

namespace PortKisel.Api.Tests.Infrastructure
{
    public class PortApiFixture : IAsyncLifetime
    {
        private readonly CustomWebApplicationFactory factory;
        private PortContext? portContext;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="PortApiFixture"/>
        /// </summary>
        public PortApiFixture()
        {
            factory = new CustomWebApplicationFactory();
        }

        Task IAsyncLifetime.InitializeAsync() => PortContext.Database.MigrateAsync();

        async Task IAsyncLifetime.DisposeAsync()
        {
            await PortContext.Database.EnsureDeletedAsync();
            await PortContext.Database.CloseConnectionAsync();
            await PortContext.DisposeAsync();
            await factory.DisposeAsync();
        }

        public CustomWebApplicationFactory Factory => factory;

        public IPortContext Context => PortContext;

        public IUnitOfWork UnitOfWork => PortContext;

        internal PortContext PortContext
        {
            get
            {
                if (portContext != null)
                {
                    return portContext;
                }

                var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
                portContext = scope.ServiceProvider.GetRequiredService<PortContext>();
                return portContext;
            }
        }
    }
}
