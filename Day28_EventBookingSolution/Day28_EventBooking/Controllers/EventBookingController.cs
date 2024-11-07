using Day28_EventBooking.DTO;
using Day28_EventBooking.Model;
using Day28_EventBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day28_EventBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventBookingController : ControllerBase
    {
        private readonly EventBookingService _EventBookingService;
       

        public EventBookingController(EventBookingService productService)
        {
            _EventBookingService = productService;

        }
        [HttpPost("AddEventBooking")]
        public async Task<IActionResult> AddEventBooking(EventBookingDTO product)
        {
            try
            {
                var productId = await _EventBookingService.AddProduct(product);
                return Ok("EventBooking added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAllEvents")]
        public async Task<IActionResult> GetAllEventBooking()
        {
            try
            {
                var EventBookings = await _EventBookingService.GetAllEventBooking();
               

                return Ok(EventBookings);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }


    }

}
