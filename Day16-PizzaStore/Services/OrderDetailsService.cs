using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models;
using Day16_PizzaStore.Repositories;
using Day16_PizzaStore.Models.DTOs;

namespace Day16_PizzaStore.Services
{
    public class OrderDetailsService
    {
        private readonly IRepository<int,OrderDetailsRepository> _orderDetailsRepository;
        private readonly IRepository<int,OrderRepository> _orderRepository;
        private readonly OrderService _orderService;
        public OrderDetailsService(IRepository<int,OrderDetailsRepository> orderDetailsRepository,IRepository<int,OrderRepository> orderRepository,OrderService orderService) { 
            _orderDetailsRepository = orderDetailsRepository;
            _orderRepository = orderRepository;
            _orderService = orderService;
        }

        public async Task<OrderDetailsDTO> GetAll(int customerid)
        {
            var orderDetails = _orderService.GetAllOrder(customerid);
            return new OrderDetailsDTO()
            {

            };

        }
    }
}
