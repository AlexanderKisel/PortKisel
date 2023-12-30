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
    public class DocumentiServiceTests : PortContextInMemory
    {
        public readonly IDocumentiService documentiService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="DocumentiServiceTests"/>
        /// </summary>
        public DocumentiServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            documentiService = new DocumentiService(
                new DocumentiReadRepository(Reader),
                new CargoReadRepository(Reader),
                new VesselReadRepository(Reader),
                new StaffReadRepository(Reader),
                config.CreateMapper(),
                new DocumentiWriteRepository(WriterContext),
                UnitOfWork);
        }

        /// <summary>
        /// Получение документа по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await documentiService.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение документа по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Documenti();
            await Context.Documentis.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiService.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Number
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Documenti}"/> по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await documentiService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Documenti}"/> по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var vessel = TestDataGenerator.Vessel(x => x.Id = Guid.NewGuid());
            await Context.Vessels.AddAsync(vessel);
            var cargo = TestDataGenerator.Cargo(x => x.Id = Guid.NewGuid());
            await Context.Cargos.AddAsync(cargo);
            var staff = TestDataGenerator.Staff(x => x.Id = Guid.NewGuid());
            await Context.Staffs.AddAsync(staff);

            var documenti = TestDataGenerator.Documenti(x => { x.Id = Guid.NewGuid(); x.VesselId = vessel.Id; x.CargoId = cargo.Id; x.StaffId = staff.Id; });

            await Context.Documentis.AddRangeAsync(documenti,
                TestDataGenerator.Documenti(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await documentiService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == documenti.Id);
        }

        /// <summary>
        /// Удаление несуществующего <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => documentiService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortEntityNotFoundException<Documenti>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Documenti(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Documentis.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => documentiService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortInvalidOperationException>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Documenti();
            await Context.Documentis.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => documentiService.DeleteAsync(model.Id, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Documentis.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.DocumentiRequestModel();

            //Act
            var act = await documentiService.AddAsync(model, CancellationToken);

            // Assert
            var entity = Context.Documentis.Single(x => x.Id == act.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.DocumentiRequestModel();

            //Act
            Func<Task> act = () => documentiService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PortEntityNotFoundException<Documenti>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Documenti"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            var vesselReq = TestDataGenerator.VesselRequestModel();
            var vessel = TestDataGenerator.Vessel(x => x.Id = vesselReq.Id);
            await Context.Vessels.AddAsync(vessel);

            var cargoReq = TestDataGenerator.CargoRequestModel();
            var cargo = TestDataGenerator.Cargo(x => x.Id = cargoReq.Id);
            await Context.Cargos.AddAsync(cargo);

            var staffReq = TestDataGenerator.StaffRequestModel();
            var staff = TestDataGenerator.Staff(x => x.Id = staffReq.Id);
            await Context.Staffs.AddAsync(staff);

            var documenti = TestDataGenerator.Documenti();
            await Context.Documentis.AddAsync(documenti);
            await UnitOfWork.SaveChangesAsync(CancellationToken);
            var documentiReq = TestDataGenerator.DocumentiRequestModel(x => { x.VesselId = vessel.Id; x.CargoId = cargo.Id; x.StaffId = staff.Id; x.Id = documenti.Id; });

            //Act
            Func<Task> act = () => documentiService.UpdateAsync(documentiReq, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Documentis.Single(x => x.Id == documentiReq.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    documentiReq.Id,
                    documentiReq.Number,
                    documentiReq.VesselId,
                    documentiReq.CargoId,
                    documentiReq.StaffId,
                });
        }
    }
}
