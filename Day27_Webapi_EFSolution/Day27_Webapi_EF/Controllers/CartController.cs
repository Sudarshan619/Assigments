using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartService CartService, ILogger<CartController> logger)
        {
            _cartService = CartService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddCart(CartDTO Cart)
        {
            try
            {
                var CartId = await _cartService.AddCart(Cart);
                return Ok("Cart added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCart(int id, CartDTO Cart)
        {
            try
            {
                var CartId = await _cartService.UpdateCart(id, Cart);
                return Ok(CartId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var Cart = await _cartService.GetAllCarts();
                return Ok(Cart);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCart(int id)
        {
            try
            {
                var Cart = await _cartService.GetCart(id);
                return Ok(Cart);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }
    }

}



