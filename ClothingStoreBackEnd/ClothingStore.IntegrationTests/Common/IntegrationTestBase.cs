using ClothingStore.Infrastructure.Persistence;
using ClothingStore.IntegrationTests.Seeding;

namespace ClothingStore.IntegrationTests.Common
{

    public abstract class IntegrationTestBase : IDisposable
    {
        protected readonly ApplicationDbContext _context;

        protected IntegrationTestBase()
        {
            _context = TestDbContextFactory.Create();

            TestDataSeeder.SeedProductsData(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
