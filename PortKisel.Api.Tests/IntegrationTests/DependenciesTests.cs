using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Controllers;
using System.Reflection;
using Xunit;

namespace PortKisel.Api.Tests.IntegrationTests
{
    public class DependenciesTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> factory;


        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DependenciesTests"/>
        /// </summary>
        public DependenciesTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder => builder.ConfigureTestAppConfiguration());
        }

        public void ControllerCoreShouldBeResolved(Type controller)
        {
            //Arrange
            using var scope = factory.Services.CreateScope();

            //Act
            var instance = scope.ServiceProvider.GetRequiredService(controller);

            //Assert
            instance.Should().NotBeNull();
        }

        public static IEnumerable<object[]>? ApiControllerCore =>
            Assembly.GetAssembly(typeof(CargoController))
                ?.DefinedTypes
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type))
                .Where(type => !type.IsAbstract)
                .Select(type => new[] { type });
    }
}
