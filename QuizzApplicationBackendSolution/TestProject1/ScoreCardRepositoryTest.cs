using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
    public class ScoreCardRepositoryTest
    {
        private DbContextOptions<QuizContext> options;
        private QuizContext context;
        private ScorecardRepository repository;
        private Mock<ILogger<ScorecardRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Use unique DB for each test run
                .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<ScorecardRepository>>();
            repository = new ScorecardRepository(context, logger.Object);
        }

        [Test]
        public async Task Add_ValidScoreCard_ShouldAddSuccessfully()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90
            };

            var result = await repository.Add(scoreCard);

            Assert.IsNotNull(result);
            Assert.AreEqual(scoreCard.Score, result.Score);
        }

        [Test]
        public async Task Add_NullScoreCard_ShouldThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await repository.Add(null));
        }

        [Test]
        public async Task Get_ValidId_ShouldReturnScoreCard()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 2,
                UserId = 2,
                Score = 80,
                Acuuracy = 75
            };

            var addedScoreCard = await repository.Add(scoreCard);

            var retrievedScoreCard = await repository.Get(addedScoreCard.ScoreCardId);

            Assert.IsNotNull(retrievedScoreCard);
            Assert.AreEqual(addedScoreCard.ScoreCardId, retrievedScoreCard.ScoreCardId);
        }

        [Test]
        public async Task Get_InvalidId_ShouldThrowNotFoundException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAll_WhenScoreCardsExist_ShouldReturnAll()
        {
            var scoreCards = new List<ScoreCard>
            {
                new ScoreCard { QuizId = 1, UserId = 1, Score = 90, Acuuracy = 95 },
                new ScoreCard { QuizId = 2, UserId = 2, Score = 85, Acuuracy = 88 }
            };

            await context.ScoreCards.AddRangeAsync(scoreCards);
            await context.SaveChangesAsync();

            var result = await repository.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAll_WhenNoScoreCards_ShouldThrowCollectionEmptyException()
        {
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task Delete_ValidId_ShouldDeleteSuccessfully()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 3,
                UserId = 3,
                Score = 70,
                Acuuracy = 60
            };

            var addedScoreCard = await repository.Add(scoreCard);

            var deletedScoreCard = await repository.Delete(addedScoreCard.ScoreCardId);

            Assert.AreEqual(addedScoreCard.ScoreCardId, deletedScoreCard.ScoreCardId);

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(deletedScoreCard.ScoreCardId));
        }

        [Test]
        public async Task Delete_InvalidId_ShouldThrowException()
        {
            Assert.ThrowsAsync<Exception>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task Update_ValidId_ShouldUpdateSuccessfully()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 95
            };

            var addedScoreCard = await repository.Add(scoreCard);

            var updatedScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 110,
                Acuuracy = 97
            };

            var result = await repository.Update(addedScoreCard.ScoreCardId, updatedScoreCard);

            Assert.IsNotNull(result);
            Assert.AreEqual(updatedScoreCard.Score, result.Score);
            Assert.AreEqual(updatedScoreCard.Acuuracy, result.Acuuracy);
        }

        [Test]
        public async Task Update_InvalidId_ShouldThrowException()
        {
            var updatedScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 95
            };

            Assert.ThrowsAsync<Exception>(async () => await repository.Update(999, updatedScoreCard));
        }
    }
}
