using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TestProject1
{
    public class QuestionRepositoryTest
    {
        private DbContextOptions<QuizContext> _options;
        private QuizContext _context;
        private QuestionRepository _repository;
        private Mock<ILogger<QuestionRepository>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB for each test
                .Options;
            _context = new QuizContext(_options);
            _loggerMock = new Mock<ILogger<QuestionRepository>>();
            _repository = new QuestionRepository(_context, _loggerMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddQuestion_ShouldAddQuestionSuccessfully()
        {
            // Arrange
            var question = new Question
            {
                QuestionName = "What is the capital of France?",
                Category = Categories.Geography
            };

            // Act
            var addedQuestion = await _repository.Add(question);

            // Assert
            Assert.IsNotNull(addedQuestion);
            Assert.AreEqual(question.QuestionName, addedQuestion.QuestionName);
            Assert.AreEqual(question.Category, addedQuestion.Category);
            Assert.Greater(addedQuestion.QuestionId, 0);
        }

        [Test]
        public void AddQuestion_ShouldThrowCouldNotAddException_OnDbUpdateException()
        {
            // Arrange
            var question = new Question
            {
                QuestionName = "Invalid Question",
                Category = Categories.Geography
            };

            _context = new QuizContext(new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase("ThrowExceptionDB")
                .Options);
            var faultyRepository = new QuestionRepository(_context, _loggerMock.Object);
            _context.Dispose(); 

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await faultyRepository.Add(question));
        }

        [Test]
        public async Task GetQuestion_ShouldReturnQuestion_WhenExists()
        {
            // Arrange
            var question = new Question
            {
                QuestionName = "What is the capital of Germany?",
                Category = Categories.Geography
            };
            var addedQuestion = await _repository.Add(question);

            // Act
            var retrievedQuestion = await _repository.Get(addedQuestion.QuestionId);

            // Assert
            Assert.IsNotNull(retrievedQuestion);
            Assert.AreEqual(addedQuestion.QuestionId, retrievedQuestion.QuestionId);
            Assert.AreEqual(addedQuestion.QuestionName, retrievedQuestion.QuestionName);
        }

        [Test]
        public void GetQuestion_ShouldThrowNotFoundException_WhenDoesNotExist()
        {
            // Act & Assert
           Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
          
        }

        [Test]
        public async Task GetAllQuestions_ShouldReturnAllQuestions_WhenExist()
        {
            // Arrange
            var question1 = new Question
            {
                QuestionName = "What is the capital of Spain?",
                Category = Categories.Geography
            };
            var question2 = new Question
            {
                QuestionName = "Where is india?",
                Category = Categories.Geography
            };
            await _repository.Add(question1);
            await _repository.Add(question2);

            // Act
            var questions = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(questions);
            Assert.AreEqual(2, ((System.Collections.ICollection)questions).Count);
        }

        [Test]
        public void GetAllQuestions_ShouldThrowCollectionEmptyException_WhenNoQuestions()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
            Assert.AreEqual("Collection empty.", ex.Message);
        }

        [Test]
        public async Task DeleteQuestion_ShouldDeleteQuestionSuccessfully()
        {
            // Arrange
            var question = new Question
            {
                QuestionName = "What is the capital of Italy?",
                Category = Categories.Geography
            };
            var addedQuestion = await _repository.Add(question);

            // Act
            var deletedQuestion = await _repository.Delete(addedQuestion.QuestionId);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(addedQuestion.QuestionId));

            // Assert
            Assert.IsNotNull(deletedQuestion);
            Assert.AreEqual(addedQuestion.QuestionId, deletedQuestion.QuestionId);
            Assert.AreEqual("Question not found.", ex.Message);
        }

        [Test]
        public void DeleteQuestion_ShouldThrowNotFoundException_WhenDoesNotExist()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(999));
            Assert.AreEqual("Question not found.", ex.Message);
        }

        [Test]
        public async Task UpdateQuestion_ShouldUpdateSuccessfully()
        {
            // Arrange
            var question = new Question
            {
                QuestionName = "What is the capital of Portugal?",
                Category = Categories.Geography
            };
            var addedQuestion = await _repository.Add(question);
            addedQuestion.QuestionName = "What is the capital of Portugal updated?";

            // Act
            var updatedQuestion = await _repository.Update(addedQuestion.QuestionId, addedQuestion);

            // Assert
            Assert.IsNotNull(updatedQuestion);
            Assert.AreEqual("What is the capital of Portugal updated?", updatedQuestion.QuestionName);
        }

        [Test]
        public void UpdateQuestion_ShouldThrowArgumentException_OnIdMismatch()
        {
            // Arrange
            var question = new Question
            {
                QuestionId = 1,
                QuestionName = "Initial Question",
                Category = Categories.Geography
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repository.Update(2, question));
            Assert.AreEqual("Question ID mismatch.", ex.Message);
        }
    }
}
