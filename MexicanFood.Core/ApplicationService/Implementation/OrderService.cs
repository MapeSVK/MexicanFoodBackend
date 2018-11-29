using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;

namespace MexicanFood.Core.ApplicationService.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderService(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public Order CreateOrder(Order order)
        {
            //if (order.Meals.Count == 0)
            //    throw new InvalidDataException("You need a meal in your order");
            if (string.IsNullOrEmpty(order.CustomerName))
                throw new InvalidDataException("You need to add a name");
            return _orderRepository.CreateEntity(order);
        }

        public Order DeleteOrder(int id)
        {
            return _orderRepository.DeleteEntity(id);
        }

        public Order FindOrderById(int id)
        {
            return _orderRepository.EntityFoundById(id);
        }

        public List<Order> GetOrders()
        {
            return _orderRepository.ReadAllEntities().ToList();
        }

        public Order UpdateOrder(int id, Order updateOrder)
        {
            return _orderRepository.UpdateEntity(updateOrder);
        }
    }
}
