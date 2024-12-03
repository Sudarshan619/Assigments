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
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task TestDeleteOption()
        {
            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Test Delete",
                IsCorrect = false
            };
            var addedOption = await repository.Add(option);
            await context.SaveChangesAsync();

            var deletedOption = await repository.Delete(addedOption.OptionId);
            Assert.AreEqual(addedOption.OptionId, deletedOption.OptionId);
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(deletedOption.OptionId));
        }

        [Test]
        public void ExceptionTestDeleteOption()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task ExceptionTestUpdateOption()
        {
            Assert.ThrowsAsync<NotImplementedException>(async () => await repository.Update(1, new Option()));
        }

        [Test]
        public async Task TestUpdateOption()
        {
            // Arrange
            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Initial Text",
                IsCorrect = false
            };
            var addedOption = await repository.Add(option);

            Option updatedOptionData = new Option()
            {
                OptionId = addedOption.OptionId,
                Text = "Updated Text",
                IsCorrect = true
            };

            // Act
            var updatedOption = await repository.Update(addedOption.OptionId, updatedOptionData);

            // Assert
            Assert.AreEqual(updatedOptionData.Text, updatedOption.Text);
            Assert.AreEqual(updatedOptionData.IsCorrect, updatedOption.IsCorrect);
        }

        [Test]
        public void ExceptionTestUpdateOption_NullEntity()
        {
            // Arrange
            Option option = null;

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Update(1, option));
        }

        [Test]
        public void ExceptionTestUpdateOption_IdMismatch()
        {
            // Arrange
            Option option = new Option()
            {
                OptionId = 999, // Mismatch ID
                Text = "Mismatch ID Test",
                IsCorrect = false
            };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.Update(1, option));
        }

        [Test]
        public void ExceptionTestUpdateOption_NotFound()
        {
            // Arrange
            Option option = new Option()
            {
                OptionId = 999,
                Text = "Not Found Test",
                IsCorrect = false
            };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(999, option));
        }

        [Test]
        public async Task ExceptionTestUpdateOption_UnexpectedError()
        {
            // Arrange
            Option option = new Option()
            {
                QuestionId = 1,
                Text = "Initial Text",
                IsCorrect = false
            };
            var addedOption = await repository.Add(option);

            // Simulate unexpected error by disposing the context
            context.Dispose();

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Update(addedOption.OptionId, addedOption));
        }
    }
}
