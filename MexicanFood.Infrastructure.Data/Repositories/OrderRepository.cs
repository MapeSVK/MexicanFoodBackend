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

        /**
         * Returns the Orders in the database.
         */
        public IEnumerable<Order> ReadAll()
        {
            //Might need refactoring if filter is added etc
            return _ctx.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Meal);
        }

        /**
         * Takes an int id and returns the first found order with matching id or default,
         * including OrderLine objects where id matches OrderId, linked to the the Meal
         * that matches OrderId.
         */
        public Order ReadById(int id)
        {
            return _ctx.Orders
                .Include(o => o.OrderLines)
                .ThenInclude(ol => ol.Meal)
                .FirstOrDefault(o => o.Id == id);
        }

        /**
         * Takes an Order and attaches it to the local representation of the database,
         * then saves the changes to the local database representation to the actual
         * database.
         * Returns the Order passed.
         */
        public Order CreateEntity(Order order)
        {
            _ctx.Attach(order).State = EntityState.Added;
            _ctx.SaveChanges();
            return order;
        }

        /**
         * Takes an Order and checks if its OrderLine list is empty, if it is, it will
         * update the local representation of the order, the Order passed represented,
         * with the new data, then save the changes to the real database.
         * If OrderLines is not null, the method makes a copy of the OrderLines,
         * removes the OrderLines matching the Orders id, and adds the copied entries
         * to the local representation, then saves changes to the actual database.
         * Returns the Order passed.
         * (The removal and adding is to avoid problems with the updated meals relations)
         */
        public Order UpdateEntity(Order orderUpdate)
        {
            var newOrderLines = new List<OrderLine>();

            if (orderUpdate.OrderLines != null)
            {
                //Clone orderlines to new location in memory, so they are not overridden on Attach
                newOrderLines = new List<OrderLine>(orderUpdate.OrderLines);
            }

            //Remove all orderlines with updated order information
            _ctx.RemoveRange(
                _ctx.OrderLines.Where(ol => ol.OrderId == orderUpdate.Id)
            );

            //Attach order so basic properties are updated
            _ctx.Attach(orderUpdate).State = EntityState.Modified;

            //Add all orderlines with updated order information
            foreach (var ol in newOrderLines)
            {
                _ctx.Add(ol);
            }

            _ctx.SaveChanges();

            return orderUpdate;
        }

        /**
         * Takes an int id, finds the order with matching id, saves it to a variable and
         * removes the meal with matching id in the local representation of the database,
         * then saves the changes to the actual database, then returns the meal saved to
         * the variable.
         */
        public Order DeleteEntity(int id)
        {
            var deletedOrder = _ctx.Orders.Remove(new Order { Id = id }).Entity;
            _ctx.SaveChanges();
            return deletedOrder;
        }
    }
}
