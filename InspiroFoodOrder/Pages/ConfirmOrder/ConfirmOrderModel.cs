using InspiroFoodOrder.Models;
using InspiroFoodOrder.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class ConfirmOrderModel : PageModel
{
    private readonly ShoppingCartService _shoppingCartService;
    public readonly FoodDbContext _context;

    public ConfirmOrderModel(ShoppingCartService shoppingCartService, FoodDbContext context)
    {
        _shoppingCartService = shoppingCartService;
        _context = context;
    }

    public Dictionary<int, int> CartItems { get; private set; }

    public void OnGet()
    {
        CartItems = _shoppingCartService.GetCartItems();
    }
}
