using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Infrastructure.Persistence.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;

namespace ClothingStore.Infrastructure.Persistence.SeedsData
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            using var scop = service.CreateScope();

            var ServiceProvider = scop.ServiceProvider;

            var context = ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();

            await RolesSeeder.SeedAsync(ServiceProvider);
            await UsersSeeder.SeedAsync(ServiceProvider);
                

        }

    }
}
