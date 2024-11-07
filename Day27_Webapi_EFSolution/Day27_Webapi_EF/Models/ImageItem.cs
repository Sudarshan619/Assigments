using System.ComponentModel.DataAnnotations;

namespace Day27_Webapi_EF.Models
{
    public class ImageItem
    {
        [Key]
        public int ImageId{get; set;}
        public string ImageUrl { get; set; }
        public int ProductId { get; set;}
        public Product Product { get; set;}
        public ImageItem() { 
           Product = new Product();
        }
    }
}
