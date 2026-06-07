using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.IntegrationTests.Common
{
    public static class TestDbContextFactory
    {

        public static ApplicationDbContext Create()
        {

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite("DataSource=:memory:")
                .EnableSensitiveDataLogging()
                .Options;

            var context = new ApplicationDbContext(options);

            context.Database.OpenConnection();

            context.Database.EnsureCreated();

            return context;


        }

      
    }
}
