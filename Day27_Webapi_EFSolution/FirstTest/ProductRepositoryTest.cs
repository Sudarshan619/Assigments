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

namespace FirstTest
{
    internal class ProductRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductRepository repository;
        Mock<ILogger<ProductRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductRepository>>();
            repository = new ProductRepository(context,logger.Object);
        }


        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product", 1)]
        [TestCase("TestAdd2", 120, 4, "", "Test description for Product", 2)]
        public async Task TestAdd(string name, float price, int quantity, string image, string desc, int id)
        {
            //Arrange
            Product product = new Product
            {
                Name = name,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };
            //Act
            var result = await repository.Add(product);
            //Assert
            Assert.AreEqual(id, result.Id);
        }

        [Test]
        [TestCase("TestAdd1", 120, 4, "", "Test description for Product")]

        public async Task TestAddException(string name, float price, int quantity, string image, string desc)
        {
            //Arrange
            Product product = new Product
            {
                Name = null,
                Price = price,
                Quantity = quantity,
                BasicImage = image,
                Description = desc
            };

            //Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(product));
        }


        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 3)]
        public async Task TestGet(int id, int res)
        {
            //Arrange
            Product product = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };
            //Act
            await repository.Add(product);
            var result = await repository.Get(id);
            //Assert
            Assert.AreEqual(result.Id, res);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,2)]
        [TestCase(3,3)]
        public async Task TestGetException(int num,int res)
        {
            //Arrange
            Product product = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
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
            Product product = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };
            //Act
            await repository.Add(product);
            var result = await repository.GetAll();
            //Assert
            Assert.AreEqual(num,result.Count());
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
            Product product = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };
            //Act
            await repository.Add(product);
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
        [TestCase(1,1)]
        public async Task TestUpdate(int id, int res)
        {
            //Arrange
            Product product = new Product
            {
                Name = "sudu1",
                Price = 101,
                Quantity = 52,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };

            Product product1 = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };
            //Act

            await repository.Add(product);


            var result = await repository.Update(id,product1);
            //Assert
            Assert.AreEqual(result.Id, res);
        }

        [Test]
        [TestCase(1)]
        public async Task TestUpdateException(int id)
        {
            //Arrange
            
            Product product1 = new Product
            {
                Name = "sudu",
                Price = 10,
                Quantity = 5,
                BasicImage = "dhjhdj",
                Description = "dshdsd"
            };
            //Act
            //var result = await repository.Update(id, product1);
            //Assert
            Assert.ThrowsAsync<NotFoundException>(async ()=>  await repository.Update(id,product1));
        }

    }
}
