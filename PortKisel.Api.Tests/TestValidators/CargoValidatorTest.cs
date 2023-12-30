using FluentValidation.TestHelper;
using Moq;
using PortKisel.Api.ModelsRequest.Cargo;
using PortKisel.Api.Validators.Cargo;
using PortKisel.Repositories.Contracts.Interface;
using Xunit;

namespace PortKisel.Api.Tests.TestValidators
{
    public class CargoValidatorTest
    {
        private readonly CreateCargoRequestValidator validatorCreateRequest;
        private readonly EditCargoRequestValidator validatorRequest;

        private readonly Mock<ICompanyZakazchikReadRepository> companyZakazchikReadRepositoryMock;

        public CargoValidatorTest()
        {
            companyZakazchikReadRepositoryMock = new Mock<ICompanyZakazchikReadRepository>();
            validatorRequest = new EditCargoRequestValidator(
                companyZakazchikReadRepositoryMock.Object);
            validatorCreateRequest = new CreateCargoRequestValidator(
                companyZakazchikReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new EditCargoRequest();

            //Act
            var validation = await validatorRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerators.EditCargoRequest();

            companyZakazchikReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CompanyZakazchikId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var validation = await validatorRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateCargoRequest();

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerators.CreateCargoRequest();

            companyZakazchikReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CompanyZakazchikId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
