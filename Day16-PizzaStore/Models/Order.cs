namespace Day16_PizzaStore.Models
{
    public enum OrderStatus
    {
        Created,
        Processing,
        Success,
        Delivered,
        Pending,
        Cancelled
    }

    public class Order : IEquatable<Order>
    {
        public int OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public bool IsPaymentSuccess { get; set; } 
        public OrderStatus OrderStatus { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return Equals(obj as Order);
        }

        public bool Equals(Order? other)
        {
            if (other == null)
                return false;

            return this.OrderNumber == other.OrderNumber;
        }

       
    }
}
