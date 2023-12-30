using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    public class CompanyZakazchikReadRepositoyTests : PortContextInMemory
    {
        private readonly ICompanyZakazchikReadRepository companyZakazchikReadRepository;

        public CompanyZakazchikReadRepositoyTests()
        {
            companyZakazchikReadRepository = new CompanyZakazchikReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список компаний заказчиков
        /// </summary>
        [Fact]
        public async Task GetAllCompanyZakazchikEmpty()
        {
            //Act
            var result = await companyZakazchikReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка компаний заказчиков
        /// </summary>
        [Fact]
        public async Task GetCompanyZakazchikValue()
        {
            //Arrange
            var target = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddRangeAsync(target,
                TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение компании заказчика по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdCompanyZakazchikNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await companyZakazchikReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение компании заказчика по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdCompanyZakazchikValue()
        {
            //Arrange
            var target = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка компаний заказчиков по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsCompanyZakazchikEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await companyZakazchikReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка компаний заказчиков по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsCompanyZakazchikValue()
        {
            //Arrange
            var target1 = TestDataGenerator.CompanyZakazchik();
            var target2 = TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.CompanyZakazchik();
            var target4 = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск компаний заказчиков в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.CompanyZakazchik();
            await Context.CompanyZakazchiks.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск компаний заказчиков в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await companyZakazchikReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленной компании заказчика в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.CompanyZakazchik(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.CompanyZakazchiks.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await companyZakazchikReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
