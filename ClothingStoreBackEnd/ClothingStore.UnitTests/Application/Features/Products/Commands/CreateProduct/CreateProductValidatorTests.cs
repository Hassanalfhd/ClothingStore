using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using FluentValidation.TestHelper;

namespace ClothingStore.UnitTests.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductValidatorTests
    {

        private readonly CreateProductValidator _validator;

        public CreateProductValidatorTests()
        {
            _validator = new();
        }


        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new  CreateProductCommand("Nike T-Shirt", "none", 10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            var result = _validator.TestValidate(command);

            result.ShouldNotHaveAnyValidationErrors();
        }



        [Fact]
        public void Should_Have_Error_When_Price_Is_Less_Than_Zero()
        {
            var command = new CreateProductCommand("Nike T-Shirt", "none", -10, "YR", true, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x=>x.Price);

        }




    }

}
