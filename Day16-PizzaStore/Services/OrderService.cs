using Day16_PizzaStore.Exceptions;
using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models;
using Day16_PizzaStore.Models.DTOs;
using Day16_PizzaStore.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        static int orderNumber = 100;
        // Create a new order
        public async Task<PizzaOrderDTO> CreateOrder(PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            if (await DoesCustomerHaveCart(customerId))
            {
                    var customer = await _customerRepository.Get(customerId);
                    var carts = await _cartRepository.GetAll();
                    var customerCart = carts.FirstOrDefault(c => c.CustomerId == customerId);
                    Order order = new Order();

                    orderNumber++;
                    order.OrderNumber = orderNumber;
                    order.CustomerId = customerId;
                    order.PaymentMethod = pizzaOrderDTO.PaymentMethod;
                    order.TotalAmount = pizzaOrderDTO.TotalAmount;
                    order.IsPaymentSuccess = !order.IsPaymentSuccess;
                    order.Pizzas = customerCart.Pizzas;
                    order.OrderStatus = pizzaOrderDTO.OrderStatus;
                    

                await _orderRepository.Add(order);
                await _cartRepository.Delete(customerId);
                return pizzaOrderDTO;
            }

            return pizzaOrderDTO;
        }

        [HttpPost]
        public async Task<PizzaOrderDTO> CreateOrderWithoutCart(PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            
                Order order = new Order();
                var customer = _customerRepository.Get(customerId);
                order.CustomerId = customerId;

               await _orderRepository.Add(order);
            

            return pizzaOrderDTO;
        }
          

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
            Console.WriteLine("line 82" + orders);
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
                    Pizzas = order.Pizzas,
                    OrderNumber= order.OrderNumber,
                    OrderStatus = order.OrderStatus,
                    TotalAmount = order.TotalAmount,
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
