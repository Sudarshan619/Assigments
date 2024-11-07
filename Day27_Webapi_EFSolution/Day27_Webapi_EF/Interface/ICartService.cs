using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface ICartService
    {
        Task<bool> AddCart(CartDTO Cart);

        Task<int> UpdateCart(int id, CartDTO Cart);

        Task<Cart> GetCart(int id);

        Task<IEnumerable<Cart>> GetAllCarts();
    }
}

