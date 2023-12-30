using FluentValidation.TestHelper;
using Moq;
using PortKisel.Api.ModelsRequest.Vessel;
using PortKisel.Api.Validators.Vessel;
using PortKisel.Repositories.Contracts.Interface;
using Xunit;

namespace PortKisel.Api.Tests.TestValidators
{
    public class VesselValidatorTest
    {
        private readonly CreateVesselRequestValidator validatorCreateRequest;
        private readonly EditVesselRequestValidator validatorRequest;

        private readonly Mock<ICompanyPerReadRepository> companyPerReadRepositoryMock;

        public VesselValidatorTest()
        {
            companyPerReadRepositoryMock = new Mock<ICompanyPerReadRepository>();
            validatorRequest = new EditVesselRequestValidator(
                companyPerReadRepositoryMock.Object);
            validatorCreateRequest = new CreateVesselRequestValidator(
                companyPerReadRepositoryMock.Object);
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new EditVesselRequest();

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
            var model = TestDataGenerators.EditVesselRequest();

            companyPerReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CompanyPerId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

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
            var model = new CreateVesselRequest();

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
            var model = TestDataGenerators.CreateVesselRequest();

            companyPerReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CompanyPerId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
