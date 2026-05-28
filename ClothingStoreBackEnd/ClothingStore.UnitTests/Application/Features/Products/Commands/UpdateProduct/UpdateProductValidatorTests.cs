using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Application.Features.Products.Commands.UpdateProduct;
using FluentValidation.TestHelper;

namespace ClothingStore.UnitTests.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductValidatorTests
    {

        private readonly UpdateProductValidator _validator;

        public UpdateProductValidatorTests()
        {
            _validator = new();
        }


        [Fact]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = UpdateProductCommand(10);

            var result = _validator.TestValidate(command);
            
            result.ShouldNotHaveAnyValidationErrors();

        }


        [Fact]
        public void Should_Have_Error_When_Price_Is_Less_Than_Zero()
        {

            var command = UpdateProductCommand(-10);

            
            var result = _validator.TestValidate(command);

            result.ShouldHaveValidationErrorFor(x => x.Price);

        }

        private static UpdateProductCommand UpdateProductCommand(decimal price)
        {
            return new UpdateProductCommand(Guid.NewGuid(), "Nike T-Shirt", "none", price, "YR", Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null);

        }


    }
}
