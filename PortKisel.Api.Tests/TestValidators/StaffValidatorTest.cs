using FluentValidation.TestHelper;
using PortKisel.Api.ModelsRequest.Staff;
using PortKisel.Api.Validators.Staff;
using Xunit;

namespace PortKisel.Api.Tests.TestValidators
{
    public class StaffValidatorTest
    {
        private readonly CreateStaffRequestValidator validatorCreateRequest;
        private readonly EditStaffRequestValidator validatorRequest;

        public StaffValidatorTest()
        {
            validatorRequest = new EditStaffRequestValidator();
            validatorCreateRequest = new CreateStaffRequestValidator();
        }

        /// <summary>
        /// Тест на наличие ошибок
        /// </summary>
        [Fact]
        public void ValidatorEditRequestShouldError()
        {
            //Arrange
            var model = new EditStaffRequest();

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
            var model = TestDataGenerators.EditStaffRequest();

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
            var model = new CreateStaffRequest();

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
            var model = TestDataGenerators.CreateStaffRequest();

            // Act
            var result = validatorCreateRequest.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
