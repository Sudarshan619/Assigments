using AutoMapper;
using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.Controllers;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTest
{
    public class ProductControllerTest
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductRepository repository;
        Mock<ILogger<ProductRepository>> logger;
        private Mock<ILogger<ProductController>> loggerController;
        Mock<IMapper> mapper;
        IProductService productService;
        ProductDTO product;
        Product productEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductRepository>>();
            loggerController = new Mock<ILogger<ProductController>>();
            repository = new ProductRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            productService = new ProductService(repository, mapper.Object);
        }

        [Test]
        public async Task AddProductTest()
        {
            // Arrange
            var product = new ProductDTO
            {
                Name = "Test Product",
                PricePerUnit = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);//dummying the method to return the result for testing
            var controller = new ProductController(productService, loggerController.Object);
            // Act
            var result = await controller.AddProduct(product);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        [TestCase(1,200)]
        public async Task GetProductTest(int id,int res)
        {
            // Arrange
            var product = new ProductDTO
            {
                Name = "Test Product",
                PricePerUnit = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);//dummying the method to return the result for testing
            var controller = new ProductController(productService, loggerController.Object);
            // Act
             await controller.AddProduct(product);
            var result = await controller.GetProduct(id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }

        [Test]
        [TestCase(200)]
        public async Task GetProductAllTest(int res)
        {
            // Arrange
            var product = new ProductDTO
            {
                Name = "Test Product",
                PricePerUnit = 10.0f,
                Quantity = 100
            };
            var productEntity = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Quantity = 100
            };
            mapper.Setup(m => m.Map<Product>(product)).Returns(productEntity);//dummying the method to return the result for testing
            var controller = new ProductController(productService, loggerController.Object);
            // Act
            await controller.AddProduct(product);
            var result = await controller.GetAllProducts();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }
    }
}
