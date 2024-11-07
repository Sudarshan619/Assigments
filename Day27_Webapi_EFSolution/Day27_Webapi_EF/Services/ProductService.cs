using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Mapper;

namespace Day27_Webapi_EF.Services
{
    public class ProductService:IProductService
    {
        IRepository<int,Product> _productRepository;
        IMapper _mapper;
        public ProductService(IRepository<int, Product> repository,IMapper mapper) {
            _productRepository = repository;
            _mapper = mapper;
        
        }
        public async Task<bool> AddProduct(ProductDTO product)
        {

            Product newProduct = _mapper.Map<Product>(product);
            var addedProduct = await _productRepository.Add(newProduct);
            return addedProduct!=null;
        }

        public async Task<int> UpdateProduct(int id,ProductDTO product)
        {
            try
            {
                var prod = _productRepository.Get(id);
                if (prod != null)
                {
                    Product newProduct = _mapper.Map<Product>(product);
                    var updatedProduct = await _productRepository.Update(id, newProduct);
                    return updatedProduct.Id;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception ex) {
                throw new Exception("Some error occured");
            }
            
        }

        public async Task<Product> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.Get(id);
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex) {
                throw new Exception("Not found");           
            }

        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            try
            {
                var product = await _productRepository.GetAll();
                if (product != null)
                {
                    return product;
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
