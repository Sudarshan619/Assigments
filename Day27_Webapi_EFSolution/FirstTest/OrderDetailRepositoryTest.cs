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
    internal class OrderDetailRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        OrderDetailRepository repository;
        Mock<ILogger<OrderDetailRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<OrderDetailRepository>>();
            repository = new OrderDetailRepository(context, logger.Object);
        }


        [TestCase(1, 1, 4, 100, 1)]
        [TestCase(2, 2, 4, 100, 2)]
        public async Task TestAdd(int orderNumber, int productId, int quantity, float price, int id)
        {
            // Arrange
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = orderNumber,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            // Act
            var result = await repository.Add(OrderDetail);

            // Assert
            Assert.AreEqual(id, result.SNo);
        }

        [Test]
        [TestCase(1, 1, 4, 100, 1)]
        [TestCase(2, 2, 4, 100, 2)]
        public async Task TestAddException(int orderNumber, int productId, int quantity, float price, int id)
        {
            //Arrange
            
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = orderNumber,
                ProductId = productId,
                Quantity = quantity,
                Price = price
            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(OrderDetail));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGet(int id, int res)
        {
            //Arrange
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };
            //Act
            await repository.Add(OrderDetail);
            var result = await repository.Get(id);
            //Assert
            Assert.AreEqual(result.SNo, res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGetException(int num, int res)
        {
            //Arrange
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
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
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };
            //Act
            await repository.Add(OrderDetail);
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
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };
            //Act
            await repository.Add(OrderDetail);
            var result = await repository.Delete(id);
            //Assert
            Assert.AreEqual(result.SNo, res);
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
            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };

            OrderDetail OrderDetail1 = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };
            //Act

            await repository.Add(OrderDetail);


            var result = await repository.Update(id, OrderDetail1);
            //Assert
            Assert.AreEqual(result.SNo, res);
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdateException(int id)
        {
            //Arrange

            OrderDetail OrderDetail = new OrderDetail
            {
                OrderNumber = 1,
                ProductId = 1,
                Quantity = 10,
                Price = 100
            };
            //Act
            //var result = await repository.Update(id, OrderDetail1);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(id, OrderDetail));
        }

    }
}
