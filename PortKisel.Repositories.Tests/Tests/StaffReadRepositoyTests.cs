﻿using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    public class StaffReadRepositoyTests : PortContextInMemory
    {
        private readonly IStaffReadRepository staffReadRepository;

        public StaffReadRepositoyTests()
        {
            staffReadRepository = new StaffReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список сотрудников
        /// </summary>
        [Fact]
        public async Task GetAllStaffEmpty()
        {
            //Act
            var result = await staffReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка сотрудников
        /// </summary>
        [Fact]
        public async Task GetStaffValue()
        {
            //Arrange
            var target = TestDataGenerator.Staff();
            await Context.Staffs.AddRangeAsync(target,
                TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение сотрудника по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdStaffNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await staffReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение сотрудника по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdStaffValue()
        {
            //Arrange
            var target = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка сотрудников по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsStaffEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await staffReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка сотрудников по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsStaffValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Staff();
            var target2 = TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Staff();
            var target4 = TestDataGenerator.Staff();
            await Context.Staffs.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск сотрудников в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Staff();
            await Context.Staffs.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск сотрудников в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await staffReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного сотрудника в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Staff(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Staffs.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await staffReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
