using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Context;
using Microsoft.Extensions.Configuration;
using NETCore.MailKit.Core;


namespace TestProject1
{
    internal class UserControllerTest
    {
        DbContextOptions options;
        QuizContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> userLoggerRepo;
        Mock<ILogger<UserService>> userLoggerService;
        Mock<ILogger<UserController>> userLoggerController;
        Mock<IMapper> mapper;
        IAuthentication UserService;
        Mock<QuizzApplicationBackend.Services.EmailService> _emailService;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new QuizContext(options);
            userLoggerRepo = new Mock<ILogger<UserRepository>>();
            _emailService = new Mock<QuizzApplicationBackend.Services.EmailService>();
            repository = new UserRepository(context, userLoggerRepo.Object,_emailService.Object);
            mapper = new Mock<IMapper>();
            userLoggerService = new Mock<ILogger<UserService>>();
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            UserService = new UserService(repository, mockTokenService.Object, userLoggerService.Object, mapper.Object);
            userLoggerController = new Mock<ILogger<UserController>>();
        }

        [Test]
        public async Task AddUserTest()
        {
            //Arrange
            var user = new CreateUserDTO()
            {
                Username = "Test1",
                Password = "TestPass",
                Email = "",
                Role = Roles.QuizzCreator
            };

            // Assert
            var controller = new UserController(userLoggerController.Object, UserService);

            var result = await controller.Register(user);
            Assert.IsNotNull(result);

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
            var user = new CreateUserDTO()
            {
                Username = "Test1",
                Password = "TestPass",
                Email = "",
                Role = Roles.QuizzCreator,
            };

            // Assert
            var controller = new UserController(userLoggerController.Object, UserService);

            await controller.Register(user);

            var user1 = new LoginRequestDTO()
            {
                UserName = "Test1",
                Password = "TestPass"
            };
            var result = await controller.Login(user1);
            Assert.IsNotNull(result);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);

            // Assert the status code
            var loginResponse = okResult.Value as LoginResponseDTO;
            Assert.IsNotNull(loginResponse);
            Assert.AreEqual(200, loginResponse.StatusCode);
        }
    }
}
