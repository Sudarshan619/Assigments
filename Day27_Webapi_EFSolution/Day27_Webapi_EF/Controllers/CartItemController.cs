using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;
        private readonly ILogger<CartItemController> _logger;

        public CartItemController(ICartItemService CartItemService, ILogger<CartItemController> logger)
        {
            _cartItemService = CartItemService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddCartItem(CartItemDTO CartItem)
        {
            try
            {
                var CartItemId = await _cartItemService.AddCartItem(CartItem);
                return Ok("CartItem added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateCartItem(int id, CartItemDTO CartItem)
        {
            try
            {
                var CartItemId = await _cartItemService.UpdateCartItem(id, CartItem);
                return Ok(CartItemId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                var CartItem = await _cartItemService.GetAllCartItems();
                return Ok(CartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCartItem(int id)
        {
            try
            {
                var CartItem = await _cartItemService.GetCartItem(id);
                return Ok(CartItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }
    }

}



