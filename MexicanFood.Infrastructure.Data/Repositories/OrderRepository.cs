using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class orderRepository: IRepository<Order>
    {
        readonly MexicanFoodContext _ctx;
        public orderRepository(MexicanFoodContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Order> ReadAllEntities()
        {
            return _ctx.Orders;
        }

        public Order EntityFoundById(int id)
        {
            return _ctx.Orders.FirstOrDefault(o => o.Id == id);
        }

        public Order CreateEntity(Order order)
        {
            _ctx.Add(order);
            _ctx.SaveChanges();
            return order;
        }

        public Order UpdateEntity(Order order)
        {
            _ctx.Update(order);
            _ctx.SaveChanges();
            return order;
        }

        public Order DeleteEntity(int id)
        {
            var deletedOrder = _ctx.Orders.Remove(new Order { Id = id }).Entity;
            _ctx.SaveChanges();
            return deletedOrder;
        }
    }
}
