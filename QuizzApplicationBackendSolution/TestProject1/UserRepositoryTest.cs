using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class UserRepositoryTest
    {
        private DbContextOptions<QuizContext> options;
        private QuizContext context;
        private UserRepository repository;
        private Mock<ILogger<UserRepository>> logger;
        private Mock<ILogger<EmailService>> mockLogger;
        private Mock<IOptions<SmtpSettings>> smtpSettingsMock;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            context = new QuizContext(options);
            logger = new Mock<ILogger<UserRepository>>();
            mockLogger = new Mock<ILogger<EmailService>>();
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

            var emailService = new EmailService(smtpSettingsMock.Object, mockLogger.Object);
            repository = new UserRepository(context, logger.Object, emailService);
        }

        [Test]
        public async Task Add_ShouldThrowCouldNotAddException_WhenEmailIsInvalid()
        {
            var user = new User
            {
                Name = "InvalidUser",
                Email = "invalidemail",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(user));
        }

        [Test]
        public async Task Get_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get("NonExistentUser"));
        }

        [Test]
        public async Task Update_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            var user = new User
            {
                Name = "NonExistentUser",
                Email = "newemail@example.com",
                Password = Encoding.UTF8.GetBytes("UpdatedPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update("NonExistentUser", user));
        }

        [Test]
        public async Task Delete_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete("NonExistentUser"));
        }

        [Test]
        public async Task GetAll_ShouldThrowNotFoundException_WhenNoUsersExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task Add_ShouldAddUser_WhenUserIsValid()
        {
            var user = new User
            {
                Name = "TestUser",
                Email = "testuser@example.com",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            var addedUser = await repository.Add(user);
            Assert.AreEqual(user.Name, addedUser.Name);
        }

        [Test]
        public async Task Get_ShouldReturnUser_WhenUserExists()
        {
            var user = new User
            {
                Name = "TestUser",
                Email = "testuser@example.com",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);
            var fetchedUser = await repository.Get(user.Name);
            Assert.IsNotNull(fetchedUser);
            Assert.AreEqual(user.Name, fetchedUser.Name);
        }

        [Test]
        public async Task Update_ShouldUpdateUser_WhenUserExists()
        {
            var user = new User
            {
                Name = "TestUser",
                Email = "oldemail@example.com",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            var newUser = new User
            {
                Name = "TestUser",
                Email = "newemail@example.com",
                Password = Encoding.UTF8.GetBytes("UpdatedPassword"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);
            var updatedUser = await repository.Update(user.Name, newUser);
            Assert.AreEqual(newUser.Email, updatedUser.Email);
        }

        [Test]
        public async Task Delete_ShouldDeleteUser_WhenUserExists()
        {
            var user = new User
            {
                Name = "TestUser",
                Email = "testuser@example.com",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user);
            var deletedUser = await repository.Delete(user.Name);
            Assert.AreEqual(user.Name, deletedUser.Name);
        }

        [Test]
        public async Task GetAll_ShouldReturnAllUsers_WhenUsersExist()
        {
            var user1 = new User
            {
                Name = "User1",
                Email = "user1@example.com",
                Password = Encoding.UTF8.GetBytes("Password"),
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Role = Roles.QuizzCreator
            };
            var user2 = new User
            {
                Name = "User2",
                Email = "user2@example.com",
                HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
                Password = Encoding.UTF8.GetBytes("Password"),
                Role = Roles.QuizzCreator
            };

            await repository.Add(user1);
            await repository.Add(user2);

            var users = await repository.GetAll();
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count());
        }
    }
}
