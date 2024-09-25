namespace Day16_PizzaStore.Models.DTOs
{
    public class PizzaCartDTO : IEquatable<PizzaCartDTO>
    {
        public string PizzaName { get; set; } = string.Empty;
        public int PizzaId { get; set; }
        public int Quantity { get; set; }

        public float Price { get; set; }
        public float Discount { get; set; }

        public bool Equals(PizzaCartDTO? other)
        {
            return (this ?? new PizzaCartDTO()).PizzaId == (other ?? new PizzaCartDTO()).PizzaId;
        }
    }
}
