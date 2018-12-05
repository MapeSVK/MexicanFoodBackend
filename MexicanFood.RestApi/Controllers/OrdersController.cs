using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MexicanFood.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/Orders
        [HttpGet]
        public ActionResult<List<Order>> Get()
        {
            try
            {
                return Ok(_orderService.GetOrders());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }         
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Order> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be less than 1");

            return Ok(_orderService.FindOrderById(id));
        }

        // POST: api/Orders
        [HttpPost]
        public ActionResult<Order> Post([FromBody] Order order)
        {
            try
            {
                return Ok(_orderService.CreateOrder(order));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
                return BadRequest("Parameter id and order Id must be the same");

            return Ok(_orderService.UpdateOrder(id, order));    
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        { 
            var oDelete = _orderService.DeleteOrder(id);

            return Ok($"Order with id {id} was deleted");   
        }
    }
}
