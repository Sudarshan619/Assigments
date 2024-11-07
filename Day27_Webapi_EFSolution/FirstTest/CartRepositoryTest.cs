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
    internal class CartRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        CartRepository repository;
        Mock<ILogger<CartRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<CartRepository>>();
            repository = new CartRepository(context, logger.Object);
        }


        [TestCase(1, "2024-10-20", 1)]
        [TestCase(2, "2024-10-21", 2)]
        public async Task TestAdd(int customerId, string dateTimeStr, int id)
        {
            // Arrange
            DateTime CartDate = DateTime.Parse(dateTimeStr); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = customerId,
                CreationDate = CartDate,
                
            };

            // Act
            var result = await repository.Add(Cart);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        [TestCase(1, "2024-10-20", 1)]
        public async Task TestAddException(int customerId, string dateTimeStr, int id)
        {
            //Arrange
            DateTime CartDate = DateTime.Parse(dateTimeStr); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = customerId,
                CreationDate = CartDate,

            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(Cart));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGet(int id, int res)
        {
            //Arrange
            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

            };
            //Act
            await repository.Add(Cart);
            var result = await repository.Get(id);
            //Assert
            Assert.AreEqual(result.Id, res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGetException(int num, int res)
        {
            //Arrange
            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

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
            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

            };
            //Act
            await repository.Add(Cart);
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
            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

            };
            //Act
            await repository.Add(Cart);
            var result = await repository.Delete(id);
            //Assert
            Assert.AreEqual(result.Id, res);
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
            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

            };

            
            DateTime CartDate1 = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart1 = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate1,

            };
            //Act

            await repository.Add(Cart);


            var result = await repository.Update(id, Cart1);
            //Assert
            Assert.AreEqual(result.Id, res);
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdateException(int id)
        {
            //Arrange

            DateTime CartDate = DateTime.Parse("2024-10-20"); // Convert string to DateTime
            Cart Cart = new Cart
            {
                CustomerId = 1,
                CreationDate = CartDate,

            };
            //Act
            //var result = await repository.Update(id, Cart1);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(id, Cart));
        }

    }
}
