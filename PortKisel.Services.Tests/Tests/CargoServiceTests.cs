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
    public class CargoServiceTests : PortContextInMemory
    {
        public readonly ICargoService cargoService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CargoServiceTests"/>
        /// </summary>
        public CargoServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            cargoService = new CargoService(
                new CargoReadRepository(Reader),
                new CompanyZakazchikReadRepository(Reader),
                config.CreateMapper(),
                UnitOfWork,
                new CargoWriteRepository(WriterContext));
        }

        /// <summary>
        /// Получение груз по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await cargoService.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение грузов по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Cargo();
            await Context.Cargos.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoService.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description,
                    target.Weight
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Cargo}"/> по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await cargoService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Cargo}"/> по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var companyZakazchik = TestDataGenerator.CompanyZakazchik(x => x.Id = Guid.NewGuid());
            await Context.CompanyZakazchiks.AddAsync(companyZakazchik);
            var cargo = TestDataGenerator.Cargo(x => { x.Id = Guid.NewGuid(); x.CompanyZakazchikId = companyZakazchik.Id; });

            await Context.Cargos.AddRangeAsync(cargo,
                TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await cargoService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == cargo.Id);
        }

        /// <summary>
        /// Удаление несуществующего <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => cargoService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortEntityNotFoundException<Cargo>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Cargo(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Cargos.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => cargoService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortInvalidOperationException>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Cargo();
            await Context.Cargos.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => cargoService.DeleteAsync(model.Id, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Cargos.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.CargoRequestModel();

            //Act
            var act = await cargoService.AddAsync(model, CancellationToken);

            // Assert
            var entity = Context.Cargos.Single(x => x.Id == act.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.CargoRequestModel();

            //Act
            Func<Task> act = () => cargoService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PortEntityNotFoundException<Cargo>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Cargo"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            var companyZakazchikReq = TestDataGenerator.CompanyZakazchikRequestModel();
            var companyZakazchik = TestDataGenerator.CompanyZakazchik(x => x.Id = companyZakazchikReq.Id);
            await Context.CompanyZakazchiks.AddAsync(companyZakazchik);

            var cargo = TestDataGenerator.Cargo();
            await Context.Cargos.AddAsync(cargo);
            await UnitOfWork.SaveChangesAsync(CancellationToken);
            var cargoReq = TestDataGenerator.CargoRequestModel(x => { x.CompanyZakazchikId = companyZakazchik.Id; x.Id = cargo.Id; });

            //Act
            Func<Task> act = () => cargoService.UpdateAsync(cargoReq, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Cargos.Single(x => x.Id == cargoReq.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    cargoReq.Id,
                    cargoReq.Name,
                    cargoReq.Description,
                    cargoReq.Weight,
                    cargoReq.CompanyZakazchikId
                });
        }
    }
}
