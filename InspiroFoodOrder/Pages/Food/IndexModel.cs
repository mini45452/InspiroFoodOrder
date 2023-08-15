using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using InspiroFoodOrder.Models;
using InspiroFoodOrder.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InspiroFoodOrder.Pages.Food
{
    public class IndexModel : PageModel
    {
        private readonly FoodDbContext _context;
        private readonly ShoppingCartService _shoppingCartService;
        public Dictionary<int, int> CartItems { get; set; }
        public FoodDbContext FoodDbContext { get; set; }

        public IndexModel(FoodDbContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
            CartItems = _shoppingCartService.GetCartItems();
            FoodDbContext = _context;
        }

        public PaginatedList<InspiroFoodOrder.Models.Food> Foods { get; set; } // Define the Foods property

        public async Task OnGetAsync(string searchQuery, int pageIndex = 1)
        {
            int pageSize = 5;
            IQueryable<InspiroFoodOrder.Models.Food> foodsQuery = _context.Foods.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                foodsQuery = foodsQuery.Where(food => food.Name.Contains(searchQuery));
            }

            int totalItems = await foodsQuery.CountAsync();
            List<InspiroFoodOrder.Models.Food> items = await foodsQuery
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            Foods = new PaginatedList<InspiroFoodOrder.Models.Food>(items, totalItems, pageIndex, pageSize);
            ViewData["searchQuery"] = searchQuery;
        }

    }
}
