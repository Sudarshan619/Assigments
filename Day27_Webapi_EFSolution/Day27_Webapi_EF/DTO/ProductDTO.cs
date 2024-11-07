namespace Day27_Webapi_EF.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public float PricePerUnit { get; set; }
        public string BasicImage { get; set; } = string.Empty;
    }
}
