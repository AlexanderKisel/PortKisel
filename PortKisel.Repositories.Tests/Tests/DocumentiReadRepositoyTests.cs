using FluentAssertions;
using PortKisel.Context.Tests;
using PortKisel.Repositories.Contracts.Interface;
using PortKisel.Repositories.Implementations;
using Xunit;

namespace PortKisel.Repositories.Tests.Tests
{
    public class DocumentiReadRepositoyTests : PortContextInMemory
    {
        private readonly IDocumentiReadRepository documentiReadRepository;

        public DocumentiReadRepositoyTests()
        {
            documentiReadRepository = new DocumentiReadRepository(Reader);
        }

        /// <summary>
        /// Возвращает пустой список документов
        /// </summary>
        [Fact]
        public async Task GetAllDocumentiEmpty()
        {
            //Act
            var result = await documentiReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Возвращение списка документов
        /// </summary>
        [Fact]
        public async Task GetDocumentiValue()
        {
            //Arrange
            var target = TestDataGenerator.Documenti();
            await Context.Documentis.AddRangeAsync(target,
                TestDataGenerator.Documenti(x => x.DeletedAt = DateTimeOffset.UtcNow));
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiReadRepository.GetAllAsync(CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(1)
                .And.ContainSingle(x => x.Id == target.Id);
        }

        /// <summary>
        /// Получение документа по id возвращает null
        /// </summary>
        [Fact]
        public async Task GetByIdDocumentiNull()
        {
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result = await documentiReadRepository.GetByIdAsync(id, CancellationToken);

            //Assert
            result.Should().BeNull();
        }

        /// <summary>
        /// Получение документа по id возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdDocumentiValue()
        {
            //Arrange
            var target = TestDataGenerator.Documenti();
            await Context.Documentis.AddAsync(target);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiReadRepository.GetByIdAsync(target.Id, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEquivalentTo(target);
        }

        /// <summary>
        /// Получение списка документ по ids возвращает пустую коллекцию
        /// </summary>
        [Fact]
        public async Task GetByIdsDocumentiEmpty()
        {
            //Arrange
            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            //Act
            var result = await documentiReadRepository.GetByIdsAsync(new[] { id1, id2, id3 }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.BeEmpty();
        }

        /// <summary>
        /// Получение списка документов по ids возвращает данные
        /// </summary>
        [Fact]
        public async Task GetByIdsDocumentiValue()
        {
            //Arrange
            var target1 = TestDataGenerator.Documenti();
            var target2 = TestDataGenerator.Documenti(x => x.DeletedAt = DateTimeOffset.UtcNow);
            var target3 = TestDataGenerator.Documenti();
            var target4 = TestDataGenerator.Documenti();
            await Context.Documentis.AddRangeAsync(target1, target2, target3, target4);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiReadRepository.GetByIdsAsync(new[] { target1.Id, target2.Id, target4.Id }, CancellationToken);

            //Assert
            result.Should()
                .NotBeNull()
                .And.HaveCount(2)
                .And.ContainKey(target1.Id)
                .And.ContainKey(target4.Id);
        }

        /// <summary>
        /// Поиск документов в коллекции по id (true)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnTrue()
        {
            //Arrange
            var target1 = TestDataGenerator.Documenti();
            await Context.Documentis.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            // Assert
            result.Should().BeTrue();
        }

        /// <summary>
        /// Поиск документов в коллекции по id (false)
        /// </summary>
        [Fact]
        public async Task IsNotNullEntityReturnFalse()
        {
            //Arrange
            var target1 = Guid.NewGuid();

            //Act
            var result = await documentiReadRepository.IsNotNullAsync(target1, CancellationToken);

            // Assert
            result.Should().BeFalse();
        }

        /// <summary>
        /// Поиск удаленного документа в коллекции по id 
        /// </summary>
        [Fact]
        public async Task IsNotNullDeletedEntityReturnFalse()
        {
            //Arrange
            var target1 = TestDataGenerator.Documenti(x => x.DeletedAt = DateTimeOffset.UtcNow);
            await Context.Documentis.AddAsync(target1);
            await Context.SaveChangesAsync(CancellationToken);

            //Act
            var result = await documentiReadRepository.IsNotNullAsync(target1.Id, CancellationToken);

            //Assert
            result.Should().BeFalse();
        }
    }
}
