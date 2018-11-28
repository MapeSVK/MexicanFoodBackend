using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Infrastructure.Data.Repositories
{
    public class OrderRepository: IRepository<Order>
    {
        public IEnumerable<Order> ReadAllEntities()
        {
            throw new NotImplementedException();
        }

        public Order EntityFoundById(int id)
        {
            throw new NotImplementedException();
        }

        public Order CreateEntity(Order entity)
        {
            throw new NotImplementedException();
        }

        public Order UpdateEntity(int id, Order entity)
        {
            throw new NotImplementedException();
        }

        public Order DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }
    }
}
