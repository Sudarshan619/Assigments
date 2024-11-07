using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService OrderService, ILogger<OrderController> logger)
        {
            _orderService = OrderService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddOrder(OrderDTO Order)
        {
            try
            {
                var OrderId = await _orderService.AddOrder(Order);
                return Ok("Order added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateOrder(int id, OrderDTO Order)
        {
            try
            {
                var OrderId = await _orderService.UpdateOrder(id, Order);
                return Ok(OrderId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var Order = await _orderService.GetAllOrders();
                return Ok(Order);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            try
            {
                var Order = await _orderService.GetOrder(id);
                return Ok(Order);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }
    }

}



