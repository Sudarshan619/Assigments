namespace Day27_Webapi_EF.DTO
{
    public class OrderDTO
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public float TotalValue { get; set; }
        public string OrderStatus { get; set; } = string.Empty;
    }
}
