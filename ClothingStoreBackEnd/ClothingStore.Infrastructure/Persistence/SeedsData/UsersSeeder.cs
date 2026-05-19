using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClothingStore.Infrastructure.Persistence.Seed
{
    public static class UsersSeeder
    {
        //public static async Task SeedAsync(IServiceProvider services)
        //{
        //    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        //    var context = services.GetRequiredService<ApplicationDbContext>();

        //    var email = "Hassan@gmail.com";

        //    var existingUser = await userManager.Users
        //        .FirstOrDefaultAsync(x => x.Email == email);

        //    if (existingUser != null)
        //        return;

        //    var adminUser = new ApplicationUser
        //    (
        //        FirstName = "Hassan",
        //        LastName = "Alfahd",
        //        UserName = "Hassan",

        //        Email = email,
        //        NormalizedEmail = email.ToUpper(),

        //        EmailConfirmed = true,

        //        IsActive = true,

        //        CreatedAt = DateTime.UtcNow
        //    );

        //    var result = await userManager.CreateAsync(
        //        adminUser,
        //        "Hassan123456"
        //    );

        //    if (!result.Succeeded)
        //    {
        //        var errors = string.Join(
        //            ", ",
        //            result.Errors.Select(x => x.Description)
        //        );

        //        throw new Exception(errors);
        //    }

        //    await userManager.AddToRoleAsync(adminUser, "Admin");


        //    var profile = new UserProfile
        //    {
        //        PublicId = Guid.NewGuid(),

        //        ApplicationUserId = adminUser.Id,

        //        FirstName = "Hassan",
        //        LastName = "Alfahd",

        //        ProfileImage = "profiles/default-admin.png",

        //        ContactInfo = new ContactInfo(
        //            email,
        //            "Sanaa, Yemen",
        //            "+967700000000"
        //        ),

        //        CreatedAt = DateTime.UtcNow
        //    };

        //    await context.UserProfiles.AddAsync(profile);

        //    await context.SaveChangesAsync();
        //}
   
    }
}
