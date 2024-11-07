using Day28_EventBooking.Model;
using Day28_EventBooking.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text;
using Moq;
using Day28_EventBooking.Context;



namespace TestProject1
{
    class UserRepositoryTest
    {
        DbContextOptions options;
        EventBookingContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<EventBookingContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new EventBookingContext(options);
            logger = new Mock<ILogger<UserRepository>>();
            repository = new UserRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAdd()
        {
            User user = new User
            {
                UserName = "TestUser",
                //test password is the passowrd given for testing
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);
            Assert.IsTrue(addedUser.UserName == user.UserName);
        }

        [Test]
        public async Task TesGet()
        {
            User user = new User
            {
                UserName = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.Admin
            };
            var addedUser = await repository.Add(user);

            var getUser = await repository.Get(user.UserName);
            Assert.IsNotNull(getUser);
        }
    }
}