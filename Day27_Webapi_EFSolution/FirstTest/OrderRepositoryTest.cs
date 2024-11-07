using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Castle.Core.Resource;
using Day27_Webapi_EF.DTO;
using NUnit.Framework.Constraints;

namespace FirstTest
{
    internal class OrderRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        OrderRepository repository;
        Mock<ILogger<OrderRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<OrderRepository>>();
            repository = new OrderRepository(context, logger.Object);
        }


        [TestCase(1, "2024-10-20", 4.0f, "Test description for Order", 1)]
        [TestCase(2, "2024-10-21", 4.0f, "Test description for Order", 2)]
        public async Task TestAdd(int customerId, string dateTimeStr, float total, string status, int id)
        {
            // Arrange
            DateTime orderDate = DateTime.Parse(dateTimeStr); // Convert string to DateTime
            Order order = new Order
            {
                CustomerId = customerId,
                OrderDate = orderDate,
                TotalValue = total,
                OrderStatus = status
            };

            // Act
            var result = await repository.Add(order);

            // Assert
            Assert.AreEqual(id, result.OrderNumber);
        }

        [Test]
        [TestCase(1, "2024-10-20", 4.0f, "Test description for Order", 1)]
        public async Task TestAddException(int customerId, string dateTimeStr, float total, string status, int id)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse(dateTimeStr);
            Order order = new Order
            {
                CustomerId = customerId,
                OrderDate = orderDate,
                TotalValue = total,
                OrderStatus = status
            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(order));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGet(int id, int res)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act
            await repository.Add(order);
            var result = await repository.Get(id);
            //Assert
            Assert.AreEqual(result.OrderNumber, res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGetException(int num, int res)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act

            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(num));
        }

        [Test]
        [TestCase(1)]
        public async Task TestGetAll(int num)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act
            await repository.Add(order);
            var result = await repository.GetAll();
            //Assert
            Assert.AreEqual(num, result.Count());
        }

        [Test]
        public async Task TestGetAllException()
        {

            //Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(1, 1)]
        public async Task TestDelete(int id, int res)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act
            await repository.Add(order);
            var result = await repository.Delete(id);
            //Assert
            Assert.AreEqual(result.OrderNumber, res);
        }

        [Test]
        [TestCase(3, 3)]
        public async Task TestDeleteException(int id, int res)
        {
            //Arrange

            //Act
            //var result = await repository.Delete(id);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(id));
        }


        [Test]
        [TestCase(1, 1)]
        public async Task TestUpdate(int id, int res)
        {
            //Arrange
            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };

            DateTime orderDate1 = DateTime.Parse("2024-10-20");
            Order order1 = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate1,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act

            await repository.Add(order);


            var result = await repository.Update(id, order1);
            //Assert
            Assert.AreEqual(result.OrderNumber, res);
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdateException(int id)
        {
            //Arrange

            DateTime orderDate = DateTime.Parse("2024-10-20");
            Order order = new Order
            {
                CustomerId = 1,
                OrderDate = orderDate,
                TotalValue = 100,
                OrderStatus = null
            };
            //Act
            //var result = await repository.Update(id, Order1);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(id, order));
        }

    }
}
