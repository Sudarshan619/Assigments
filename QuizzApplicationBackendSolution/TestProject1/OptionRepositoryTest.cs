using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
    public class OptionRepositoryTest
    {
        DbContextOptions<QuizContext> options;
        QuizContext context;
        OptionRepository repository;
        QuestionRepository questionRepository;
        Mock<ILogger<OptionRepository>> logger;
        Mock<ILogger<QuestionRepository>> loggerMock;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<OptionRepository>>();
            loggerMock = new Mock<ILogger<QuestionRepository>>();
            repository = new OptionRepository(context, logger.Object);
            questionRepository = new QuestionRepository(context, loggerMock.Object);
        }

        [Test]
        public async Task TestAddOption()
        {
          
            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Test",
                IsCorrect = true
            };
            var addedOption = await repository.Add(option);

            Assert.AreEqual(option.Text, addedOption.Text);
        }

        [Test]
        public async Task ExceptionTestAddOption()
        {
            
            Option option = new Option()
            {
                QuestionId = 999,
                Text = "Test",
                IsCorrect = true
            };
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(option));
        }

        [Test]
        public async Task TestGet()
        {
            

            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Test",
                IsCorrect = true
            };
            var addedOption = await repository.Add(option);
            await context.SaveChangesAsync();

            var retrievedOption = await repository.Get(addedOption.OptionId);
            Assert.IsNotNull(retrievedOption);
            Assert.AreEqual(addedOption.OptionId, retrievedOption.OptionId);
        }

        [Test]
        public void ExceptionTestGet()
        {
            // Test retrieval of a non-existent OptionId to trigger exception
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAllOptionTest()
        {
            
            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Test",
                IsCorrect = true
            };

            await repository.Add(option);

            var options = await repository.GetAll();
            Assert.IsNotNull(options);
        }

        [Test]
        public async Task ExceptionGetAllOptionTest()
        {
            // Ensure no options are added
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }
    }
}
