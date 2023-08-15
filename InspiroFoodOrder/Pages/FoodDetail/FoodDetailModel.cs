using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InspiroFoodOrder.Models;
using System.Threading.Tasks;
using InspiroFoodOrder.Services;

namespace InspiroFoodOrder.Pages
{
    public class FoodDetailModel : PageModel
    {
        private readonly FoodDbContext _context;
        private readonly ShoppingCartService _shoppingCartService;

        public FoodDetailModel(FoodDbContext context, ShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        public InspiroFoodOrder.Models.Food Food { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(int? foodId)
        {
            if (foodId == null)
            {
                return NotFound();
            }

            Food = await _context.Foods.FirstOrDefaultAsync(food => food.Id == foodId);

            if (Food == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? foodId)
        {
            if (foodId == null)
            {
                return NotFound();
            }

            Food = await _context.Foods.FirstOrDefaultAsync(food => food.Id == foodId);

            if (Food == null)
            {
                return NotFound();
            }

            if (Quantity <= 0)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than 0.");
                return Page();
            }

            if (Quantity > Food.Stock)
            {
                ModelState.AddModelError("Quantity", "Not enough stock available.");
                return Page();
            }

            _shoppingCartService.AddToCart(Food.Id, Quantity);

            //Food.Stock -= Quantity;
            //await _context.SaveChangesAsync();

            return RedirectToPage("/Food/Index", new {SearchQuery = SearchQuery});
        }
    }
}
