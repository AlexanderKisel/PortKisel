using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    public class CompanyPerReadRepositoyTests : PortContextInMemory
    {
        private readonly ICompanyPerReadRepository companyPerReadRepository;

        public CompanyPerReadRepositoyTests()
        {
            companyPerReadRepository = new CompanyPerReadRepository(Reader);
        }
        /// <summary>
        /// Возвращает пустой список компаний перевозчиков
        /// </summary>
        [Fact]
        public async Task GetAllCompanyPerEmpty()
        {
            //Act
            var result = await companyPerReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка компаний перевозчиков
        /// </summary>
        [Fact]
        public async Task GetCompanyPerValue()
        {
            //Arrange
            var target = TestDataGenerator.CompanyPer();
            await Context.CompanyPers.AddRangeAsync(target,
                TestDataGenerator.CompanyPer(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyPerReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение компании перевозчика по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdCompanyPerNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await companyPerReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение компании перевозчика по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdCompanyPerValue()
        {
            //Arrange
            var target = TestDataGenerator.CompanyPer();
            await Context.CompanyPers.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyPerReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка компаний перевозчиков по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsCompanyPerEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await companyPerReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка компаний перевозчиков по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsCompanyPerValue()
        {
            //Arrange
            var target1 = TestDataGenerator.CompanyPer();
            var target2 = TestDataGenerator.CompanyPer(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.CompanyPer();
            var target4 = TestDataGenerator.CompanyPer();
            await Context.CompanyPers.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyPerReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск компаний перевозчиков в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.CompanyPer();
            await Context.CompanyPers.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyPerReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск компаний перевозчиков в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await companyPerReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленной компании перевозчика в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Cargo(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Cargos.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyPerReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
