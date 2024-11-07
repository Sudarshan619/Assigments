using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day27_Webapi_EF.Repositories
{
    public class ProductRepository : IRepository<int, Product>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ShoppingContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Product> Add(Product entity)
        {
            try
            {
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Product added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Product");
            }
        }

        public async Task<Product> Delete(int key)
        {
            var product = await Get(key);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Get(int key)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == key);
            if(product == null)
            {
                throw new NotFoundException("No item to get");
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            if (products.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return products;
        }

        public async Task<Product> Update(int key, Product entity)
        {
            var product = await Get(key);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
