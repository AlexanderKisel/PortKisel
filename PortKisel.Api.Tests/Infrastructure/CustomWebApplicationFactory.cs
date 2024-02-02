﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PortKisel.Context;

namespace PortKisel.Api.Tests.Infrastructure
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public static string EnvironmentName = "integration";

        /// <inheritdoc cref="WebApplicationFactory{TEntryPoint}.CreateHost"/>
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(EnvironmentName);
            return base.CreateHost(builder);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestAppConfiguration();
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<PortContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddSingleton(provider =>
                {
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    var optionsBuilder = new DbContextOptionsBuilder<PortContext>()
                        .UseApplicationServiceProvider(provider)
                        .UseSqlServer(connectionString: string.Format(configuration.GetConnectionString("DefaultConnection"), Guid.NewGuid().ToString("N")));
                    return optionsBuilder.Options;
                });
            });
        }
    }
}
