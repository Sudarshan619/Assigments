using Day16_PizzaStore.Models.DTOs;

namespace Day16_PizzaStore.Interface
{
    public interface IOrderService
    {
        public Task<PizzaOrderDTO> CreateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId);

        public Task<OrderDTO> GetAllOrder(int customerId);
    }
        
}
