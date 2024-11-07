using Day28_EventBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _BookingService;
        private readonly EmployeeService _employeeService;
       private readonly ILogger<BookingController> _logger;

        public BookingController(BookingService productService,EmployeeService employeeService)
        {
            _BookingService = productService;
            _employeeService = employeeService;

        }
        [HttpPost]
        public async Task<IActionResult> AddBooking(BookingDTO product)
        {
            try
            {
                var productId = await _BookingService.AddProduct(product);
                return Ok("Booking added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBooking()
        {
            try
            {
                var Bookings = await _BookingService.GetAllBooking();
                var Employees = await _employeeService.GetAllEmployee();
                var bookingsWithEmployees = from booking in Bookings
                                            join emp in Employees
                                            on booking.EmployeeId equals emp.Id
                                            select new
                                            {
                                                booking.Id,
                                                booking.DateOfBooking,
                                                EmployeeId = emp.Id,
                                                EmployeeName = emp.Name
                                            };


                return Ok(bookingsWithEmployees);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }


    }
}
