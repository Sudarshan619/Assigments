namespace Day27_Webapi_EF.DTO
{
    public class OrderDetailDTO
    {
        public int OrderNumber { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
