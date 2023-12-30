using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    public class VesselReadRepositoyTests : PortContextInMemory
    {
        private readonly IVesselReadRepository vesselReadRepository;

        public VesselReadRepositoyTests()
        {
            vesselReadRepository = new VesselReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список суден
        /// </summary>
        [Fact]
        public async Task GetAllVesselEmpty()
        {
            //Act
            var result = await vesselReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка суден
        /// </summary>
        [Fact]
        public async Task GetVesselValue()
        {
            //Arrange
            var target = TestDataGenerator.Vessel();
            await Context.Vessels.AddRangeAsync(target,
                TestDataGenerator.Vessel(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение судна по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdStaffNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await vesselReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение судна по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdVesselValue()
        {
            //Arrange
            var target = TestDataGenerator.Vessel();
            await Context.Vessels.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка суден по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsVesselEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await vesselReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка суден по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsVesselValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Vessel();
            var target2 = TestDataGenerator.Vessel(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Vessel();
            var target4 = TestDataGenerator.Vessel();
            await Context.Vessels.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск суден в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Vessel();
            await Context.Vessels.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск суден в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await vesselReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного судна в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Vessel(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Vessels.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await vesselReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
