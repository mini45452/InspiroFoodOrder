namespace InspiroFoodOrder.Models
{
    public class Food
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public int Price { get; set; }

        public Food(string name, int stock, int price)
        {
            Name = name;
            Stock = stock;
            Price = price;
        }
    }
}
