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

namespace FirstTest
{
    class UserRepositoryTest
    {
        DbContextOptions options;
        ShoppingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ShoppingContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new ShoppingContext(options);
            logger = new Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            User user = new User
            {
                Username = "TestUser",
                //test password is the passowrd given for testing
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);
            Assert.IsTrue(addedUser.Username == user.Username);
        }

        [Test]
        public async Task TesGet()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get(user.Username);
            Assert.IsNotNull(getUser);
        }
    }
}
