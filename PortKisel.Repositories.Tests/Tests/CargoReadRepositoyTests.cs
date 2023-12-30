using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    /// <summary>
    /// Тесты для <see cref="ICargoReadRepository"/>
    /// </summary>
    public class CargoReadRepositoyTests : PortContextInMemory
    {
        private readonly ICargoReadRepository cargoReadRepository;

        public CargoReadRepositoyTests()
        {
            cargoReadRepository = new CargoReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список грузов
        /// </summary>
        [Fact]
        public async Task GetAllCargoEmpty()
        {
            //Act
            var result = await cargoReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка грузов
        /// </summary>
        [Fact]
        public async Task GetCargosValue()
        {
            //Arrange
            var target = TestDataGenerator.Cargo();
            await Context.Cargos.AddRangeAsync(target,
                TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение груза по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdCargoNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await cargoReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение груза по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdCargoValue()
        {
            //Arrange
            var target = TestDataGenerator.Cargo();
            await Context.Cargos.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка грузов по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsCargoEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await cargoReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка грузов по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsCargosValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Cargo();
            var target2 = TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Cargo();
            var target4 = TestDataGenerator.Cargo();
            await Context.Cargos.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск груза в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Cargo();
            await Context.Cargos.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск груза в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await cargoReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного груза в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Cargos.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await cargoReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
