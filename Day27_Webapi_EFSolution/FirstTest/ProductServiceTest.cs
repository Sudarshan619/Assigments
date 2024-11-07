using AutoMapper;
using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Services;
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
    public class ProductServiceTest
    {
        DbContextOptions options;
        ShoppingContext context;
        ProductRepository repository;
        Mock<ILogger<ProductRepository>> logger;
        Mock<IMapper> mapper;


        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<ProductRepository>>();
            repository = new ProductRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
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
            IProductService productService = new ProductService(repository, mapper.Object);
            // Act
            var result = await productService.AddProduct(product);

            // Assert
            Assert.IsTrue(result);
        }


        [Test]
        [TestCase(1,1)]
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
            IProductService productService = new ProductService(repository, mapper.Object);
            // Act
            await productService.AddProduct(product);
            var result = await productService.GetProduct(id);

            // Assert
            Assert.AreEqual(res,result.Id);
        }

        [Test]
        [TestCase(1)]
        public async Task GetProductAllTest( int res)
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
            IProductService productService = new ProductService(repository, mapper.Object);
            // Act
            await productService.AddProduct(product);
            var result = await productService.GetAllProducts();

            // Assert
            Assert.AreEqual(res,result.Count());
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,2)]
        public async Task UpdateProductTest(int id,int res)
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
            IProductService productService = new ProductService(repository, mapper.Object);
            // Act
            await productService.AddProduct(product);
            var result = await productService.UpdateProduct(id,product);

            // Assert
            Assert.AreEqual(res,result);
        }


    }
}
