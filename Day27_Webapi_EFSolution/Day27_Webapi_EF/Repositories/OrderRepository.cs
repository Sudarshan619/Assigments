using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Interface;
using Microsoft.EntityFrameworkCore;

namespace Day27_Webapi_EF.Repositories
{
    public class OrderRepository:IRepository<int,Order>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<OrderRepository> _logger;

        public OrderRepository(ShoppingContext context, ILogger<OrderRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Order> Add(Order entity)
        {
            try
            {
                _context.Orders.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Order added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Order");
            }
        }

        public async Task<Order> Delete(int key)
        {
            var Order = await Get(key);
            if (Order == null)
            {
                throw new NotFoundException("Order");
            }
            _context.Orders.Remove(Order);
            await _context.SaveChangesAsync();
            return Order;
        }

        public async Task<Order> Get(int key)
        {
            var Order = await _context.Orders.FirstOrDefaultAsync(p => p.OrderNumber == key);
            if (Order == null)
            {
                throw new NotFoundException("No item to get");
            }
            return Order;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            var Orders = await _context.Orders.ToListAsync();
            if (Orders.Count == 0)
            {
                throw new CollectionEmptyException("Orders");
            }
            return Orders;
        }

        public async Task<Order> Update(int key, Order entity)
        {
            var Order = await Get(key);
            if (Order == null)
            {
                throw new NotFoundException("Order");
            }
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
            return Order;
        }
    }

}
