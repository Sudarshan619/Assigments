using Day16_PizzaStore.Exceptions;
using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models;
using Day16_PizzaStore.Models.DTOs;

namespace Day16_PizzaStore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<int, Customer> _customerRepository;
        private readonly IRepository<int, Pizza> _pizzaRepository;
        private readonly IRepository<int, Cart> _cartRepository;
        private readonly IRepository<int, Order> _orderRepository;

        public OrderService(
            IRepository<int, Customer> customerRepository,
            IRepository<int, Pizza> pizzaRepository,
            IRepository<int, Cart> cartRepository,
            IRepository<int, Order> orderRepository
            )
        {
            _customerRepository = customerRepository;
            _pizzaRepository = pizzaRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        // Create a new order
        public async Task<PizzaOrderDTO> CreateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            if (await DoesCustomerHaveCart(customerId))
            {
                Order order = new Order()
                {
                    CustomerId = customerId,
                    OrderStatus = OrderStatus.Created,
                    PaymentMethod = pizzaOrderDTO.PaymentMethod,
                    TotalAmount = 100.0f // Placeholder for calculation
                };

                // Save the order
                _orderRepository.Add(order);
            }
            return pizzaOrderDTO;
        }

        // Get all orders for a specific customer
        public async Task<OrderDTO> GetAllOrder(int customerId)
        {
            var orders = await GetCustomerOrders(customerId);
            var pizzaOrderDTOs = await MapOrdersToPizzaOrderDTO(orders);

            return new OrderDTO()
            {
                OrderNumber = orders.First().OrderNumber,
                Orders = pizzaOrderDTOs
            };
        }

        // Get all orders for a customer
        public async Task<List<Order>> GetCustomerOrders(int customerId)
        {
            var orders = await _orderRepository.GetAll();
            return orders.Where(c => c.CustomerId == customerId).ToList();
        }

        // Map orders to PizzaOrderDTO
        private async Task<List<PizzaOrderDTO>> MapOrdersToPizzaOrderDTO(List<Order> orders)
        {
            var pizzaOrderDTOs = new List<PizzaOrderDTO>();

            foreach (var order in orders)
            {
                pizzaOrderDTOs.Add(new PizzaOrderDTO()
                {
                    CustomerId = order.CustomerId,
                    CartNumber = order.OrderNumber,
                    PaymentMethod = order.PaymentMethod
                });
            }
            return pizzaOrderDTOs;
        }

        // Check if the customer has a cart
        private async Task<bool> DoesCustomerHaveCart(int customerId)
        {
            try
            {
                var customer = await _customerRepository.Get(customerId);
                var carts = await _cartRepository.GetAll();
                var customerCart = carts.FirstOrDefault(c => c.CustomerId == customerId);
                return customerCart != null;
            }
            catch (CollectionEmptyException)
            {
                return false;
            }
        }
    }
}
