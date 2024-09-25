using Day16_PizzaStore.Exceptions;
using Day16_PizzaStore.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Day16_PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<IActionResult> ViewPizzas()
        {
            try
            {
                var pizzas = await _customerService.ViewPizzas();
                return Ok(pizzas);
            }
            catch (CollectionEmptyException e)
            {

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
