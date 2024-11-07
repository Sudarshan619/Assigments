using Day28_EventBooking.Services;
using Microsoft.AspNetCore.Mvc;
using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(EmployeeService productService)
        {
            _employeeService = productService;

        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO product)
        {
            try
            {
                var productId = await _employeeService.AddProduct(product);
                return Ok("Employee added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployee();
                var empNames = (from employee in employees 
                                where employee.Age>23
                                orderby employee.Name
                                select new {eName = employee.Name,eAge= employee.Age}).ToList();
                return Ok(empNames);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        
    }
}
