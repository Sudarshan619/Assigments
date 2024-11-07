using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailDetailService;
        private readonly ILogger<OrderDetailController> _logger;

        public OrderDetailController(IOrderDetailService OrderDetailService, ILogger<OrderDetailController> logger)
        {
            _orderDetailDetailService = OrderDetailService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddOrderDetail(OrderDetailDTO OrderDetail)
        {
            try
            {
                var OrderDetailId = await _orderDetailDetailService.AddOrderDetail(OrderDetail);
                return Ok("OrderDetail added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateOrderDetail(int id, OrderDetailDTO OrderDetail)
        {
            try
            {
                var OrderDetailId = await _orderDetailDetailService.UpdateOrderDetail(id, OrderDetail);
                return Ok(OrderDetailId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            try
            {
                var OrderDetail = await _orderDetailDetailService.GetAllOrderDetails();
                return Ok(OrderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderDetail(int id)
        {
            try
            {
                var OrderDetail = await _orderDetailDetailService.GetOrderDetail(id);
                return Ok(OrderDetail);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }
    }

}



