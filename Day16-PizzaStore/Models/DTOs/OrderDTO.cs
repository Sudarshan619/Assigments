namespace Day16_PizzaStore.Models.DTOs
{
    public class OrderDTO : IEquatable<OrderDTO>
    {
        public int OrderNumber { get; set; }

        
        public List<PizzaOrderDTO> Orders { get; set; }

        public OrderDTO()
        {
            Orders = new List<PizzaOrderDTO>();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            return Equals(obj as OrderDTO);
        }

        public bool Equals(OrderDTO? other)
        {
            if (other == null)
                return false;

            return this.OrderNumber == other.OrderNumber;
        }

    }
}
