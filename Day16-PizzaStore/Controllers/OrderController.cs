﻿using Day16_PizzaStore.Interface;
using Day16_PizzaStore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Day16_PizzaStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // Get all orders for a specific customer
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrder(int customerId)
        {
            try
            {
                var order = await _orderService.GetAllOrder(customerId);
                return Ok(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                return BadRequest(e.Message);
            }
        }

        // Create a new order for a customer
        [HttpPost("{customerId}")]
        public async Task<IActionResult> GenerateOrder([FromBody] PizzaOrderDTO pizzaOrderDTO, int customerId)
        {
            try
            {
                var order = await _orderService.CreateOrder(pizzaOrderDTO, customerId);
                return Ok(order);
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