namespace Day16_PizzaStore.Models.DTOs
{
    public class PizzaOrderDTO:IEquatable<PizzaOrderDTO>
    {
        public int CartNumber { get; set; }
        public int CustomerId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;

        bool IEquatable<PizzaOrderDTO>.Equals(PizzaOrderDTO? other)
        {
            return this.CartNumber == other.CartNumber;
        }
    }
}
