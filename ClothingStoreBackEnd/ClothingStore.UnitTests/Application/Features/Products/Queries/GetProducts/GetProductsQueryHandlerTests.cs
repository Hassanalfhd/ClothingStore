using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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
        private readonly Mock<IProductReadRepos> _repoMock;
        private readonly GetProductsQueryHandler _handler;

        public GetProductsQueryHandlerTests()
        {
            _repoMock = new Mock<IProductReadRepos>();
            _handler = new GetProductsQueryHandler(_repoMock.Object);


        }

        private List<Product> CreateProducts()
        {
            var products = new List<Product>()
            {
                new Product("Nike T-Shirt","Cotton shirt", new Money(300, "USD"),true, 1, 1, 1),
                new Product("Shoes","Sport", new Money(100, "USD"),true, 1, 1, 1),
                new Product("T-Shirt","Wool shirt", new Money(200, "USD"),true, 1, 1, 1)
            };

            return products;
        }


        [Fact]
        public async Task Should_Return_All_Products()
        {
            var products = CreateProducts();

            
            _repoMock
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
            
            var result = await _handler.Handle(
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
            
            _repoMock
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
            
            var result = await _handler.Handle(
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

            _repoMock
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
            
            var result = await _handler.Handle(
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


        [Fact]
        public async Task Should_Return_Correct_Page_Of_Products()
        {
            //Arrange 
            var products = Enumerable.Range(1, 10)
                .Select(i => new Product(
                    $"Product {i}", 
                    "Desc", 
                    new Money(100, "USD"), 
                    true, 1, 1, 1))
                .ToList();

            _repoMock
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
                    .ReturnsAsync((string s, long? c,
                    ProductStatus? st, decimal? min, decimal? max,
                    int pageNumber, int pageSize,
                    List<string>? specs, ProductSortBy sort, CancellationToken ct) =>
                    {
                        var items = products
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .Select(p => new ProductListDto
                            {
                                Name = p.Name
                            })
                            .ToList();
                        
                        return new PagedResult<ProductListDto>
                        {
                            Items = items,
                            TotalCount = products.Count
                        };
                    });

            //Act 
            
            var result = await _handler.Handle(new GetProductsQuery
            {
                PageNumber = 2,
                PageSize = 5
            }, CancellationToken.None);


            //Assert 
            result.Items.Should().HaveCount(5);

            result.Items.First().Name.Should().Be("Product 6");

            result.Items.Last().Name.Should().Be("Product 10");
        }


        [Fact]
        public async Task Should_Sort_Products_By_Price_Ascending()
        {
            //Arrange
            var products = new List<ProductListDto>
                {
                    new ProductListDto
                    {
                        Name = "Shoes",
                        Price = 300
                    },

                    new ProductListDto
                    {
                        Name = "T-Shirt",
                        Price = 100
                    },

                    new ProductListDto
                    {
                        Name = "Watch",
                        Price = 200
                    }
                };
                            _repoMock
                .Setup(x => x.GetProductsAsync(
                     It.IsAny<string>(),
                    It.IsAny<long?>(),
                    It.IsAny<ProductStatus?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<decimal?>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<List<string>>(),
                    ProductSortBy.PriceAsc,
                    It.IsAny<CancellationToken>()
                    ))
                .ReturnsAsync(new PagedResult<ProductListDto>
                {
                    Items = products
                        .OrderBy(x => x.Price)
                        .ToList(),

                    TotalCount = products.Count
                });


            //Act 
            var result = await _handler.Handle(
                new GetProductsQuery
                {
                    SortBy = ProductSortBy.PriceAsc,
                    PageNumber = 1,
                    PageSize = 10
                }, CancellationToken.None);

            //Assert 
            result.Items.First().Price.Should().Be(100);
            result.Items.Last().Price.Should().Be(300);


        }


        [Fact]

        public async Task Should_Apply_Filtering_Sorting_And_Pagination_Correctly()
        {
            //Arrange
            var products = new List<ProductListDto>
            {
                new ProductListDto
                {
                    Name = "Nike Shoes",
                    Price = 150
                },

                new ProductListDto
                {
                    Name = "Nike Air",
                    Price = 300
                }
            };



            _repoMock
                .Setup(x => x.GetProductsAsync(
                    "Nike",
                    1,
                    ProductStatus.Active,
                    100,
                    500,
                    1,
                    5,
                    It.Is<List<string>>(s => s != null && s.Contains("Brand:Nike")),
                    ProductSortBy.PriceAsc,
                    It.IsAny<CancellationToken>()
                    ))
                .ReturnsAsync(new PagedResult<ProductListDto>
                {
                    Items = products.OrderBy(x => x.Price).ToList(),
                    TotalCount = 2
                });

            //Act 

            var result = await _handler.Handle(
                new GetProductsQuery
                {
                    Search = "Nike",
                    CategoryId = 1,
                    Status = ProductStatus.Active,
                    MinPrice = 100,
                    MaxPrice = 500,
                    PageNumber = 1,
                    PageSize = 5,
                    SortBy = ProductSortBy.PriceAsc,

                    Specifications = new List<string>
                    {
                            "Brand:Nike"
                    }
                },
                CancellationToken.None);


            //Assert 
            result.Items.Should().HaveCount(2);
            result.Items.First().Price.Should().Be(150);
            result.Items.Last().Price.Should().Be(300);

        }

        [Fact]
        public async Task Should_Filter_By_Category_Only()
        {
            var repoMock = new Mock<IProductReadRepos>();
            var handler = new GetProductsQueryHandler(repoMock.Object);

            repoMock.Setup(x => x.GetProductsAsync(
                It.IsAny<string>(),
                1,
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
            new ProductListDto { Name = "Product A" }
                },
                TotalCount = 1
            });

            var result = await handler.Handle(new GetProductsQuery
            {
                CategoryId = 1,
                PageNumber = 1,
                PageSize = 10
            }, CancellationToken.None);

            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task Should_Filter_By_Status_Only()
        {
            var repoMock = new Mock<IProductReadRepos>();
            var handler = new GetProductsQueryHandler(repoMock.Object);

            repoMock.Setup(x => x.GetProductsAsync(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                ProductStatus.Active,
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
            new ProductListDto { Name = "Product B" }
                },
                TotalCount = 1
            });

            var result = await handler.Handle(new GetProductsQuery
            {
                Status = ProductStatus.Active,
                PageNumber = 1,
                PageSize = 10
            }, CancellationToken.None);

            result.Items.Should().HaveCount(1);
        }


        [Fact]
        public async Task Should_Filter_By_Price_Range_Only()
        {
            var repoMock = new Mock<IProductReadRepos>();
            var handler = new GetProductsQueryHandler(repoMock.Object);

            repoMock.Setup(x => x.GetProductsAsync(
                It.IsAny<string>(),
                It.IsAny<long?>(),
                It.IsAny<ProductStatus?>(),
                100,
                500,
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<List<string>>(),
                It.IsAny<ProductSortBy>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new PagedResult<ProductListDto>
            {
                Items = new List<ProductListDto>
                {
            new ProductListDto { Name = "Product C" }
                },
                TotalCount = 1
            });

            var result = await handler.Handle(new GetProductsQuery
            {
                MinPrice = 100,
                MaxPrice = 500,
                PageNumber = 1,
                PageSize = 10
            }, CancellationToken.None);

            result.Items.Should().HaveCount(1);
        }
    }
}
