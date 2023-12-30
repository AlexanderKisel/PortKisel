using FluentValidation.TestHelper;
using PortKisel.Api.ModelsRequest.CompanyPer;
using PortKisel.Api.Validators.CompanyPer;
using Xunit;

namespace PortKisel.Api.Tests.TestValidators
{
    public class CompanyPerValidatorTest
    {
        private readonly CreateCompanyPerRequestValidator validatorCreateRequest;
        private readonly EditCompanyPerRequestValidator validatorRequest;

        public CompanyPerValidatorTest()
        {
            validatorRequest = new EditCompanyPerRequestValidator();
            validatorCreateRequest = new CreateCompanyPerRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorEditRequestShouldError()
        {
            //Arrange
            var model = new EditCompanyPerRequest();

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
            var model = TestDataGenerators.EditCompanyPerRequest();

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
            var model = new CreateCompanyPerRequest();

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
            var model = TestDataGenerators.CreateCompanyPerRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
