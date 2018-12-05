using MexicanFood.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MexicanFood.Core.ApplicationService
{
    public interface IOrderService
    {
        List<Order> GetOrders();

        Order GetOrderById(int id);

        Order CreateOrder(Order order);

        Order UpdateOrder(int id, Order updateOrder);

        Order DeleteOrder(int id);
    }
}
