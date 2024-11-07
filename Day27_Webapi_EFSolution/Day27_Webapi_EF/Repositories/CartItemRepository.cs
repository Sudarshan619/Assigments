using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day27_Webapi_EF.Repositories
{
    public class CartItemRepository : IRepository<int, CartItem>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<CartItemRepository> _logger;

        public CartItemRepository(ShoppingContext context, ILogger<CartItemRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<CartItem> Add(CartItem entity)
        {
            try
            {
                _context.CartItems.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("CartItem added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("CartItem");
            }
        }

        public async Task<CartItem> Delete(int key)
        {
            var CartItem = await Get(key);
            if (CartItem == null)
            {
                throw new NotFoundException("CartItem");
            }
            _context.CartItems.Remove(CartItem);
            await _context.SaveChangesAsync();
            return CartItem;
        }

        public async Task<CartItem> Get(int key)
        {
            var CartItem = await _context.CartItems.FirstOrDefaultAsync(p => p.SNo == key); 
            if (CartItem == null)
            {
                throw new NotFoundException("No item to get");
            }
            return CartItem;
        }

        public async Task<IEnumerable<CartItem>> GetAll()
        {
            var CartItems = await _context.CartItems.ToListAsync();
            if (CartItems.Count == 0)
            {
                throw new CollectionEmptyException("CartItems");
            }
            return CartItems;
        }

        public async Task<CartItem> Update(int key, CartItem entity)
        {
            var CartItem = await Get(key);
            if (CartItem == null)
            {
                throw new NotFoundException("CartItem");
            }
            _context.CartItems.Update(entity);
            await _context.SaveChangesAsync();
            return CartItem;
        }
    }
}
