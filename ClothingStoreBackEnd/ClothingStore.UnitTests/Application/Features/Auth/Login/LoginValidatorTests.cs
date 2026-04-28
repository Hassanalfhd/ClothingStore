using ClothingStore.Application.Features.Auth.Login;
using FluentValidation.TestHelper;

namespace ClothingStore.UnitTests.Application.Features.Auth.Login
{
    public class LoginValidatorTests
    {
        private readonly LoginValidator _validator;


        public LoginValidatorTests()
        {
            _validator = new();
        }




        [Fact]
        public void Should_HaveError_When_Email_Is_Empty()
        {
            //1.Arrange 
            var command = CreateValidCommand();
            command.Email = string.Empty;

            //2.Act
            var result = _validator.TestValidate(command);

            //3.Assert 
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }


        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            // Arrange
            var command = CreateValidCommand();
            command.Email = "invalid-email";

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            // Arrange
            var command = CreateValidCommand();
            command.Password = string.Empty;

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            // Arrange
            var command = CreateValidCommand();

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }



        //Helper Methods
        private static LoginCommand CreateValidCommand()
        {
            return new LoginCommand
            (
             "test@example.com",
              "Password123!"
            );
        }


    }
}
