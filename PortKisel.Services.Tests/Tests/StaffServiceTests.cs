using AutoMapper;
using PortKisel.Context.Tests;
using PortKisel.Context.Contracts.Models;
using PortKisel.Context.Contracts.Enums;
using PortKisel.Repositories.Implementations;
using PortKisel.Services.AutoMappers;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Implementations;
using FluentAssertions;
using Xunit;

namespace PortKisel.Services.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="IStaffService"/>
    /// </summary>
    public  class StaffServiceTests : PortContextInMemory
    {
        public readonly IStaffService staffService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StaffServiceTests"/>
        /// </summary>
        public StaffServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            staffService = new StaffService(
                new StaffReadRepository(Reader),
                config.CreateMapper(),
                new StaffWriteRepository(WriterContext),
                UnitOfWork);
        }

        /// <summary>
        /// Получение сотрудника по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await staffService.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение сотрудников по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffService.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.FIO,
                    target.Post
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Staff}"/> по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await staffService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{Staff}"/> по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var staff = TestDataGenerator.Staff(x => { x.Id = Guid.NewGuid(); });

            await Context.Staffs.AddRangeAsync(staff,
                TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await staffService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == staff.Id);
        }

        /// <summary>
        /// Удаление несуществующего <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentCinemaReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => staffService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortEntityNotFoundException<Staff>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedCinemaReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.Staff(x => x.DeletedAt = DateTime.UtcNow);
            await Context.Staffs.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => staffService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortInvalidOperationException>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => staffService.DeleteAsync(model.Id, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Staffs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.StaffRequestModel();

            //Act
            var act = await staffService.AddAsync(model, CancellationToken);

            // Assert
            var entity = Context.Staffs.Single(x => x.Id == act.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.StaffRequestModel();

            //Act
            Func<Task> act = () => staffService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PortEntityNotFoundException<Staff>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="Staff"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            var staff = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(staff);
            await UnitOfWork.SaveChangesAsync(CancellationToken);
            var model = TestDataGenerator.StaffRequestModel(x => x.Id = staff.Id);

            //Act
            Func<Task> act = () => staffService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.Staffs.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.FIO,
                    model.Post
                });
        }
    }
}
