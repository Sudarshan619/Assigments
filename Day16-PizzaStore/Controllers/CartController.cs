using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Day16_PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost]
        public async Task<IActionResult> AddPizzaToCart(PizzaCartDTO pizzaCartDTO, int customerId)
        {
            try
            {
                var cart = await _cartService.AddPizzaToCart(pizzaCartDTO, customerId);
                return Ok(cart);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCart(int customerId)
        {
            try
            {
                var cart = await _cartService.GetCart(customerId);
                return Ok(cart);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }
    }
}
