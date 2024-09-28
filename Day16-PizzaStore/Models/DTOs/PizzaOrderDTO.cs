namespace Day16_PizzaStore.Models.DTOs
{
    public class PizzaOrderDTO:IEquatable<PizzaOrderDTO>
    {
        public int OrderNumber { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string OrderStatus { get; set; }
        public List<Pizza> Pizzas { get; set; }

        bool IEquatable<PizzaOrderDTO>.Equals(PizzaOrderDTO? other)
        {
            return this.OrderNumber == other.OrderNumber;
        }
    }
}
