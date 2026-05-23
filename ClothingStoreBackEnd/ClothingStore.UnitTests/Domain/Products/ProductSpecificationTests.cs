using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;

namespace ClothingStore.UnitTests.Domain.Products
{
    public class ProductSpecificationTests
    {

        [Fact]
        public void Should_Create_ProductSpecification_Correctly()
        {

            //Arrange
            var spec = new ProductSpecification(1, "Brand", "Nike");

            //Assert 
            spec.Key.Should().Be("Brand");
            spec.Value.Should().Be("Nike");
        }


        [Fact]
        public void Should_Throw_Exception_When_Key_Is_Empty()
        {
            // Arrange 
            Action act = () =>
            {
                new ProductSpecification(1, "", "Nike");
            };


            //Assert 
            act.Should().Throw<ArgumentException>();


        }


        [Fact]
        public void Should_Throw_Exception_When_Value_Is_Empty()
        {
            // Arrange 
            Action act = () =>
            {
                new ProductSpecification(1, "Brand", "");
            };


            //Assert 
            act.Should().Throw<ArgumentException>();


        }

        [Fact]
        public void Should_Add_Specification_To_Product()
        {

            // Arrange 
            var product = new Product("T-Shirt",
        "Cotton shirt",
        new Money(100, "USD"),
         true, 1, 1, 1);

            //Act 
            product.AddSepecifiaction("Brand", "Nike");

            //Assert 
            product.Specifications.Should().HaveCount(1);

            product.Specifications.First().Key.Should().Be("Brand");
            product.Specifications.First().Value.Should().Be("Nike");
        }


        [Fact]
        public void Should_Remove_Specification_From_Product()
        {
            //Arrange 
            var product = new Product("T-Shirt",
        "Cotton shirt",
        new Money(100, "USD"),
         true, 1, 1, 1);

            product.AddSepecifiaction("Brand", "Nike");

            //Act 
            product.RemoveSpecification("Brand");

            //Assert
            product.Specifications.Should().BeEmpty();
        }

        [Fact]
        public void Should_Clear_Specifications_From_Product()
        {
            //Arrange
            var product = new Product("T-Shirt",
      "Cotton shirt",
      new Money(100, "USD"),
       true, 1, 1, 1);

            product.AddSepecifiaction("Brand", "Nike");
            product.AddSepecifiaction("Color", "Black");

            //Act 
            product.ClearSpecifications();

            //Assert
            product.Specifications.Should().BeEmpty();
        }

    }
}
