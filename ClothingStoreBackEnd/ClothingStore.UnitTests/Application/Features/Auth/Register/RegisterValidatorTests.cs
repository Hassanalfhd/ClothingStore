using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Auth.Register;
using FluentValidation.TestHelper;

namespace ClothingStore.UnitTests.Application.Features.Auth.Register
{
    public class RegisterValidatorTests
    {
        private readonly RegisterValidator _validator = new();

        [Fact]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = CreateValidCommand();
            command.Email = "";

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }


        [Fact]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var command = CreateValidCommand();
            command.Email = "invalid-email";

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            var command = CreateValidCommand();
            command.Password = "123";

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Fact]
        public void Should_Have_Error_When_FirstName_Is_Empty()
        {
            var command = CreateValidCommand();
            command.FirstName = "";

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.FirstName);
        }

        [Fact]
        public void Should_Not_Have_Any_Error_When_Command_Is_Valid()
        {
            var command = CreateValidCommand();

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }

        private static RegisterCommand CreateValidCommand()
        {
            return new RegisterCommand
            (
                "test@example.com",
                "Password123!",
                "Hassan",
                "Alfahd"
            );
        }
    }
}
