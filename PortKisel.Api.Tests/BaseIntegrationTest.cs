using AutoMapper;
using PortKisel.Api.Tests.Infrastructure;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts;
using PortKisel.Services.AutoMappers;
using Xunit;
using PortKisel.Api.Infrastructures;

namespace PortKisel.Api.Tests
{
    /// <summary>
    /// Базовый класс для тестов
    /// </summary>
    [Collection(nameof(PortApiTestCollection))]
    public class BaseIntegrationTest
    {
        protected readonly CustomWebApplicationFactory factory;
        protected readonly IPortContext context;
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;

        public BaseIntegrationTest(PortApiFixture fixture)
        {
            factory = fixture.Factory;
            context = fixture.Context;
            unitOfWork = fixture.UnitOfWork;

            Profile[] profiles = { new ApiAutoMapperProfile(), new ServiceProfile() };

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(profiles);
            });

            mapper = config.CreateMapper();
        }
    }
}
