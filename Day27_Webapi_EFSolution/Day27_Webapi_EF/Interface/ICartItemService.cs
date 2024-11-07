using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;

namespace Day27_Webapi_EF.Interface
{
    public interface ICartItemService
    {
        Task<bool> AddCartItem(CartItemDTO CartItem);

        Task<int> UpdateCartItem(int id, CartItemDTO CartItem);

        Task<CartItem> GetCartItem(int id);

        Task<IEnumerable<CartItem>> GetAllCartItems();
    }
}

