using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Infrastructure.Persistence;
using ClothingStore.Infrastructure.Persistence.Repositories;
using ClothingStore.IntegrationTests.Common;
using ClothingStore.IntegrationTests.Seeding;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.IntegrationTests.Repositories
{
    public class ProductRepositoryTests: IDisposable
    {

        private readonly ApplicationDbContext _context;
        private readonly ProductRepo _repository;

        public ProductRepositoryTests()
        {
            _context = TestDbContextFactory.Create();

            TestDataSeeder.SeedProductsData(_context);
            
            _repository = new ProductRepo(_context);
        }

        public void Dispose() 
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        [Fact]
        public async Task AddAsync_Should_Add_Product_To_Context()
        {

            //Arrange
            var category = _context.Categories.First();
            var brand = _context.Brands.First();
            var user = _context.UserProfiles.First();

            var product = new Product(
                "Nike shoes",
                "Running shoes",
                new Money(180m, "USD"),
                true,
                user.Id,
                category.Id,
                brand.Id
                );


            //Act
            await _repository.AddAsync(product, CancellationToken.None);

            await _context.SaveChangesAsync();

            //Assert

            var savedProduct = await _context.Products.FirstOrDefaultAsync(x => x.PublicId == product.PublicId);

            savedProduct.Should().NotBeNull();


            savedProduct.Name.Should().Be(product.Name);

        }


        [Fact]
        public async Task GetByIdAsync_Should_Return_Product_When_Exists()
        {
            // Arrange
            var product = _context.Products.First();

            // Act
            var result = await _repository.GetByIdAsync(
                product.PublicId,
                CancellationToken.None);

            // Assert
            result.Should().NotBeNull();

            result!.PublicId.Should().Be(product.PublicId);

            result.Name.Should().Be(product.Name);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_Product_Not_Exists()
        {
            // Arrange
            var publicId = Guid.NewGuid();

            // Act
            var result = await _repository.GetByIdAsync(
                publicId,
                CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

    }
}
