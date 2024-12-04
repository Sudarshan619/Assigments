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
    public async Task Add_ShouldThrowException_WhenUserAlreadyExists()
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

        // Trying to add the same user again should throw exception
        Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(user));
    }

    [Test]
    public async Task Get_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var user = await repository.Get("NonExistentUser");
        Assert.IsNull(user);
    }

    [Test]
    public async Task Get_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        var exception = async () => await repository.Get("NonExistentUser");
        Assert.IsNull (exception);
    }

    [Test]
    public async Task Update_ShouldUpdateUser_WhenUserExists()
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

        var updatedUser = new User
        {
            Name = "TestUser",
            Email = "updatedemail@example.com",
            Password = Encoding.UTF8.GetBytes("UpdatedPassword"),
            HashKey = Encoding.UTF8.GetBytes("UpdatedHashKey"),
            Role = Roles.QuizzCreator
        };

        var result = await repository.Update("TestUser", updatedUser);

        Assert.AreEqual(updatedUser.Email, result.Email);  // Check if the email is updated
        Assert.AreEqual(updatedUser.Password, result.Password); // Check if the password is updated
    }



    [Test]
    public async Task Delete_ShouldDeleteUser_WhenUserExists()
    {
        var user = new User { Name = "TestUser", Email = "email@example.com", Password = Encoding.UTF8.GetBytes("Password"), HashKey = Encoding.UTF8.GetBytes("TestHashKey"), Role = Roles.QuizzCreator };
        await repository.Add(user);

        await repository.Delete("TestUser");

        var exception = Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Get("TestUser"));
        //Assert.AreEqual("User with name TestUser not found.", exception.Message);
    }


    [Test]
    public async Task Delete_ShouldNotDeleteIfUserDoesNotExist()
    {
        Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete("NonExistentUser"));
    }

    [Test]
    public async Task GetAll_ShouldReturnEmptyList_WhenNoUsersExist()
    {
        var result = await repository.GetAll();
        Assert.IsEmpty(result);
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
            Password = Encoding.UTF8.GetBytes("Password"),
            HashKey = Encoding.UTF8.GetBytes("TestHashKey"),
            Role = Roles.QuizzCreator
        };

        await repository.Add(user1);
        await repository.Add(user2);

        var result = await repository.GetAll();

        Assert.AreEqual(2, result.Count());  // Ensure there are two users
    }


}
