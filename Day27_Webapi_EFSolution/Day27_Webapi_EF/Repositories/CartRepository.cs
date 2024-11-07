using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day27_Webapi_EF.Repositories
{
    public class CartRepository : IRepository<int, Cart>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(ShoppingContext context, ILogger<CartRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Cart> Add(Cart entity)
        {
            try
            {
                _context.Carts.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Cart added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Cart");
            }
        }

        public async Task<Cart> Delete(int key)
        {
            var Cart = await Get(key);
            if (Cart == null)
            {
                throw new NotFoundException("Cart");
            }
            _context.Carts.Remove(Cart);
            await _context.SaveChangesAsync();
            return Cart;
        }

        public async Task<Cart> Get(int key)
        {
            var Cart = await _context.Carts.FirstOrDefaultAsync(p => p.Id == key);
            if (Cart == null)
            {
                throw new NotFoundException("No item to get");
            }
            return Cart;
        }

        public async Task<IEnumerable<Cart>> GetAll()
        {
            var Carts = await _context.Carts.ToListAsync();
            if (Carts.Count == 0)
            {
                throw new CollectionEmptyException("Carts");
            }
            return Carts;
        }

        public async Task<Cart> Update(int key, Cart entity)
        {
            var Cart = await Get(key);
            if (Cart == null)
            {
                throw new NotFoundException("Cart");
            }
            _context.Carts.Update(entity);
            await _context.SaveChangesAsync();
            return Cart;
        }
    }
}
