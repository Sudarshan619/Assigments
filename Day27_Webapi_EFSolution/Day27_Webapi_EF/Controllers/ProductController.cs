using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {  
            private readonly IProductService _productService;
            private readonly ILogger<ProductController> _logger;

        public ProductController(IProductService productService, ILogger<ProductController> logger)
            {
                _productService = productService;
                _logger = logger;
               
            }
            [HttpPost]
            [Authorize(Roles = "Admin")]

            public async Task<IActionResult> AddProduct(ProductDTO product)
            {
                try
                {
                    var productId = await _productService.AddProduct(product);
                    return Ok("Product added");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }

        [HttpPost("Update")] 
        public async Task<IActionResult> UpdateProduct(int id,ProductDTO product)
        {
            try
            {
                var productId = await _productService.UpdateProduct(id,product);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

         }
        

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var product = await _productService.GetAllProducts();
                return Ok(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
            public async Task<IActionResult> GetProduct(int id)
            {
                try
                {
                    var product = await _productService.GetProduct(id);
                    return Ok(product);
                }
                catch (Exception ex) {
                    throw new Exception("Some error");
                }
              }
           }

}
   
    

