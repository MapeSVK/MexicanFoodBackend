using MexicanFood.Core.ApplicationService;
using MexicanFood.Core.ApplicationService.Implementation;
using MexicanFood.Core.DomainService;
using MexicanFood.Core.Entities;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitTestProject1
{
    public class OrderServiceTest
    {
        [Fact]
        public void DeleteOrderShouldCallOrderRepositoryOnceTest()
        {
            var orderRepository = new Mock<IRepository<Order>>();

            orderRepository.Setup(m => m.DeleteEntity(1));

            IOrderService service = new OrderService(orderRepository.Object);

            service.DeleteOrder(1);

            orderRepository.Verify(m => m.DeleteEntity(1), Times.Once);
        }

        [Fact]
        public void UdpateOrderShouldCallOrderRepositoryOnceTest()
        {
            var orderRepository = new Mock<IRepository<Order>>();

            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns(new Order() { Id = 1 });

            IOrderService service = new OrderService(orderRepository.Object);

            var order = new Order()
            {
                CustomerName = "Hala"
            };

            service.UpdateOrder(1, order);

            orderRepository.Verify(m => m.UpdateEntity(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void CreateOrderShouldCallOrderRepositoryOnceTest()
        {
            var orderRepository = new Mock<IRepository<Order>>();

            orderRepository.Setup(m => m.ReadById(It.IsAny<int>())).Returns(new Order() { Id = 1 });

            IOrderService service = new OrderService(orderRepository.Object);

            var order = new Order()
            {
                CustomerName = "Hala",
                MobileNumber = "22",
                Comment = "hi",
                OrderedDateAndTime = DateTime.Now,
                PickUpDateAndTime = DateTime.Now
            };

            service.CreateOrder(order);

            orderRepository.Verify(m => m.CreateEntity(It.IsAny<Order>()), Times.Once);
        }

        [Fact]
        public void GetOrdersShouldCallOrderRepositoryOncetest()
        {
            var orderRepository = new Mock<IRepository<Order>>();

            orderRepository.Setup(m => m.ReadAll()).Returns(new List<Order>());

            IOrderService service = new OrderService(orderRepository.Object);

            service.GetOrders();

            orderRepository.Verify(m => m.ReadAll(), Times.Once);
        }

        [Fact]
        public void OrderFoundByIdShouldCallOrderRepositoryOncetest()
        {
            var orderRepository = new Mock<IRepository<Order>>();

            orderRepository.Setup(m => m.ReadById(1));

            IOrderService service = new OrderService(orderRepository.Object);

            service.GetOrderById(1);

            orderRepository.Verify(m => m.ReadById(1), Times.Once);
        }

    }
}
