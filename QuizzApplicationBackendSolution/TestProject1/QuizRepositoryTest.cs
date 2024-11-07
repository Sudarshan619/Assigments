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
        public async Task TestAddQuiz()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };
            var addedQuiz = await repository.Add(Quiz);
            Assert.IsTrue(addedQuiz.QuizId == Quiz.QuizId);
        }

        [Test]
        public async Task ExceptionTestAddQuiz()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(Quiz));
        }

        [Test]
        public async Task TesGet()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };
            var addedQuiz = await repository.Add(Quiz);

            var getUser = await repository.Get(Quiz.QuizId);
            Assert.IsNotNull(getUser);
        }

        [Test]
        public async Task ExceptionTestGet()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(Quiz.QuizId));
        }

        [Test]
        public async Task GetAllQuestionTest()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };


            await repository.Add(Quiz);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        public async Task ExceptionGetAllQuestionTest()
        {
            Quiz Quiz = new Quiz
            {
                CreatorId = 1,
                Category = Categories.Geography,
                Title = "Half-half",
                Difficulty = Difficulties.Medium,
                MaxPoint = 100,

            };


            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }
    }
}
