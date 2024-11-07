using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Mapper;

namespace Day27_Webapi_EF.Services
{
    public class OrderService : IOrderService
    {
        IRepository<int, Order> _orderRepository;
        IMapper _mapper;
        public OrderService(IRepository<int, Order> repository, IMapper mapper)
        {
            _orderRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddOrder(OrderDTO Order)
        {

            Order newOrder = _mapper.Map<Order>(Order);
            var addedOrder = await _orderRepository.Add(newOrder);
            return addedOrder != null;
        }

        public async Task<int> UpdateOrder(int id, OrderDTO Order)
        {
            try
            {
                var prod = _orderRepository.Get(id);
                if (prod != null)
                {
                    Order newOrder = _mapper.Map<Order>(Order);
                    var updatedOrder = await _orderRepository.Update(id, newOrder);
                    return updatedOrder.OrderNumber;
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

        public async Task<Order> GetOrder(int id)
        {
            try
            {
                var Order = await _orderRepository.Get(id);
                if (Order != null)
                {
                    return Order;
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

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            try
            {
                var Order = await _orderRepository.GetAll();
                if (Order != null)
                {
                    return Order;
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
