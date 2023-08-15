using InspiroFoodOrder.Models;
using InspiroFoodOrder.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace InspiroFoodOrder
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<ShoppingCartService>();
                    services.AddHttpContextAccessor(); // Add this line
                    services.AddDbContext<FoodDbContext>(options =>
                    {
                        options.UseInMemoryDatabase(databaseName: "FoodDatabase"); // In-memory database configuration
                    });

                    // Seed initial data
                    SeedInitialData(services);
                });

        private static void SeedInitialData(IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var dbContext = serviceProvider.GetRequiredService<FoodDbContext>();

                if (!dbContext.Foods.Any())
                {
                    dbContext.Foods.AddRange(
                        new Food("Rendang", 50, 15000),
                        new Food("Ayam Geprek", 100, 12000),
                        new Food("Kentang Mustopa", 100, 8000),
                        new Food("Telor Dadar", 200, 7000),
                        new Food("Siomay", 300, 3000),
                        new Food("Nasi Goreng", 200, 12000),
                        new Food("Kwetiaw", 150, 15000),
                        new Food("Sate", 250, 15000),
                        new Food("Mi Goreng", 400, 12000),
                        new Food("Kupat Tahu", 200, 10000),
                        new Food("Soto Ayam", 300, 15000),
                        new Food("Mie baso", 300, 20000),
                        new Food("Nasi Kuning", 200, 12000),
                        new Food("Pempek", 250, 15000),
                        new Food("Bubur Ayam", 100, 13000)
                    );

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
