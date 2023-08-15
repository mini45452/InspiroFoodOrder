using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InspiroFoodOrder.Models;
using InspiroFoodOrder.Services;

namespace InspiroFoodOrder.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly FoodDbContext _context;
        private readonly ShoppingCartService _shoppingCartService;

        public IndexModel(FoodDbContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            foreach (var cartItem in _shoppingCartService.GetCartItems())
            {
                var food = await _context.Foods.FindAsync(cartItem.Key);
                if (food != null)
                {
                    food.Stock -= cartItem.Value;
                }
            }

            _shoppingCartService.ClearCart();
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
