namespace Day16_PizzaStore.Models
{
    public class OrderDetails : IEquatable<OrderDetails>
    {
        public int SINo { get; set; }
        public int OrderNumber { get; set; }
        public int PizzaId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }

        public bool Equals(OrderDetails? other)
        {
            return (this ?? new OrderDetails()).SINo == (other ?? new OrderDetails()).SINo;
        }
    }
}
