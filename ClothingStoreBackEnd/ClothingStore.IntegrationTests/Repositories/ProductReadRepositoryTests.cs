using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Enums;
using ClothingStore.IntegrationTests.Common;
using ClothingStore.IntegrationTests.Seeding;
using FluentAssertions;

namespace ClothingStore.IntegrationTests.Repositories
{
    public class ProductReadRepositoryTests
    {

        [Fact]
        public async Task Should_Return_All_Seeded_Products()
        {
            // Arrange
            using var context = TestDbContextFactory.Create();

            TestDataSeeder.SeedProducts(context);

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
                ProductSortBy.NameAsc,
                CancellationToken.None);

            // Assert
            result.Items.Should().HaveCount(6);
        }


    }
}
