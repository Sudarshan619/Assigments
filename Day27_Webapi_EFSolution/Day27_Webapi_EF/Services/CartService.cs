using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;


namespace Day27_Webapi_EF.Services
{
    public class CartService : ICartService
    {
        IRepository<int, Cart> _cartRepository;
        IMapper _mapper;
        public CartService(IRepository<int, Cart> repository, IMapper mapper)
        {
            _cartRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddCart(CartDTO Cart)
        {

            Cart newCart = _mapper.Map<Cart>(Cart);
            var addedCart = await _cartRepository.Add(newCart);
            return addedCart != null;
        }

        public async Task<int> UpdateCart(int id, CartDTO Cart)
        {
            try
            {
                var prod = _cartRepository.Get(id);
                if (prod != null)
                {
                    Cart newCart = _mapper.Map<Cart>(Cart);
                    var updatedCart = await _cartRepository.Update(id, newCart);
                    return updatedCart.Id;
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

        public async Task<Cart> GetCart(int id)
        {
            try
            {
                var Cart = await _cartRepository.Get(id);
                if (Cart != null)
                {
                    return Cart;
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

        public async Task<IEnumerable<Cart>> GetAllCarts()
        {
            try
            {
                var Cart = await _cartRepository.GetAll();
                if (Cart != null)
                {
                    return Cart;
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
