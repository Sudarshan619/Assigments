using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Mapper;

namespace Day27_Webapi_EF.Services
{
    public class CartItemService : ICartItemService
    {
        IRepository<int, CartItem> _cartItemRepository;
        IMapper _mapper;
        public CartItemService(IRepository<int, CartItem> repository, IMapper mapper)
        {
            _cartItemRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddCartItem(CartItemDTO CartItem)
        {

            CartItem newCartItem = _mapper.Map<CartItem>(CartItem);
            var addedCartItem = await _cartItemRepository.Add(newCartItem);
            return addedCartItem != null;
        }

        public async Task<int> UpdateCartItem(int id, CartItemDTO CartItem)
        {
            try
            {
                var prod = _cartItemRepository.Get(id);
                if (prod != null)
                {
                    CartItem newCartItem = _mapper.Map<CartItem>(CartItem);
                    var updatedCartItem = await _cartItemRepository.Update(id, newCartItem);
                    return updatedCartItem.SNo;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Some error occured");
            }

        }

        public async Task<CartItem> GetCartItem(int id)
        {
            try
            {
                var CartItem = await _cartItemRepository.Get(id);
                if (CartItem != null)
                {
                    return CartItem;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }

        public async Task<IEnumerable<CartItem>> GetAllCartItems()
        {
            try
            {
                var CartItem = await _cartItemRepository.GetAll();
                if (CartItem != null)
                {
                    return CartItem;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
