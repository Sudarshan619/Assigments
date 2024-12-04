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

namespace TestProject
{
    [TestFixture]
    public class ScorecardRepositoryTests
    {
        private DbContextOptions<QuizContext> options;
        private QuizContext context;
        private ScorecardRepository repository;
        private Mock<ILogger<ScorecardRepository>> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Create a unique in-memory database
                .Options;
            context = new QuizContext(options);
            logger = new Mock<ILogger<ScorecardRepository>>();
            repository = new ScorecardRepository(context, logger.Object);
        }

        [TearDown]
        public void Teardown()
        {
            context.Dispose();
        }

        [Test]
        public async Task Add_ValidScoreCard_ShouldAddSuccessfully()
        {
            // Arrange
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90,
                SubmittedTime = DateTime.UtcNow.ToString()
            };

            // Act
            var result = await repository.Add(scoreCard);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(scoreCard.Score, result.Score);
            Assert.AreEqual(scoreCard.Acuuracy, result.Acuuracy);
        }

        [Test]
        public void Add_NullScoreCard_ShouldThrowArgumentException()
        {
            // Assert
            Assert.ThrowsAsync<ArgumentException>(async () => await repository.Add(null));
        }

        [Test]
        public async Task Get_ValidId_ShouldReturnScoreCard()
        {
            // Arrange
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90,
                SubmittedTime = DateTime.UtcNow.ToString()
            };
            await repository.Add(scoreCard);

            // Act
            var result = await repository.Get(scoreCard.ScoreCardId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(scoreCard.ScoreCardId, result.ScoreCardId);
        }

        [Test]
        public void Get_InvalidId_ShouldThrowNotFoundException()
        {
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAll_WhenScoreCardsExist_ShouldReturnAll()
        {
            // Arrange
            var scoreCards = new List<ScoreCard>
            {
                new ScoreCard { QuizId = 1, UserId = 1, Score = 90, Acuuracy = 95, SubmittedTime = DateTime.UtcNow.ToString() },
                new ScoreCard { QuizId = 2, UserId = 2, Score = 85, Acuuracy = 88, SubmittedTime = DateTime.UtcNow.ToString() }
            };

            await context.ScoreCards.AddRangeAsync(scoreCards);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAll_WhenNoScoreCardsExist_ShouldThrowCollectionEmptyException()
        {
            // Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task Delete_ValidId_ShouldDeleteSuccessfully()
        {
            // Arrange
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90,
                SubmittedTime = DateTime.UtcNow.ToString()
            };
            await repository.Add(scoreCard);

            // Act
            var result = await repository.Delete(scoreCard.ScoreCardId);

            // Assert
            Assert.AreEqual(scoreCard.ScoreCardId, result.ScoreCardId);
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(scoreCard.ScoreCardId));
        }

        [Test]
        public void Delete_InvalidId_ShouldThrowNotFoundException()
        {
            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task Update_ValidId_ShouldUpdateSuccessfully()
        {
            // Arrange
            var scoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90,
                SubmittedTime = DateTime.UtcNow.ToString()
            };
            var addedScoreCard = await repository.Add(scoreCard);

            var updatedScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 110,
                Acuuracy = 95,
                SubmittedTime = DateTime.UtcNow.ToString()
            };

            // Act
            var result = await repository.Update(addedScoreCard.ScoreCardId, updatedScoreCard);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedScoreCard.Score, result.Score);
            Assert.AreEqual(updatedScoreCard.Acuuracy, result.Acuuracy);
        }

        [Test]
        public void Update_InvalidId_ShouldThrowNotFoundException()
        {
            // Arrange
            var updatedScoreCard = new ScoreCard
            {
                QuizId = 1,
                UserId = 1,
                Score = 100,
                Acuuracy = 90,
                SubmittedTime = DateTime.UtcNow.ToString()
            };

            // Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(999, updatedScoreCard));
        }

        [Test]
        public void Add_WhenExceptionOccurs_ShouldThrowGeneralException()
        {
            // Arrange
            var invalidContext = new Mock<QuizContext>();
            var repositoryWithFaultyContext = new ScorecardRepository(invalidContext.Object, logger.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await repositoryWithFaultyContext.Add(new ScoreCard()));
        }

        [Test]
        public void Get_WhenExceptionOccurs_ShouldThrowGeneralException()
        {
            // Arrange
            var invalidContext = new Mock<QuizContext>();
            invalidContext.Setup(ctx => ctx.ScoreCards.FindAsync(It.IsAny<int>()))
                          .Throws(new Exception("Database error"));

            var repositoryWithFaultyContext = new ScorecardRepository(invalidContext.Object, logger.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await repositoryWithFaultyContext.Get(1));
        }

       

        [Test]
        public void Update_WhenExceptionOccurs_ShouldThrowGeneralException()
        {
            // Arrange
            var invalidContext = new Mock<QuizContext>();
            invalidContext.Setup(ctx => ctx.ScoreCards.FindAsync(It.IsAny<int>()))
                          .Throws(new Exception("Database error"));

            var repositoryWithFaultyContext = new ScorecardRepository(invalidContext.Object, logger.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await repositoryWithFaultyContext.Update(1, new ScoreCard()));
        }

        [Test]
        public void Delete_WhenExceptionOccurs_ShouldThrowGeneralException()
        {
            // Arrange
            var invalidContext = new Mock<QuizContext>();
            invalidContext.Setup(ctx => ctx.ScoreCards.FindAsync(It.IsAny<int>()))
                          .Throws(new Exception("Database error"));

            var repositoryWithFaultyContext = new ScorecardRepository(invalidContext.Object, logger.Object);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await repositoryWithFaultyContext.Delete(1));
        }

    }
}
