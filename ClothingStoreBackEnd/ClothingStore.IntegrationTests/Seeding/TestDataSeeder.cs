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

        public static void SeedProductsData(ApplicationDbContext context)
        {
            // =========================
            // USER
            // =========================
            var user = new ApplicationUser(
                "hassan.alfahd@example.com",
                "Hassan",
                "Alfahd");

            context.Users.Add(user);
            context.SaveChanges();

            var userProfile = new UserProfile(
                user.Id,
                new ContactInfo(
                    user.Email,
                    "Sanaa, Yemen",
                    "+967700000000"),
                user.FirstName,
                user.LastName,
                null,
                null);

            // =========================
            // CATEGORIES
            // =========================
            var sportCategory = new Category(
                "Sport",
                "Sport products");

            var clothingCategory = new Category(
                "Clothing",
                "Clothing products");

            var electronicsCategory = new Category(
                "Electronics",
                "Electronics products");

            // =========================
            // BRANDS
            // =========================
            var nike = new Brand(
                "Nike",
                "nike",
                "Just Do It",
                null);

            var adidas = new Brand(
                "Adidas",
                "adidas",
                "Impossible is Nothing",
                null);

            var puma = new Brand(
                "Puma",
                "puma",
                "Forever Faster",
                null);

            var apple = new Brand(
                "Apple",
                "apple",
                "Think Different",
                null);

            context.AddRange(
                userProfile,
                sportCategory,
                clothingCategory,
                electronicsCategory,
                nike,
                adidas,
                puma,
                apple);

            context.SaveChanges();

            // =========================
            // PRODUCTS
            // =========================

            var nikeAirZoom = new Product(
                "Nike Air Zoom",
                "Running shoes",
                new Money(120m, "USD"),
                true,
                user.Id,
                sportCategory.Id,
                nike.Id);

            nikeAirZoom.AddSepecifiaction("Brand", "Nike");
            nikeAirZoom.AddSepecifiaction("Color", "Red");
            nikeAirZoom.AddSepecifiaction("Size", "M");

            var adidasUltraBoost = new Product(
                "Adidas Ultraboost",
                "Premium running shoes",
                new Money(180m, "USD"),
                true,
                user.Id,
                sportCategory.Id,
                adidas.Id);

            adidasUltraBoost.AddSepecifiaction("Brand", "Adidas");
            adidasUltraBoost.AddSepecifiaction("Color", "Blue");
            adidasUltraBoost.AddSepecifiaction("Size", "L");

            var pumaHoodie = new Product(
                "Puma Hoodie",
                "Cotton hoodie",
                new Money(65m, "USD"),
                true,
                user.Id,
                clothingCategory.Id,
                puma.Id);

            pumaHoodie.AddSepecifiaction("Brand", "Puma");
            pumaHoodie.AddSepecifiaction("Color", "Black");
            pumaHoodie.AddSepecifiaction("Size", "XL");

            var nikeTrainingShirt = new Product(
                "Nike Training Shirt",
                "Training shirt",
                new Money(35m, "USD"),
                true,
                user.Id,
                clothingCategory.Id,
                nike.Id);

            nikeTrainingShirt.AddSepecifiaction("Brand", "Nike");
            nikeTrainingShirt.AddSepecifiaction("Color", "Red");
            nikeTrainingShirt.AddSepecifiaction("Size", "S");

            var appleWatch = new Product(
                "Apple Watch Series",
                "Smart watch",
                new Money(300m, "USD"),
                true,
                user.Id,
                electronicsCategory.Id,
                apple.Id);

            appleWatch.AddSepecifiaction("Brand", "Apple");
            appleWatch.AddSepecifiaction("Color", "White");

            context.Products.AddRange(
                nikeAirZoom,
                adidasUltraBoost,
                pumaHoodie,
                nikeTrainingShirt,
                appleWatch);

            var red = new Color("Red", "#ff0000");
            var small = new Size("S", 1);

            context.Colors.Add(red);
            context.Sizes.Add(small);

            context.SaveChanges();

            var variant1 = new ProductVariant(nikeAirZoom.Id, red.Id, small.Id, userProfile.Id, new Money(200, "USD"), 100, "#10001");

            context.ProductsVariant.Add(variant1);
            context.SaveChanges();

        }







    }
}
