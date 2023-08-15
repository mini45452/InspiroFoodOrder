namespace InspiroFoodOrder.Models
{
    public class FoodOrder
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }

        public FoodOrder(int foodId, int quantity)
        {
            FoodId = foodId;
            Quantity = quantity;
        }
    }
}
