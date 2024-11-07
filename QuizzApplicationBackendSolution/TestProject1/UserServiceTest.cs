using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TestProject1
{
    internal class UserServiceTest
    {
        DbContextOptions options;
        QuizContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        Mock<ILogger<UserService>> loggerUserService;
        Mock<TokenService> mockTokenService;
        Mock<EmailService> mockEmailService;
        Mock<IConfiguration> mockConfiguration;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new QuizContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            mockEmailService = new Mock<EmailService>();
            repository = new UserRepository(context, loggerUserRepo.Object,mockEmailService.Object);
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");

        }

        [Test]
        [TestCase("TestUser", "TestPassword", "TestHashKey", Roles.QuizzCreator)]
        public async Task TestAdd(string username, string password, string hashKey, Roles role)
        {
            var user = new CreateUserDTO
            {
                Username = username,
                Password = password,
                Email = "",
                Role = role
            };
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [TestCase("TestUser", "TestPassword", "TestHashKey")]
        public async Task TestAuthenticate(string username, string password, string hashKey)
        {
            var user = new CreateUserDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "",
                Role = Roles.QuizzCreator
            };
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            var loggedInUser = await userService.Autheticate(new LoginRequestDTO
            {
                UserName = user.Username,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);
        }


    }
}
