//using System.Data;
//using ClothingStore.Domain.Entities;
//using ClothingStore.Domain.ValueObjects;
//using ClothingStore.Identity.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//namespace ClothingStClothingStore.Infrastructure.Persistence.SeedsData;

//public static class ApplicationDbContextSeeder
//{
//    public static async Task SeedAsync(IServiceProvider services)
//    {
//        var roleManager = services.GetRequiredService<RoleManager<Role>>();
//        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

//        await SeedRolesAsync(roleManager);
//        await SeedUsersAsync(userManager);
//    }

//    private static async Task SeedRolesAsync(
//        RoleManager<ApplicationRole> roleManager)
//    {
//        var roles = new List<ApplicationRole>
//        {
//            new()
//            {
//                Name = Roles.SuperAdmin,
//                NormalizedName = Roles.SuperAdmin.ToUpper(),
//                Description = "Has full access to all system features and settings.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            },
//            new()
//            {
//                Name = Roles.Admin,
//                NormalizedName = Roles.Admin.ToUpper(),
//                Description = "Manages products, orders, users, and store operations.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            },
//            new()
//            {
//                Name = Roles.Manager,
//                NormalizedName = Roles.Manager.ToUpper(),
//                Description = "Oversees daily operations and business performance.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            },
//            new()
//            {
//                Name = Roles.Customer,
//                NormalizedName = Roles.Customer.ToUpper(),
//                Description = "Can browse products and place orders.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            },
//            new()
//            {
//                Name = Roles.Support,
//                NormalizedName = Roles.Support.ToUpper(),
//                Description = "Handles customer support and complaints.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            },
//            new()
//            {
//                Name = Roles.Warehouse,
//                NormalizedName = Roles.Warehouse.ToUpper(),
//                Description = "Manages inventory and shipping operations.",
//                CreatedAt = DateTime.UtcNow,
//                ConcurrencyStamp = Guid.NewGuid().ToString()
//            }
//        };

//        foreach (var role in roles)
//        {
//            if (!await roleManager.RoleExistsAsync(role.Name!))
//            {
//                await roleManager.CreateAsync(role);
//            }
//        }
//    }

//    private static async Task SeedUsersAsync(
//        UserManager<ApplicationUser> userManager)
//    {
//        await CreateUserAsync(
//            userManager,
//            firstName: "System",
//            lastName: "Administrator",
//            email: "superadmin@clothingstore.com",
//            password: "SuperAdmin@123",
//            role: Roles.SuperAdmin,
//            phoneNumber: "+967700000001");

//        await CreateUserAsync(
//            userManager,
//            firstName: "Store",
//            lastName: "Administrator",
//            email: "admin@clothingstore.com",
//            password: "Admin@123",
//            role: Roles.Admin,
//            phoneNumber: "+967700000002");

//        await CreateUserAsync(
//            userManager,
//            firstName: "Ahmed",
//            lastName: "Manager",
//            email: "manager@clothingstore.com",
//            password: "Manager@123",
//            role: Roles.Manager,
//            phoneNumber: "+967700000003");

//        await CreateUserAsync(
//            userManager,
//            firstName: "Sara",
//            lastName: "Support",
//            email: "support@clothingstore.com",
//            password: "Support@123",
//            role: Roles.Support,
//            phoneNumber: "+967700000004");

//        await CreateUserAsync(
//            userManager,
//            firstName: "Ali",
//            lastName: "Warehouse",
//            email: "warehouse@clothingstore.com",
//            password: "Warehouse@123",
//            role: Roles.Warehouse,
//            phoneNumber: "+967700000005");

//        await CreateUserAsync(
//            userManager,
//            firstName: "John",
//            lastName: "Customer",
//            email: "customer@clothingstore.com",
//            password: "Customer@123",
//            role: Roles.Customer,
//            phoneNumber: "+967700000006");
//    }

//    private static async Task CreateUserAsync(
//        UserManager<ApplicationUser> userManager,
//        string firstName,
//        string lastName,
//        string email,
//        string password,
//        string role,
//        string phoneNumber)
//    {
//        var existingUser = await userManager.FindByEmailAsync(email);

//        if (existingUser is not null)
//            return;

//        var user = new ApplicationUser
//        {
//            PublicId = Guid.NewGuid(),
//            FirstName = firstName,
//            LastName = lastName,
//            UserName = email,
//            NormalizedUserName = email.ToUpperInvariant(),
//            Email = email,
//            NormalizedEmail = email.ToUpperInvariant(),
//            EmailConfirmed = true,
//            PhoneNumber = phoneNumber,
//            PhoneNumberConfirmed = true,
//            IsActive = true,
//            CreatedAt = DateTime.UtcNow,
//            SecurityStamp = Guid.NewGuid().ToString(),
//            ConcurrencyStamp = Guid.NewGuid().ToString(),

//            UserProfile = new UserProfile
//            {
//                PublicId = Guid.NewGuid(),
//                FirstName = firstName,
//                LastName = lastName,
//                BirthDate = new DateTime(1995, 1, 1),
//                ProfileImage = string.Empty,
//                CreatedAt = DateTime.UtcNow,
//                UpdatedAt = DateTime.UtcNow,
//                ContactInfo = new ContactInfo(
//                    email,
//                    "Sanaa, Yemen",
//                    phoneNumber)
//            }
//        };

//        var result = await userManager.CreateAsync(user, password);

//        if (!result.Succeeded)
//        {
//            var errors = string.Join(", ",
//                result.Errors.Select(e => e.Description));

//            throw new InvalidOperationException(errors);
//        }

//        await userManager.AddToRoleAsync(user, role);
//    }
//}