using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Castle.Core.Smtp;

namespace TestProject1
{
    class UserRepositoryTest
    {
        DbContextOptions options;
        QuizContext context;
        UserRepository repository;
        Mock<ILogger<UserRepository>> logger;
        Mock<IEmailSender> mockEmailSender;
        Mock<ILogger<EmailService>> mockLogger;
        Mock<IOptions<SmtpSettings>> smtpSettingsMock;

        [SetUp]
        public void Setup()
        {
            
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase("TestAdd")
                .Options;

            context = new QuizContext(options);

            // Mock dependencies
            logger = new Mock<ILogger<UserRepository>>();
            mockLogger = new Mock<ILogger<EmailService>>();
            mockEmailSender = new Mock<IEmailSender>();

            smtpSettingsMock = new Mock<IOptions<SmtpSettings>>();
            smtpSettingsMock.Setup(x => x.Value).Returns(new SmtpSettings
            {
                Server = "smtp.test.com",
                Port = 587,
                SenderEmail = "test@example.com",
                SenderName = "Test Sender",
                Username = "testuser",
                Password = "testpassword",
                EnableSsl = true
            });

            
            var mockEmailService = new EmailService(smtpSettingsMock.Object, mockLogger.Object);

            // Create UserRepository instance
            repository = new UserRepository(context, logger.Object, mockEmailService);
        }

        [Test]
        public async Task TestAdd()
        {
            var user = new User
            {
                Name = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                Email = "testuser@example.com",
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            var addedUser = await repository.Add(user);
            Assert.IsTrue(addedUser.Name == user.Name);
        }

        [Test]
        public async Task ExceptionTestAdd()
        {
            var user = new User
            {
                Name = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                Email = "invalidemail",  // Invalid email to trigger error
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(user));
        }

        [Test]
        public async Task TestGet()
        {
            var user = new User
            {
                Name = "TestUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                Email = "testuser@example.com",
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);

            var getUser = await repository.Get(user.Name);
            Assert.IsNotNull(getUser);
        }

        [Test]
        public async Task TestDelete()
        {
            var user = new User
            {
                Name = "abc",
                Email = "abc@example.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);

            var deletedUser = await repository.Delete(user.Name);
            Assert.IsNotNull(deletedUser);
        }

        [Test]
        public async Task TestUpdate()
        {
            var user = new User
            {
                Name = "abc",
                Email = "abc@example.com",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            var newUser = new User
            {
                Name = "abc",
                Email = "newabc@example.com",
                Password = Encoding.UTF8.GetBytes("UpdatedPassword"),
                HashKey = Encoding.UTF8.GetBytes("UpdatedHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);

            var updatedUser = await repository.Update("abc", newUser);
            Assert.IsNotNull(updatedUser);
        }

        [Test]
        public async Task ExceptionTestGet()
        {
            var user = new User
            {
                Name = "NonExistentUser",
                Password = Encoding.UTF8.GetBytes("TestPassword"),
                Email = "nonexistent@example.com",
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(user.Name));
        }
    }
}
