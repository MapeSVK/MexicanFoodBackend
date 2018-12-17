using System;
using System.Collections.Generic;
using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.Entities;
using Microsoft.AspNetCore.Authorization;
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

        /**
         * Returns a list of Orders.
         */
        // GET: api/Orders
        //[Authorize(Roles = "Administrator")]
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

        /**
         * Takes an int id, checks that the id is greater then 0,
         * if it is not, returns BadRequest. Otherwise requests the
         * service for an Order with the passed id.
         */
        // GET: api/Orders/5
        [Authorize(Roles = "Administrator")]
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Order> Get(int id)
        {
            if (id < 1)
                return BadRequest("Id cannot be less than 1");

            return Ok(_orderService.GetOrderById(id));
        }

        /**
         * Requests the service to create the Order created from the
         * passed [FromBody].
         * Returns the created Order.
         */
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

        /**
         * Takes an int id and creates an Order from the passed [FromBody],
         * checks if the id or the created orders id are greater then 0, if either
         * are not, returns BadRequest. otherwise requests the service to update
         * the Order created.
         * Returns the Order created.
         */
        // PUT: api/Orders/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Order> Put(int id, [FromBody] Order order)
        {
            if (id < 1 || id != order.Id)
                return BadRequest("Parameter id and order Id must be the same");

            return Ok(_orderService.UpdateOrder(id, order));
        }

        /**
         * Takes an int id and requests the service to delete the specified order.
         * Returns the Order to be deleted.
         */
        // DELETE: api/ApiWithActions/5
        //[Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Order> Delete(int id)
        {
            var oDelete = _orderService.DeleteOrder(id);

            return Ok($"Order with id {id} was deleted");
        }
    }
}