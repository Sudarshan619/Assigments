using Day16_PizzaStore.Models.DTOs;

namespace Day16_PizzaStore.Interface
{
    public interface ICartService
    {
        public Task<PizzaCartDTO> AddPizzaToCart(PizzaCartDTO pizzaCartDTO, int customerId);
        public Task<CartDTO> GetCart(int customerId);
        public Task<CartDTO> UpdateCart(int cartNumber, PizzaCartDTO pizzaCartDTO);

    }
}
