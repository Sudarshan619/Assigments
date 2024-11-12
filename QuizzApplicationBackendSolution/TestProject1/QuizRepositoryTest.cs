using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class QuizRepositoryTest
    {
        DbContextOptions options;
        QuizContext context;
        QuizRepository repository;
        Mock<ILogger<QuizRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<QuizRepository>>();
            repository = new QuizRepository(context, logger.Object);
        }

        [Test]
        public async Task TestDeleteQuiz()
        {
            Quiz quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,
            };

            var addedQuiz = await repository.Add(quiz);
            var deletedQuiz = await repository.Delete(addedQuiz.QuizId);

            Assert.AreEqual(addedQuiz.QuizId, deletedQuiz.QuizId);
        }

        [Test]
        public async Task ExceptionTestDeleteQuiz()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999)); // Nonexistent ID
        }

        [Test]
        public async Task TestUpdateQuiz()
        {
            Quiz quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Original Title",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,
            };

            var addedQuiz = await repository.Add(quiz);
            Quiz updatedQuizData = new Quiz
            {
                Category = Categories.History,
                Title = "Updated Title",
                Difficulty = Difficulties.Hard,
                NoOfQuestions = 20,
            };

            var updatedQuiz = await repository.Update(addedQuiz.QuizId, updatedQuizData);

            Assert.AreEqual("Updated Title", updatedQuiz.Title);
            Assert.AreEqual(Categories.History, updatedQuiz.Category);
        }

        [Test]
        public async Task ExceptionTestUpdateQuiz()
        {
            Quiz quizUpdateData = new Quiz
            {
                Category = Categories.History,
                Title = "Updated Title",
                Difficulty = Difficulties.Hard,
                NoOfQuestions = 20,
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(999, quizUpdateData)); // Nonexistent ID
        }

        [Test]
        public async Task TestGetAllQuizzes()
        {
            Quiz quiz1 = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Quiz 1",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,
            };

            Quiz quiz2 = new Quiz
            {
                CreatorId = 2,
                Category = Categories.Geography,
                Title = "Quiz 2",
                Difficulty = Difficulties.Easy,
                MaxPoint = 50,
            };

            await repository.Add(quiz1);
            await repository.Add(quiz2);

            var allQuizzes = await repository.GetAll();

            Assert.IsTrue(allQuizzes.Count() >= 2);
        }

        [Test]
        public async Task ExceptionTestGetAllQuizzes()
        {
            // Clear the database to make sure it's empty
            context.Quizes.RemoveRange(context.Quizes);
            await context.SaveChangesAsync();

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task ExceptionTestAddQuiz()
        {
            Assert.ThrowsAsync<Exception>(async () => await repository.Add(null));
        }

    }
}
