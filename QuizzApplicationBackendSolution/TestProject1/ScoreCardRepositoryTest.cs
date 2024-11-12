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
                .UseInMemoryDatabase("TestScoreCardDb")
                .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<ScorecardRepository>>();
            repository = new ScorecardRepository(context, logger.Object);
        }

        [Test]
        public async Task TestAddScoreCard()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(scoreCard);
            Assert.AreEqual(scoreCard.ScoreCardId, addedScoreCard.ScoreCardId);
        }

        [Test]
        public void ExceptionTestAddScoreCard()
        {
            // Attempting to add a null scoreCard to trigger exception
            Assert.ThrowsAsync<Exception>(async () => await repository.Add(null));
        }

        [Test]
        public async Task TestGetScoreCard()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(scoreCard);

            var retrievedScoreCard = await repository.Get(addedScoreCard.ScoreCardId);
            Assert.IsNotNull(retrievedScoreCard);
            Assert.AreEqual(scoreCard.ScoreCardId, retrievedScoreCard.ScoreCardId);
        }

        [Test]
        public void ExceptionTestGetScoreCard()
        {
            // Attempting to get a ScoreCard with a non-existing ID to trigger NotFoundException
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task TestGetAllScoreCards()
        {
            var scoreCard1 = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };

            var scoreCard2 = new ScoreCard
            {
                QuizId = 2,
                UserId = 2,
                Score = 80,
                Acuuracy = 70
            };

            await repository.Add(scoreCard1);
            await repository.Add(scoreCard2);

            var allScoreCards = await repository.GetAll();
            Assert.IsNotNull(allScoreCards);
        }

        [Test]
        public async Task ExceptionTestGetAllScoreCards()
        {
            // Ensure no scorecards exist to trigger CollectionEmptyException
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task TestDeleteScoreCard()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(scoreCard);

            var deletedScoreCard = await repository.Delete(addedScoreCard.ScoreCardId);
            Assert.AreEqual(addedScoreCard.ScoreCardId, deletedScoreCard.ScoreCardId);

            // Verify that the deleted scoreCard cannot be retrieved
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(deletedScoreCard.ScoreCardId));
        }

        [Test]
        public void ExceptionTestDeleteScoreCard()
        {
            
            Assert.ThrowsAsync<Exception>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task TestUpdateScoreCard()
        {
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 50
            };
            var addedScoreCard = await repository.Add(scoreCard);

            // Modify properties
            addedScoreCard.Score = 90;
            addedScoreCard.Acuuracy = 80;

            var updatedScoreCard = await repository.Update(addedScoreCard.ScoreCardId, addedScoreCard);

            Assert.IsNotNull(updatedScoreCard);
            Assert.AreEqual(90, updatedScoreCard.Score);
            Assert.AreEqual(80, updatedScoreCard.Acuuracy);
        }

        [Test]
        public void ExceptionTestUpdateScoreCard()
        {
            var nonExistentScoreCard = new ScoreCard
            {
                ScoreCardId = 999,
                QuizId = 1,
                UserId = 1,
                Score = 70,
                Acuuracy = 60
            };

            Assert.ThrowsAsync<Exception>(async () => await repository.Update(999, nonExistentScoreCard));
        }

     
    }
}
