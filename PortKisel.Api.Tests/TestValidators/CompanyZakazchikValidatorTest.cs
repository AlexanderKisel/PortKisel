using FluentValidation.TestHelper;
using PortKisel.Api.ModelsRequest.CompanyZakazchik;
using PortKisel.Api.Validators.CompanyZakazchik;
using Xunit;

namespace PortKisel.Api.Tests.TestValidators
{
    public class CompanyZakazchikValidatorTest
    {
        private readonly CreateCompanyZakazchikRequestValidator validatorCreateRequest;
        private readonly EditCompanyZakazchikRequestValidator validatorRequest;

        public CompanyZakazchikValidatorTest()
        {
            validatorRequest = new EditCompanyZakazchikRequestValidator();
            validatorCreateRequest = new CreateCompanyZakazchikRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorEditRequestShouldError()
        {
            //Arrange
            var model = new EditCompanyZakazhikRequest();

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorEditRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerators.EditCompanyZakazhikRequest();

            // Act
            var result = validatorRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldError()
        {
            //Arrange
            var model = new CreateCompanyZakazhikRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldHaveAnyValidationError();
        }

        /// <summary>
        /// Тест на отсутствие ошибок
        /// </summary>
        [Fact]
        public void ValidatorCreateRequestShouldSuccess()
        {
            //Arrange
            var model = TestDataGenerators.CreateCompanyZakazhikRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
