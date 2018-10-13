﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetcore.Services;
using aspnetcore.Models;

namespace aspnetcore.Controllers
{
    //[Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        public OrderController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("Api/Order", Name = "GetAllOrders")]
        public async Task<IEnumerable<OrderItem>> GetAll()
        {
            var orderItems = await _orderItemService.GetAllOrdersAsync();
            return orderItems;
        }

        [HttpGet("Api/Order/{ID}", Name = "GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _orderItemService.GetOrderAsync(id);
            if (item == null)
            {
                return NotFound("Order not found");
            }
            return Ok(item);
        }

        [HttpGet("Api/Order/ForUser/{userID}", Name = "GetOrdersForUser")]
        public async Task<IEnumerable<OrderItem>> GetOrdersFor(int userID)
        {
            var orderItems = await _orderItemService.GetOrdersFor(userID);
            return orderItems;
        }

        [HttpPost("Api/Order")]
        public async Task<IActionResult> Create([FromBody] OrderItem item)
        {
           if (item == null)
           {
               return BadRequest();
           }
           var response = await _orderItemService.CreateOrder(item); 

           return Ok();
        }

        [HttpPut("Api/Order")]
        public async Task<IActionResult> Update([FromBody] OrderItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            try
            {
                var response = await _orderItemService.UpdateOrder(item);
            }
            catch (Exception)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " +
                    "see your system administrator.");
            }
           return Ok();
        }
    }
}
