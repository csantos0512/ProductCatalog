using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Domain.Interfaces.Repositories;
using ProductCatalog.Domain.Models;
using System;

namespace ProductCatalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        
        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult Post(Order order)
        {
            try
            {
                if (_orderRepository.Add(order))
                return Ok(new
                {
                    Message = "Order successfully inserted!"
                });
                else
                throw new Exception("Error adding order!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}
