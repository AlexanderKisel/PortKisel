using AutoMapper;
using FluentAssertions;
using PortKisel.Api.Infrastructures;
using PortKisel.Common.Entity.InterfaceDB;
using PortKisel.Context.Contracts.Models;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Implementations;
using PortKisel.Services.AutoMappers;
using PortKisel.Services.Contracts.Exceptions;
using PortKisel.Services.Contracts.Interface;
using PortKisel.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PortKisel.Services.Tests.Tests
{
    public class CompanyZakazchikServiceTests : PortContextInMemory
    {
        public readonly ICompanyZakazchikService companyZakazchikService;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="CompanyZakazchikServiceTests"/>
        /// </summary>
        public CompanyZakazchikServiceTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ServiceProfile());
            });
            companyZakazchikService = new CompanyZakazchikService(
                new CompanyZakazchikReadRepository(Reader),
                config.CreateMapper(),
                UnitOfWork,
                new CompanyZakazchikWriteRepository(WriterContext));
        }

        /// <summary>
        /// Получение компаний заказчика по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await companyZakazchikService.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение компаний заказчиков по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdShouldReturnValue()
        {
            //Arrange
            var target = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikService.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(new
                {
                    target.Id,
                    target.Name,
                    target.Description
                });
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{CompanyZakazchik}"/> по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnEmpty()
        {
            // Act
            var result = await companyZakazchikService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение <see cref="IEnumerable{CompanyZakazchik}"/> по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetAllShouldReturnValues()
        {
            //Arrange
            var companyZakazchik = TestDataGenerator.CompanyZakazchik(x => { x.Id = Guid.NewGuid(); });

            await Context.CompanyZakazchiks.AddRangeAsync(companyZakazchik,
                TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            var result = await companyZakazchikService.GetAllAsync(CancellationToken);

            // Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == companyZakazchik.Id);
        }

        /// <summary>
        /// Удаление несуществующего <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task DeletingNonExistentReturnExсeption()
        {
            //Arrange
            var id = Guid.NewGuid();

            // Act
            Func<Task> result = () => companyZakazchikService.DeleteAsync(id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortEntityNotFoundException<CompanyZakazchik>>()
               .WithMessage($"*{id}*");
        }

        /// <summary>
        /// Удаление удаленного <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task DeletingDeletedReturnExсeption()
        {
            //Arrange
            var model = TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTime.UtcNow);
            await Context.CompanyZakazchiks.AddAsync(model);
            await Context.SaveChangesAsync(CancellationToken);

            // Act
            Func<Task> result = () => companyZakazchikService.DeleteAsync(model.Id, CancellationToken);

            // Assert
            await result.Should().ThrowAsync<PortInvalidOperationException>()
               .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Удаление <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task DeleteShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddAsync(model);
            await UnitOfWork.SaveChangesAsync(CancellationToken);

            //Act
            Func<Task> act = () => companyZakazchikService.DeleteAsync(model.Id, CancellationToken);

            //Assert
            await act.Should().NotThrowAsync();
            var entity = Context.CompanyZakazchiks.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().NotBeNull();
        }

        /// <summary>
        /// Добавление <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task AddShouldWork()
        {
            //Arrange
            var model = TestDataGenerator.CompanyZakazchikRequestModel();

            //Act
            var act = await companyZakazchikService.AddAsync(model, CancellationToken);

            // Assert
            var entity = Context.CompanyZakazchiks.Single(x => x.Id == act.Id);
            entity.Should().NotBeNull();
            entity.DeletedAt.Should().BeNull();
        }

        /// <summary>
        /// Изменение несуществующего <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task EditShouldNotFoundException()
        {
            //Arrange
            var model = TestDataGenerator.CompanyZakazchikRequestModel();

            //Act
            Func<Task> act = () => companyZakazchikService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().ThrowAsync<PortEntityNotFoundException<CompanyPer>>()
                .WithMessage($"*{model.Id}*");
        }

        /// <summary>
        /// Изменение <see cref="CompanyZakazchik"/>
        /// </summary>
        [Fact]
        public async Task EditShouldWork()
        {
            var companyZakazchik = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddAsync(companyZakazchik);
            await UnitOfWork.SaveChangesAsync(CancellationToken);
            var model = TestDataGenerator.CompanyZakazchikRequestModel(x => x.Id = companyZakazchik.Id);

            //Act
            Func<Task> act = () => companyZakazchikService.UpdateAsync(model, CancellationToken);

            // Assert
            await act.Should().NotThrowAsync();
            var entity = Context.CompanyZakazchiks.Single(x => x.Id == model.Id);
            entity.Should().NotBeNull()
                .And
                .BeEquivalentTo(new
                {
                    model.Id,
                    model.Name,
                    model.Description
                });
        }
    }
}
