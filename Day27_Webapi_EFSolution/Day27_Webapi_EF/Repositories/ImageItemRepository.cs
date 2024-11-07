using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Day27_Webapi_EF.Repositories
{
    public class ImageItemRepository : IRepository<int, ImageItem>
    {
        private readonly ShoppingContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ImageItemRepository(ShoppingContext context, ILogger<ProductRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ImageItem> Add(ImageItem entity)
        {
            try
            {
                _context.ImageItems.Add(entity);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Image added");
                return entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new CouldNotAddException("Product");
            }
        }

        public async Task<ImageItem> Delete(int key)
        {
            var product = await Get(key);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            _context.ImageItems.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<ImageItem> Get(int key)
        {
            var product = await _context.ImageItems.FirstOrDefaultAsync(p => p.ImageId == key);
            if (product == null)
            {
                throw new NotFoundException("No item to get");
            }
            return product;
        }

        public async Task<IEnumerable<ImageItem>> GetAll()
        {
            var imageItem = await _context.ImageItems.ToListAsync();
            if (imageItem.Count == 0)
            {
                throw new CollectionEmptyException("Products");
            }
            return imageItem;
        }

        public async Task<ImageItem> Update(int key, ImageItem entity)
        {
            var product = await Get(key);
            if (product == null)
            {
                throw new NotFoundException("Product");
            }
            _context.ImageItems.Update(entity);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
