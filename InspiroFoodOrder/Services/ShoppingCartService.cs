using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace InspiroFoodOrder.Services
{
    public class ShoppingCartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShoppingCartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ISession Session => _httpContextAccessor.HttpContext.Session;

        public void AddToCart(int foodId, int quantity)
        {
            var cartItemsJson = Session.GetString("CartItems");
            var cartItems = string.IsNullOrEmpty(cartItemsJson)
                ? new Dictionary<int, int>()
                : JsonConvert.DeserializeObject<Dictionary<int, int>>(cartItemsJson);

            if (cartItems.ContainsKey(foodId))
            {
                cartItems[foodId] += quantity;
            }
            else
            {
                cartItems.Add(foodId, quantity);
            }

            Session.SetString("CartItems", JsonConvert.SerializeObject(cartItems));
        }

        public Dictionary<int, int> GetCartItems()
        {
            var cartItemsJson = Session.GetString("CartItems");
            return string.IsNullOrEmpty(cartItemsJson)
                ? new Dictionary<int, int>()
                : JsonConvert.DeserializeObject<Dictionary<int, int>>(cartItemsJson);
        }

        public void ClearCart()
        {
            Session.Remove("CartItems");
        }
    }
}
