using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using aspnetcore.Services;
using aspnetcore.Models;

namespace aspnetcore.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IOrderItemService _orderItemService;
        public UserController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public async Task<IEnumerable<UserItem>> GetAll()
        {
            var users = await _orderItemService.GetAllUsersAsync();

            return users;
        }  

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserItem item)
        {
           if (item == null)
           {
               return BadRequest();
           }

           var result = await _orderItemService.CreateUser(item);
           
           return Ok(result);
        }
    }
}
