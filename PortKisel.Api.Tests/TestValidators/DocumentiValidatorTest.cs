using Moq;
using PortKisel.Api.Validators.Documenti;
using PortKisel.Repositories.Contracts.Interface;
using FluentValidation.TestHelper;
using Xunit;
using PortKisel.Api.ModelsRequest.Documenti;

namespace PortKisel.Api.Tests.TestValidators
{
    public class DocumentiValidatorTest
    {
        private readonly CreateDocumentiRequestValidator validatorCreateRequest;
        private readonly EditDocumentiRequestValidator validatorRequest;

        private readonly Mock<IVesselReadRepository> vesselreadRepositoryMock;
        private readonly Mock<ICargoReadRepository> cargoReadRepositoryMock;
        private readonly Mock<IStaffReadRepository> staffReadRepositoryMock;

        public DocumentiValidatorTest()
        {
            vesselreadRepositoryMock = new Mock<IVesselReadRepository>();
            cargoReadRepositoryMock = new Mock<ICargoReadRepository>();
            staffReadRepositoryMock = new Mock<IStaffReadRepository>();

            validatorCreateRequest = new CreateDocumentiRequestValidator(
                cargoReadRepositoryMock.Object,
                vesselreadRepositoryMock.Object,
                staffReadRepositoryMock.Object
                );
            validatorRequest = new EditDocumentiRequestValidator(
                cargoReadRepositoryMock.Object,
                vesselreadRepositoryMock.Object,
                staffReadRepositoryMock.Object
                );
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public async void ValidatorRequestShouldError()
        {
            //Arrange
            var model = new EditDocumentiRequest();

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
            var model = TestDataGenerators.EditDocumentiRequest();

            vesselreadRepositoryMock.Setup(x => x.AnyByIdAsync(model.VesselId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
            cargoReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CargoId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
            staffReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.StaffId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

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
            var model = new CreateDocumentiRequest();

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
            var model = TestDataGenerators.CreateDocumentiRequest();

            vesselreadRepositoryMock.Setup(x => x.AnyByIdAsync(model.VesselId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
            cargoReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.CargoId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
            staffReadRepositoryMock.Setup(x => x.AnyByIdAsync(model.StaffId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            //Act
            var validation = await validatorCreateRequest.TestValidateAsync(model);

            //Assert
            validation.ShouldNotHaveAnyValidationErrors();
        }
    }
}
