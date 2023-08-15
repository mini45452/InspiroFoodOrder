using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace InspiroFoodOrder.Models
{
    public class FoodDbContext : DbContext
    {
        public DbSet<Food> Foods { get; set; }

        public FoodDbContext(DbContextOptions<FoodDbContext> options) : base(options)
        {
        }
    }
}
