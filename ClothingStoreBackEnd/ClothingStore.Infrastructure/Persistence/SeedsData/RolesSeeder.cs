using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ClothingStore.Infrastructure.Persistence.Seed
{
    public static class RolesSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<long>>>();

            string[] roles =
            {
                "Admin",
                "Customer"
            };

            foreach (var role in roles)
            {
                var exists = await roleManager.RoleExistsAsync(role);

                if (!exists)
                {
                    await roleManager.CreateAsync(new IdentityRole<long>
                    {
                        Name = role
                    });
                }
            }
        }
    }
}
