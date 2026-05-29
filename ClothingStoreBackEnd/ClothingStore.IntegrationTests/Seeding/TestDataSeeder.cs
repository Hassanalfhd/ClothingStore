using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Application.Features.Products.Commands.CreateProduct;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Identity.Models;
using ClothingStore.Infrastructure.Persistence;

namespace ClothingStore.IntegrationTests.Seeding
{
    public static class TestDataSeeder
    {

        public static void SeedProducts(ApplicationDbContext context)
        {
            // =========================
            // 1. USER (Admin/Test User)
            // =========================
            var user = new ApplicationUser(
                "hassan.alfahd@example.com",
                "Hassan",
                "Alfahd"
            );

            context.Users.Add(user);
            context.SaveChanges();

            var userProfile = new UserProfile(
                user.Id,
                new ContactInfo(
                    user.Email,
                    "Sanaa, Yemen",
                    "+967700000000"
                ),
                user.FirstName,
                user.LastName,
                null,
                null
            );

            // =========================
            // 2. CATEGORIES
            // =========================
            var categorySport = new Category("Sport", "Sport products including shoes and activewear");
            var categoryClothing = new Category("Clothing", "Casual and fashion clothing");


            // =========================
            // 3. BRANDS
            // =========================
            var nike = new Brand("Nike", "nike", "Just Do It", null);
            var adidas = new Brand("Adidas", "adidas", "Impossible is Nothing", null);
            var puma = new Brand("Puma", "puma", "Forever Faster", null);

            context.AddRange(userProfile, categorySport, categoryClothing, nike, adidas, puma);
            context.SaveChanges();

            // =========================
            // 4. PRODUCTS (REALISTIC DATA)
            // =========================
            var products = new List<Product>
    {
        new Product(
            "Nike Air Zoom Pegasus 40",
            "Lightweight running shoes designed for daily training and long-distance comfort.",
            new Money(120, "USD"),
            true,
            user.Id,
            categorySport.Id,
            nike.Id
        ),

        new Product(
            "Adidas Ultraboost 22",
            "Premium running shoes with responsive Boost midsole for energy return.",
            new Money(180, "USD"),
            true,
            user.Id,
            categorySport.Id,
            adidas.Id
        ),

        new Product(
            "Puma Essentials Hoodie",
            "Comfortable cotton hoodie for everyday casual wear and street style.",
            new Money(65, "USD"),
            true,
            user.Id,
            categoryClothing.Id,
            puma.Id
        ),

        new Product(
            "Nike Dri-FIT Training T-Shirt",
            "Breathable training shirt with moisture-wicking Dri-FIT technology.",
            new Money(35, "USD"),
            true,
            user.Id,
            categorySport.Id,
            nike.Id
        ),

        new Product(
            "Adidas Originals Track Jacket",
            "Classic retro-style jacket inspired by Adidas heritage sportswear.",
            new Money(90, "USD"),
            true,
            user.Id,
            categoryClothing.Id,
            adidas.Id
        ),

        new Product(
            "Puma Running Shorts",
            "Lightweight shorts designed for maximum mobility during workouts.",
            new Money(40, "USD"),
            true,
            user.Id,
            categorySport.Id,
            puma.Id
        )
    };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
        

    }
}
