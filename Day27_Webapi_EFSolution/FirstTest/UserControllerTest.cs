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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTest
{
    public class UserControllerTest
    {
        DbContextOptions options;
        ShoppingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> userLoggerRepo;
        Mock<ILogger<UserService>> userLoggerService;
        Mock<ILogger<UserController>> userLoggerController;
        Mock<IMapper> mapper;
        IUserService UserService;
        UserCreateDTO User;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;
        User UserEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            userLoggerRepo = new Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, userLoggerRepo.Object);
            mapper = new Mock<IMapper>();
            userLoggerService = new Mock<ILogger<UserService>>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            UserService = new UserService(repository, mockTokenService.Object, userLoggerService.Object);
            userLoggerController = new Mock<ILogger<UserController>>();
        }

        [Test]
        public async Task AddUserTest()
        {
            //Arrange
            var user = new UserCreateDTO()
            {
                Username = "Test1",
                Password = "TestPass",
                Role = Roles.Customer
            };

            // Assert
            var controller = new UserController(userLoggerController.Object, UserService);

            var result = await controller.Register(user);
            Assert.IsNotNull(result);

            // Cast the result to ObjectResult to access the status code and value
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Assert the status code
            var loginResponse = okResult.Value as LoginResponseDTO;
            Assert.IsNotNull(loginResponse);
            Assert.AreEqual(200, loginResponse.StatusCode);

        }
        [Test]
        public async Task AuthenticateUserTest()
        {
            //Arrange
            var user = new UserCreateDTO()
            {
                Username = "Test1",
                Password = "TestPass",
                Role = Roles.Customer
            };

            // Assert
            var controller = new UserController(userLoggerController.Object, UserService);

            await controller.Register(user);

            var user1 = new LoginRequestDTO()
            {
                Username = "Test1",
                Password = "TestPass"
            };
            var result = await controller.Login(user1);
            Assert.IsNotNull(result);

            // Cast the result to ObjectResult to access the status code and value
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Assert the status code
            var loginResponse = okResult.Value as LoginResponseDTO;
            Assert.IsNotNull(loginResponse);
            Assert.AreEqual(200, loginResponse.StatusCode);
        }
    }
}
