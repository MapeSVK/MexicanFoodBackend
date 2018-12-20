using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /**
         * Checks if the Order passed has a CustomerName then requests the repository
         * to create the Order.
         * Returns the Order passed.
         */
        public Order CreateOrder(Order order)
        {
            if (string.IsNullOrEmpty(order.CustomerName))
                throw new InvalidDataException("You need to add a name");
            
            return _orderRepository.CreateEntity(order);
        }

        /**
         * Takes an int id, returns the Order with the specified id, and requests
         * the repository to delete it.
         */
        public Order DeleteOrder(int id)
        {
            return _orderRepository.DeleteEntity(id);
        }

        /**
        * Takes an int id, and requests an Order with the specified id, from
        * the repository, and returns it.
        */
        public Order GetOrderById(int id)
        {
            return _orderRepository.ReadById(id);
        }

        /**
         * Calls the repository to return all orders and converts the IEnumerable to
         * a list.
         */
        public List<Order> GetOrders()
        {
            return _orderRepository.ReadAll().ToList();
        }

        /**
         * Takes an Order and requests the repository to update the order in the database
         * with the matching id, with the new information of the passed Order.
         * Returns the passed Order.
         */
        public Order UpdateOrder(int id, Order updateOrder)
        {
            return _orderRepository.UpdateEntity(updateOrder);
        }
    }
}
