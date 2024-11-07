using Day28_EventBooking.DTO;
using Day28_EventBooking.Model;
using Day28_EventBooking.Repository;
using Day28_EventBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Day28_EventBooking.Context;
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
        EventBookingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> loggerUserRepo;
        Mock<ILogger<UserService>> loggerUserService;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<EventBookingContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new EventBookingContext(options);
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            repository = new UserRepository(context, loggerUserRepo.Object);
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
            var userService = new UserService(repository, loggerUserService.Object);
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
            var userService = new UserService(repository, loggerUserService.Object);
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
