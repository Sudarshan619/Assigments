using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Context;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Day29_BackendPlan.Exceptions;

namespace FirstTest
{
    class UserRepositoryTest
    {
        DbContextOptions options;
        InsuranceContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
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
        public async Task ExceptionTestAdd()
        {
            User user = new User
            {
                Username = "TestUser",
                //test password is the passowrd given for testing
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            Assert.ThrowsAsync<CouldNotAddException>(async ()=> await repository.Add(user));
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

        [Test]
        public async Task ExceptionTestGet()
        {
            User user = new User
            {
                Username = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };

            Assert.ThrowsAsync<NotFoundException>(async ()=> await repository.Get(user.Username));
        }
    }
}
