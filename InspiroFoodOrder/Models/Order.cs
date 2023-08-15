using System.Collections.Generic;

namespace InspiroFoodOrder.Models
{
    public class Order
    {
        public List<FoodOrder> FoodOrders { get; set; } = new List<FoodOrder>();
    }
}
