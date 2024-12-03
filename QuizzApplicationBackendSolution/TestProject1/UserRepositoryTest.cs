using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using System.Text;

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
            Server = "smtp.example.com",
            Port = 587,
            Username = "username",
            Password = "password",
            EnableSsl = true
        });

        var emailService = new EmailService(smtpSettingsMock.Object, mockLogger.Object);
        repository = new UserRepository(context, logger.Object, emailService);
    }

    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    public async Task Add_ShouldThrowCouldNotAddException_WhenEmailServiceFails()
    {
        var user = new User
        {
            Name = "TestUser",
            Email = "testuser@example.com",
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        smtpSettingsMock.Setup(x => x.Value.Server).Throws(new Exception("Email service error"));

        Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(user));
    }

    [Test]
    public async Task Get_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var user = await repository.Get("NonExistentUser");
        Assert.IsNull(user);
    }

    [Test]
    public async Task Update_ShouldThrowException_WhenErrorOccursDuringUpdate()
    {
        var user = new User
        {
            Name = "TestUser",
            Email = "email@example.com",
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        context.Database.EnsureDeleted(); // Simulate a database failure.

        Assert.ThrowsAsync<Exception>(async () => await repository.Update("TestUser", user));
    }

    [Test]
    public async Task Delete_ShouldThrowException_WhenErrorOccursDuringDeletion()
    {
        var user = new User
        {
            Name = "TestUser",
            Email = "email@example.com",
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user);

        context.Database.EnsureDeleted(); // Simulate a database failure.

        Assert.ThrowsAsync<Exception>(async () => await repository.Delete("TestUser"));
    }

    [Test]
    public async Task Add_ShouldNotAddUser_WhenDuplicateUserExists()
    {
        var user1 = new User
        {
            Name = "TestUser",
            Email = "testuser@example.com",
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        var user2 = new User
        {
            Name = "TestUser",
            Email = "duplicateuser@example.com",
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user1);

        Assert.ThrowsAsync<Exception>(async () => await repository.Add(user2));
    }

    [Test]
    public async Task Get_ShouldReturnCorrectUser_WhenMultipleUsersExist()
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
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user1);
        await repository.Add(user2);

        var fetchedUser = await repository.Get("User2");
        Assert.AreEqual(user2.Name, fetchedUser.Name);
    }

    [Test]
    public async Task Update_ShouldOnlyUpdateSpecifiedUser_WhenMultipleUsersExist()
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
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user1);
        await repository.Add(user2);

        var updatedUser = new User
        {
            Name = "User2",
            Email = "updateduser2@example.com",
            Password = Encoding.UTF8.GetBytes("UpdatedPassword"),
            HashKey = Encoding.UTF8.GetBytes("UpdatedHashKey"),
            Role = Roles.QuizzCreator
        };

        var result = await repository.Update("User2", updatedUser);
        Assert.AreEqual(updatedUser.Email, result.Email);

        var unaffectedUser = await repository.Get("User1");
        Assert.AreEqual(user1.Email, unaffectedUser.Email);
    }

    [Test]
    public async Task Delete_ShouldOnlyDeleteSpecifiedUser_WhenMultipleUsersExist()
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
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user1);
        await repository.Add(user2);

        await repository.Delete("User2");

        Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get("User2"));
        var existingUser = await repository.Get("User1");
        Assert.IsNotNull(existingUser);
    }
}
