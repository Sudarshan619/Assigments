using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface IProductService
    {
       Task<bool> AddProduct(ProductDTO product);

       Task<int> UpdateProduct(int id,ProductDTO product);

       Task<Product> GetProduct(int id);

       Task<IEnumerable<Product>> GetAllProducts();
    }
}
