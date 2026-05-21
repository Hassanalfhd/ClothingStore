using ClothingStore.Domain.ValueObjects;
using ClothingStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClothingStore.Infrastructure.Persistence.Seed
{
    public static class UsersSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            var context = services.GetRequiredService<ApplicationDbContext>();

            var email = "Hassan@gmail.com";

            string firstName = "Hassan";
            string lastName = "Alfahd";
            var existingUser = await userManager.Users
                .FirstOrDefaultAsync(x => x.Email == email);

            if (existingUser != null)
                return;


            var adminUser = new ApplicationUser
            (
              email,
              firstName,
              lastName
            );

            var result = await userManager.CreateAsync(
                adminUser,
                "Hassan123456"
            );


            if (!result.Succeeded)
            {
                var errors = string.Join(
                    ", ",
                    result.Errors.Select(x => x.Description)
                );

                throw new Exception(errors);
            }

            await userManager.AddToRoleAsync(adminUser, "Admin");

            var contactInfo = new ContactInfo(email, null, null);


            //var profile = new UserProfile
            //(
                
                
            //    );
            //await context.UserProfiles.AddAsync(profile);

            await context.SaveChangesAsync();
        }

    }
}
