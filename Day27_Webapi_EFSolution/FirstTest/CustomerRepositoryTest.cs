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
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;

namespace FirstTest
{
    internal class CustomerRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        CustomerRepository repository;
        Mock<ILogger<CustomerRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<CustomerRepository>>();
            repository = new CustomerRepository(context, logger.Object);
        }


        [TestCase("sudu","s@gmail.com",20,"728372872","03-03-2002",1)]
        [TestCase("desh","d@gmail.com",21,"457972993","05-10-2003",2)]
        public async Task TestAdd(string name,string email,int age, string phone, string dateTime, int id)
        {
            // Arrange
            DateTime dateOfBirth = DateTime.Parse(dateTime);
            Customer Customer = new Customer
            {
                Name = name,
                Email = email,
                Age = age,
                Phone = phone,
                DateOfBirth = dateOfBirth,

            };

            // Act
            var result = await repository.Add(Customer);

            // Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        [TestCase("desh", "d@gmail.com", 21, "457972993", "05-10-2003", 2)]
        public async Task TestAddException(string name, string email, int age, string phone, string dateTime, int id)
        {
            //Arrange
            DateTime dateOfBirth = DateTime.Parse(dateTime);
            Customer Customer = new Customer
            {
                Name = name,
                Email = email,
                Age = age,
                Phone = phone,
                DateOfBirth = dateOfBirth,

            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(Customer));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGet(int id, int res)
        {
            //Arrange
            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

            };
            //Act
            await repository.Add(Customer);
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
            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

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
            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

            };
            //Act
            await repository.Add(Customer);
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
            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

            };
            //Act
            await repository.Add(Customer);
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
            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

            };


            DateTime dateOfBirth1 = DateTime.Parse("04-04-2002");
            Customer Customer1 = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth1,

            };
            //Act

            await repository.Add(Customer);


            var result = await repository.Update(id, Customer1);
            //Assert
            Assert.AreEqual(result.Id, res);
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdateException(int id)
        {
            //Arrange

            DateTime dateOfBirth = DateTime.Parse("04-04-2002");
            Customer Customer = new Customer
            {
                Name = "sudu",
                Email = "s@gmail.com",
                Age = 20,
                Phone = "36484634643",
                DateOfBirth = dateOfBirth,

            };
            //Act
            //var result = await repository.Update(id, Customer1);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(id, Customer));
        }

    }
}
