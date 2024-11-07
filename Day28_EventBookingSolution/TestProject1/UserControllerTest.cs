using AutoMapper;
using Day28_EventBooking.DTO;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.Repository;
using Day28_EventBooking.Services;
using Day28_EventBooking.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Day28_EventBooking.Context;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestProject1
{
    public class UserControllerTest
    {
        DbContextOptions options;
        EventBookingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> userLoggerRepo;
        Mock<ILogger<UserService>> userLoggerService;
        Mock<ILogger<UserController>> userLoggerController;
        Mock<IMapper> mapper;
        IUserService UserService;
        

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<EventBookingContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new EventBookingContext(options);
            userLoggerRepo = new Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, userLoggerRepo.Object);
            mapper = new Mock<IMapper>();
            userLoggerService = new Mock<ILogger<UserService>>();
            UserService = new UserService(repository, userLoggerService.Object);
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
                Role = Roles.Cutsomer
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
                Role = Roles.Cutsomer
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
