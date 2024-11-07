using Day28_EventBooking.DTO;
using Day28_EventBooking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day28_EventBooking.Controllers
{
   
        public class EventController : ControllerBase
        {
            private readonly EventService _eventService;
           
            private readonly ILogger<EventController> _logger;

            public EventController(EventService productService)
            {
                _eventService = productService;

            }
            [HttpPost("Addevent")]
            public async Task<IActionResult> AddEvent(EventDTO product)
            {
                try
                {
                    var productId = await _eventService.AddProduct(product);
                    return Ok("Event added");
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }


            [HttpGet("GetAll")]
            public async Task<IActionResult> GetAllEvent()
            {
                try
                {
                    var Events = await _eventService.GetAllEvent();
                    

                    return Ok(Events);
                }
                catch (Exception ex)
                {
                    throw new Exception("Some error");
                }
            }


        }
    
}
