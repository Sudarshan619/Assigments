using AutoMapper;
using Day29_BackendPlan.Controllers;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Context;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Services;
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
        InsuranceContext context;
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
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
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
                Role = Roles.PolicyHolder
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
                Role = Roles.PolicyHolder
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
