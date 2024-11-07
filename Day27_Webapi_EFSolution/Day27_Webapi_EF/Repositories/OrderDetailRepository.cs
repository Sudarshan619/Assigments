using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Interface;
using Microsoft.EntityFrameworkCore;

namespace Day27_Webapi_EF.Repositories
{
    public class OrderDetailRepository : IRepository<int, OrderDetail>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<OrderDetailRepository> _logger;

        public OrderDetailRepository(ShoppingContext context, ILogger<OrderDetailRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<OrderDetail> Add(OrderDetail entity)
        {
            try
            {
                _context.OrderDetails.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("OrderDetail added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("OrderDetail");
            }
        }

        public async Task<OrderDetail> Delete(int key)
        {
            var OrderDetail = await Get(key);
            if (OrderDetail == null)
            {
                throw new NotFoundException("OrderDetail");
            }
            _context.OrderDetails.Remove(OrderDetail);
            await _context.SaveChangesAsync();
            return OrderDetail;
        }

        public async Task<OrderDetail> Get(int key)
        {
            var OrderDetail = await _context.OrderDetails.FirstOrDefaultAsync(p => p.SNo == key);
            if (OrderDetail == null)
            {
                throw new NotFoundException("No item to get");
            }
            return OrderDetail;
        }

        public async Task<IEnumerable<OrderDetail>> GetAll()
        {
            var OrderDetails = await _context.OrderDetails.ToListAsync();
            if (OrderDetails.Count == 0)
            {
                throw new CollectionEmptyException("OrderDetails");
            }
            return OrderDetails;
        }

        public async Task<OrderDetail> Update(int key, OrderDetail entity)
        {
            var OrderDetail = await Get(key);
            if (OrderDetail == null)
            {
                throw new NotFoundException("OrderDetail");
            }
            _context.OrderDetails.Update(entity);
            await _context.SaveChangesAsync();
            return OrderDetail;
        }
    }

}