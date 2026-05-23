using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Queries.GetProducts;
using ClothingStore.Application.Interfaces.Repositories;
using ClothingStore.Domain.Common;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace ClothingStore.UnitTests.Application.Features.Products.Queries.GetProducts
{
    public class GetProductsQueryHandlerTests
    {

        private List<Product> CreateProducts()
        {
            var products = new List<Product>()
            {
                new Product("Nike T-Shirt","Cotton shirt", new Money(100, "USD"),true, 1, 1, 1),
                new Product("Shoes","Sport", new Money(100, "USD"),true, 1, 1, 1),
                new Product("T-Shirt","Wool shirt", new Money(100, "USD"),true, 1, 1, 1)
            };

            return products;
        }


        [Fact]
        public async Task Should_Return_All_Products()
        {
            var products = CreateProducts();

            var repoMock = new Mock<IProductReadRepos>();

           
            repoMock
                   .Setup(x => x.GetProductsAsync(
                         It.IsAny<string>(),
                         It.IsAny<long?>(),
                         It.IsAny<ProductStatus?>(),
                         It.IsAny<decimal?>(),
                         It.IsAny<decimal?>(),
                         It.IsAny<int>(),
                         It.IsAny<int>(),
                         It.IsAny<List<string>>(),
                         It.IsAny<ProductSortBy>(),
                         It.IsAny<CancellationToken>()))
                     .ReturnsAsync(new PagedResult<ProductListDto>
                     {
                         Items = products.Select(p => new ProductListDto
                         {
                             Name = p.Name
                         }).ToList(),
                         TotalCount = 2
                     });
            //Act 
            var handler = new GetProductsQueryHandler(repoMock.Object);

            var result = await handler.Handle(
                new GetProductsQuery
                {
                    PageNumber = 1,
                    PageSize = 10,
                },
                CancellationToken.None);


            //Assert
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(3);
        }

        [Fact]
        public async Task Should_Filter_By_Specifications()
        {
            //Arrange
            var products = CreateProducts();
            var repoMock = new Mock<IProductReadRepos>();

            repoMock
                .Setup(x => x.GetProductsAsync(
                    It.IsAny<string>(),
                    It.IsAny<long?>(),
                    It.IsAny<ProductStatus?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.Is<List<string>>(s =>
                        s != null &&
                        s.Contains("Brand:Nike")),
                    It.IsAny<ProductSortBy>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PagedResult<ProductListDto>
                {
                    Items = new List<ProductListDto>
                    {
            new ProductListDto { Name = "Nike T-Shirt" }
                    },
                    TotalCount = 1
                });

            //Act
            var handler = new GetProductsQueryHandler(repoMock.Object);

            var result = await handler.Handle(
                new GetProductsQuery
                {
                    Specifications = new List<string>
                    {
                        "Brand:Nike"
                    },
                    PageNumber = 1,
                    PageSize = 10
                },
                CancellationToken.None);

            //Assert 
            result.Should().NotBeNull();
            result.Items.Should().HaveCount(1);
            result.Items.First().Name.Should().Be("Nike T-Shirt");
        }


        [Fact]
        public async Task Should_Filter_By_Search()
        {
            //Arrange
            var products = CreateProducts();
            var repoMock = new Mock<IProductReadRepos>();

            repoMock
                .Setup(x => x.GetProductsAsync(
                    "Nike",
                    It.IsAny<long?>(),
                    It.IsAny<ProductStatus?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<List<string>>(),
                    It.IsAny<ProductSortBy>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(new PagedResult<ProductListDto>
                {
                    Items = new List<ProductListDto>
                    {
            new ProductListDto { Name = "Nike T-Shirt" }
                    },
                    TotalCount = 1
                });

            //Act 
            var handler = new GetProductsQueryHandler(repoMock.Object);

            var result = await handler.Handle(
                new GetProductsQuery
                {
                    Search = "Nike",
                    PageNumber = 1,
                    PageSize = 10
                },
                CancellationToken.None);


            //Assert 
            result.Items.Should().HaveCount(1);
            result.Items.First().Name.Should().Contain("Nike");
        }

    }
}
