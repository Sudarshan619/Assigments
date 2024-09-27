namespace Day16_PizzaStore.Models
{
    //public enum OrderStatus
    //{
    //    Created,
    //    Processing,
    //    Success,
    //    Delivered,
    //    Pending,
    //    Cancelled
    //}

    public class Order : IEquatable<Order>
    {
        public int OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public List<Pizza> Pizzas { get; set; }
        public float TotalAmount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public bool IsPaymentSuccess { get; set; } = false;
        public string OrderStatus { get; set; }


        public Order() { 
           Pizzas = new List<Pizza>();
        }
        public bool Equals(Order? other)
        {
            if (other == null)
                return false;

            return this.OrderNumber == other.OrderNumber;
        }

       
    }
}
