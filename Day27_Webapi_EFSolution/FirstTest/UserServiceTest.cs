using Day27_Webapi_EF.Context;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;
using Day27_Webapi_EF.Services;
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
    internal class UserServiceTest
    {
        DbContextOptions options;
        ShoppingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        Mock<ILogger<UserService>> loggerUserService;
        Mock<TokenService> mockTokenService;
        Mock<IConfiguration> mockConfiguration;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            repository = new UserRepository(context, loggerUserRepo.Object);
            mockConfiguration = new Mock<IConfiguration>();
            mockTokenService = new Mock<TokenService>(mockConfiguration.Object);
            mockTokenService.Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>())).ReturnsAsync("TestToken");

        }

        [Test]
        [TestCase("TestUser", "TestPassword", "TestHashKey", Roles.Admin)]
        public async Task TestAdd(string username, string password, string hashKey, Roles role)
        {
            var user = new UserCreateDTO
            {
                Username = username,
                Password = password,
                Role = role
            };
            var userService = new UserService(repository,mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [TestCase("TestUser", "TestPassword", "TestHashKey")]
        public async Task TestAuthenticate(string username, string password, string hashKey)
        {
            var user = new UserCreateDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Role = Roles.Admin
            };
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object);
            var addedUser = await userService.Register(user);
            var loggedInUser = await userService.Autheticate(new LoginRequestDTO
            {
                Username = user.Username,
                Password = user.Password
            });
            Assert.IsTrue(addedUser.Username == loggedInUser.Username);
        }


    }
}
