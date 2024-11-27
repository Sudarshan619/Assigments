using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NETCore.MailKit.Core;
using NUnit.Framework;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Services;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class UserServiceTest
    {
        private DbContextOptions<QuizContext> options;
        private QuizContext context;
        private UserRepository repository;
        private Mock<ILogger<UserRepository>> loggerUserRepo;
        private Mock<ILogger<UserService>> loggerUserService;
        private Mock<ITokenService> mockTokenService;
        private Mock<QuizzApplicationBackend.Services.EmailService> mockEmailService;
        private Mock<IMapper> _mockMapper;
        [SetUp]
        public void Setup()
        {
            // In-memory database setup for isolated testing
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            context = new QuizContext(options);

            // Mock dependencies
            loggerUserRepo = new Mock<ILogger<UserRepository>>();
            loggerUserService = new Mock<ILogger<UserService>>();
            mockEmailService = new Mock<QuizzApplicationBackend.Services.EmailService>();
            mockTokenService = new Mock<ITokenService>();
            _mockMapper = new Mock<IMapper>();

            // Configure the mock TokenService to return a fixed token
            mockTokenService
                .Setup(t => t.GenerateToken(It.IsAny<UserTokenDTO>()))
                .ReturnsAsync("TestToken");

            // Initialize UserRepository with mocks
            repository = new UserRepository(context, loggerUserRepo.Object,mockEmailService.Object);
        }

        [Test]
        public async Task Register_ShouldAddUser_WhenUserIsValid()
        {
            // Arrange
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object, _mockMapper.Object);
            var newUser = new CreateUserDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "testuser@example.com",
                Role = Roles.QuizzCreator
            };

            // Act
            var result = await userService.Register(newUser);

            // Assert
            Assert.AreEqual(newUser.Username, result.Username);
            Assert.AreEqual(200, result.StatusCode);
        }

        [Test]
        public async Task Authenticate_ShouldReturnLoginResponse_WhenCredentialsAreValid()
        {
            // Arrange
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object, _mockMapper.Object);

            // Register a user for testing
            var newUser = new CreateUserDTO
            {
                Username = "TestUser",
                Password = "TestPassword",
                Email = "testuser@example.com",
                Role = Roles.QuizzCreator
            };
            await userService.Register(newUser);

            // Act
            var loginRequest = new LoginRequestDTO
            {
                UserName = newUser.Username,
                Password = newUser.Password
            };
            var result = await userService.Autheticate(loginRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TestUser", result.Username);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("TestToken", result.Token);
        }

        [Test]
        public void Authenticate_ShouldThrowNotFoundException_WhenUserDoesNotExist()
        {
            // Arrange
            var userService = new UserService(repository, mockTokenService.Object, loggerUserService.Object, _mockMapper.Object);

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () =>
                await userService.Autheticate(new LoginRequestDTO
                {
                    UserName = "NonExistentUser",
                    Password = "AnyPassword"
                }));
        }
    }
}
