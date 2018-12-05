using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class OrderRepository: IRepository<Order>
    {
        readonly MexicanFoodContext _ctx;
        public OrderRepository(MexicanFoodContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Order> ReadAll()
        {
            //Might need refactoring if filter is added etc
            return _ctx.Orders;
        }

        public Order ReadById(int id)
        {
            return _ctx.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Meal)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order CreateEntity(Order order)
        {
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        public Order UpdateEntity(Order orderUpdate)
        {
            var newOrderLines = new List<OrderLine>();

            if (orderUpdate.OrderLines != null)
            {
                //Clone orderlines to new location in memory, so they are not overridden on Attach
                newOrderLines = new List<OrderLine>(orderUpdate.OrderLines);
            }
            
            //Attach order so basic properties are updated
            _ctx.Attach(orderUpdate).State = EntityState.Modified;
           
            //Remove all orderlines with updated order information
            _ctx.OrderLines.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.OrderId == orderUpdate.Id)
            );
            
            //Add all orderlines with updated order information
            foreach (var ol in newOrderLines)
            {
                _ctx.Entry(ol).State = EntityState.Added;
            }

            _ctx.SaveChanges();

            return orderUpdate;       
        }

        public Order DeleteEntity(int id)
        {
            var deletedOrder = _ctx.Orders.Remove(new Order { Id = id }).Entity;
            _ctx.SaveChanges();
            return deletedOrder;
        }
    }
}
