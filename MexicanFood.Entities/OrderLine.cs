using MexicanFood.Entities;

namespace MexicanFood.Core.Entities
{
    public class OrderLine
    {
        public int MealId { get; set; }
        public Meal Meal { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Quantity { get; set; }
        public double PriceWhenBought { get; set; }
    }
}