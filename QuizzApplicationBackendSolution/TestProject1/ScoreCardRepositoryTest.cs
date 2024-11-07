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
    public class ScoreCardRepositoryTest
    {
        DbContextOptions options;
        QuizContext context;
        ScorecardRepository repository;
        Mock<ILogger<ScorecardRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
            .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<ScorecardRepository>>();
            repository = new ScorecardRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAddScoreCard()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(ScoreCard);
            Assert.IsTrue(addedScoreCard.ScoreCardId == ScoreCard.ScoreCardId);
        }

        [Test]
        public async Task ExceptionTestAddScoreCard()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(ScoreCard));
        }

        [Test]
        public async Task TesGet()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(ScoreCard);

            var getUser = await repository.Get(ScoreCard.ScoreCardId);
            Assert.IsNotNull(getUser);
        }

        [Test]
        public async Task ExceptionTestGet()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(ScoreCard.ScoreCardId));
        }

        [Test]
        public async Task GetAllScoreCardTest()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };

            await repository.Add(ScoreCard);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        public async Task ExceptionGetAllScoreCardTest()
        {
            ScoreCard ScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }
    }
}
