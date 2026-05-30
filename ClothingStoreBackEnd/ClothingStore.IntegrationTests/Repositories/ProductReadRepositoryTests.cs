using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.Enums;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.IntegrationTests.Common;
using ClothingStore.IntegrationTests.Seeding;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;

namespace ClothingStore.IntegrationTests.Repositories
{
    public class ProductReadRepositoryTests: IDisposable
    {

        private readonly ApplicationDbContext _context;
        private readonly ProductReadRepo _repository;

        public ProductReadRepositoryTests()
        {
            _context = TestDbContextFactory.Create();
            TestDataSeeder.SeedProductsData(_context);

            _repository = new ProductReadRepo(_context);
        }



        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [Fact]
        public async Task Should_Return_Only_Active_Products()
        {
            // Act
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                null,
                1,
                10,
                null,
                ProductSortBy.NameAsc,
                CancellationToken.None);

            // Assert
            result.Items.Should().HaveCount(5);
        

        }

        [Fact]
        public async Task Should_Filter_Products_By_Search_Term()
        {
            var result = await _repository.GetProductsAsync(
                "Nike",
                null,
                null,
                null,
                null,
                1,
                10,
                null,
                ProductSortBy.Newest,
                CancellationToken.None);


            result.Items.Should().HaveCount(2);

            result.Items.First().Name.Should().Contain("Nike");
            result.Items.Last().Name.Should().Contain("Nike");


        }


        [Fact]
        public async Task Should_Return_Empty_When_Search_Term_Not_Found()
        {
            var result = await _repository.GetProductsAsync(
                search: "Samsung",
                categoryId: null,
                status: null,
                minPrice: null,
                maxPrice: null,
                pageNumber: 1,
                pageSize: 10,
                specifications: null,
                sortBy: ProductSortBy.NameAsc,
                cancellationToken: CancellationToken.None);

            result.Items.Should().BeEmpty();
        }


        [Fact]
        public async Task Should_Filter_Products_By_Category()
        {
            var categoryId = _context.Categories
                .First(x => x.Name == "Sport")
                .Id;

            var result = await _repository.GetProductsAsync(
                null,
                categoryId,
                null,
                null,
                null,
                1,
                10,
                null,
                ProductSortBy.Newest,
                CancellationToken.None);

            result.Items.Should().HaveCount(2);
            result.Items.Should().OnlyContain(x =>
              x.CategoryName == "Sport");
        }

        [Fact]
        public async Task Should_Filter_Products_By_MinPrice()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                100,
                null,
                1,
                10,
                null,
                ProductSortBy.Newest,
                CancellationToken.None);

            result.Items.Should().HaveCount(3);

            result.Items.Should().OnlyContain(x =>
                x.Price >= 100);
        }

        [Fact]
        public async Task Should_Filter_Products_By_MaxPrice()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                70,
                1,
                10,
                null,
                ProductSortBy.Newest,
                CancellationToken.None);

            result.Items.Should().HaveCount(2);

            result.Items.Should().OnlyContain(x =>
                x.Price <= 70);
        }


        [Fact]
        public async Task Should_Filter_Products_By_Price_Range()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                50,
                200,
                1,
                10,
                null,
                ProductSortBy.Newest,
                CancellationToken.None);

            result.Items.Should().HaveCount(3);

            result.Items.Should().OnlyContain(x =>
                x.Price >= 50 &&
                x.Price <= 200);

        }

        [Fact]
        public async Task Should_Filter_By_Brand_Nike()
        {
            var result = await _repository.GetProductsAsync(
                search: null,
                categoryId: null,
                status: null,
                minPrice: null,
                maxPrice: null,
                pageNumber: 1,
                pageSize: 10,
                specifications: new List<string>
                {
                "Brand:Nike"
                },
                sortBy: ProductSortBy.NameAsc,
                cancellationToken: CancellationToken.None);

            result.Items.Should().HaveCount(2);

            result.Items.Should().Contain(x =>
                x.Name == "Nike Air Zoom");

            result.Items.Should().Contain(x =>
                x.Name == "Nike Training Shirt");
        }

        [Fact]
        public async Task Should_Filter_By_Multiple_Specifications()
        {
            var result = await _repository.GetProductsAsync(
                search: null,
                categoryId: null,
                status: null,
                minPrice: null,
                maxPrice: null,
                pageNumber: 1,
                pageSize: 10,
                specifications: new List<string>
                {
                "Brand:Nike",
                "Size:M"
                },
                sortBy: ProductSortBy.NameAsc,
                cancellationToken: CancellationToken.None);

            result.Items.Should().HaveCount(1);

            result.Items.First().Name
                .Should()
                .Be("Nike Air Zoom");
        }

        
        [Fact]
        public async Task Should_Return_First_Page()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                null,
                1,
                2,
                null,
                ProductSortBy.NameAsc,
                CancellationToken.None);

            result.Items.Should().HaveCount(2);
        }

        [Fact]
        public async Task Should_Return_Last_Page_With_Remaining_Items()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                null,
                3,
                2,
                null,
                ProductSortBy.NameAsc,
                CancellationToken.None);

            result.Items.Should().HaveCount(1);
        }

        [Fact]
        public async Task Should_Return_Correct_TotalCount()
        {
            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                null,
                1,
                2,
                null,
                ProductSortBy.NameAsc,
                CancellationToken.None);

            result.TotalCount.Should().Be(5);
        }


        [Fact]
        public async Task Should_Return_All_Seeded_Products()
        {

            var result = await _repository.GetProductsAsync(
                null,
                null,
                null,
                null,
                null,
                1,
                5,
                null,
                ProductSortBy.NameAsc,
                CancellationToken.None);

            result.Items.Should().HaveCount(5);
        }


        [Fact]
        public async Task Should_Apply_Pagination_Correctly()
        {
                var result = await _repository.GetProductsAsync(
                    null,
                    null,
                    null,
                    null,
                    null,
                    1,
                    3,
                    null,
                    ProductSortBy.NameAsc,
                    CancellationToken.None);

                result.Items.Should().HaveCount(3);

        }

        [Fact]
        public async Task Should_Return_Empty_When_Page_Exceeds_Data()
        {
                var result = await _repository.GetProductsAsync(
                          null,
                          null,
                          null,
                          null,
                          null,
                          10,
                          5,
                          null,
                          ProductSortBy.NameAsc,
                          CancellationToken.None);


                result.Items.Should().BeEmpty();

        }

        [Fact]
        public async Task Should_Handle_PageSize_Larger_Than_Data()
        {
            var result = await _repository.GetProductsAsync(
                      null,
                      null,
                      null,
                      null,
                      null,
                      1,
                      20,
                      null,
                      ProductSortBy.NameAsc,
                      CancellationToken.None);


            result.Items.Should().HaveCount(5);
        }


        [Fact]
        public async Task Should_Return_Empty_When_No_Data()
        {
                // Arrange
                using var context = TestDbContextFactory.Create();

                var repository = new ProductReadRepo(context);

                // Act
                var result = await repository.GetProductsAsync(
                    null,
                    null,
                    null,
                    null,
                    null,
                    1,
                    10,
                    null,
                    ProductSortBy.Newest,
                    CancellationToken.None);


                // Assert
                result.Items.Should().HaveCount(0);

                result.TotalCount.Should().Be(0);
        }


        [Fact]
        public async Task Should_Return_Product_Details_When_PublicId_Exists()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            TestDataSeeder.SeedProductsData(context);

            var repository = new ProductReadRepo(context);

            var existingProduct = context.Products.First();

            // Act
            var result = await repository.GetDetailsByPublicIdAsync(
                existingProduct.PublicId,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.Name.Should().Be(existingProduct.Name);
            result.PublicId.Should().Be(existingProduct.PublicId);
        }


        [Fact]
        public async Task Should_Return_Null_When_PublicId_Not_Found()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            TestDataSeeder.SeedProductsData(context);

            var repository = new ProductReadRepo(context);

            var fakePublicId = Guid.NewGuid();

            // Act
            var result = await repository.GetDetailsByPublicIdAsync(
                fakePublicId,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }


        [Fact]
        public async Task Should_Return_Product_With_Relations()
        {
            // Arrange
            var context = TestDbContextFactory.Create();
            TestDataSeeder.SeedProductsData(context);

            var repository = new ProductReadRepo(context);

            var product = context.Products.First();

            // Act
            var result = await repository.GetDetailsByPublicIdAsync(
                product.PublicId,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result.CategoryName.Should().NotBeNullOrEmpty();
            result.BrandName.Should().NotBeNullOrEmpty();

            // if have Images
            // result.Images.Should().NotBeEmpty();

            // if have Specifications
            // result.Specifications.Should().NotBeEmpty();
        }
    }
}
