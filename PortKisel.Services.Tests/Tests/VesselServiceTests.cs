using AutoMapper;
using FluentAssertions;
using PortKisel.Context.Contracts.Models;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Implementations;
using PortKisel.Services.AutoMappers;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Implementations;
using Xunit;

namespace PortKisel.Services.Tests.Tests
{
    public class VesselServiceTests : PortContextInMemory
    {
        public readonly IVesselService vesselService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="VesselServiceTests"/>
        /// </summary>
        public VesselServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            vesselService = new VesselService(
                new VesselReadRepository(Reader),
                new CompanyPerReadRepository(Reader),
                config.CreateMapper(),
                UnitOfWork,
                new VesselWriteRepository(WriterContext));
        }

        /// <summary>
        /// Получение судна по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await vesselService.GetByAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение суден по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Vessel();
            await Context.Vessels.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselService.GetByAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.LoadCapacity
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Vessel}"/> по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await vesselService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Vessel}"/> по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var companyPer = TestDataGenerator.CompanyPer(x => x.Id = Guid.NewGuid());
            await Context.CompanyPers.AddAsync(companyPer);
            var vessel = TestDataGenerator.Vessel(x => { x.Id = Guid.NewGuid(); x.CompanyPerId = companyPer.Id; });

            await Context.Vessels.AddRangeAsync(vessel,
                TestDataGenerator.Vessel(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await vesselService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == vessel.Id);
        }

        /// <summary>
        /// Удаление несуществующего <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => vesselService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortEntityNotFoundException<Vessel>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Vessel(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Vessels.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => vesselService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortInvalidOperationException>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Vessel();
            await Context.Vessels.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => vesselService.DeleteAsync(model.Id, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Vessels.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.VesselRequestModel();

            //Act
            var act = await vesselService.AddAsync(model, CancellationToken);

            // Assert
            var entity = Context.Vessels.Single(x => x.Id == act.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.VesselRequestModel();

            //Act
            Func<Task> act = () => vesselService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PortEntityNotFoundException<Vessel>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Vessel"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            var companyPerReq = TestDataGenerator.CompanyPerRequestModel();
            var companyPer = TestDataGenerator.CompanyPer(x => x.Id = companyPerReq.Id);
            await Context.CompanyPers.AddAsync(companyPer);

            var vessel = TestDataGenerator.Vessel();
            await Context.Vessels.AddAsync(vessel);
            await UnitOfWork.SaveChangesAsync(CancellationToken);
            var vesselReq = TestDataGenerator.VesselRequestModel(x => { x.CompanyPerId = companyPer.Id; x.Id = vessel.Id; });

            //Act
            Func<Task> act = () => vesselService.UpdateAsync(vesselReq, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Vessels.Single(x => x.Id == vesselReq.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    vesselReq.Id,
                    vesselReq.Name,
                    vesselReq.Description,
                    vesselReq.LoadCapacity,
                    vesselReq.CompanyPerId
                });
        }
    }
}
