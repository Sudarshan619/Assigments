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
    public class LeaderBoardRepositoryTest
    {
        private DbContextOptions<QuizContext> _options;
        private QuizContext _context;
        private LeaderBoardRepository _repository;
        private Mock<ILogger<LeaderBoardRepository>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<QuizContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB for each test
                .Options;
            _context = new QuizContext(_options);
            _loggerMock = new Mock<ILogger<LeaderBoardRepository>>();
            _repository = new LeaderBoardRepository(_context, _loggerMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task AddLeaderBoard_ShouldAddLeaderBoardSuccessfully()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardName = "Test LeaderBoard",
                Categories = 0
            };

            // Act
            var addedLeaderBoard = await _repository.Add(leaderBoard);

            // Assert
            Assert.IsNotNull(addedLeaderBoard);
            Assert.AreEqual(leaderBoard.LeaderBoardId, addedLeaderBoard.LeaderBoardId);
        }

        [Test]
        public void AddLeaderBoard_ShouldThrowCouldNotAddException()
        {
            // Act & Assert
            var leaderBoard = new LeaderBoard
            {

            };
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(leaderBoard));
        }

        [Test]
        public async Task GetLeaderBoard_ShouldReturnLeaderBoard_WhenExists()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardName = "Test LeaderBoard",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(leaderBoard);

            // Act
            var retrievedLeaderBoard = await _repository.Get(addedLeaderBoard.LeaderBoardId);

            // Assert
            Assert.IsNotNull(retrievedLeaderBoard);
            Assert.AreEqual(addedLeaderBoard.LeaderBoardId, retrievedLeaderBoard.LeaderBoardId);
        }

        [Test]
        public void GetLeaderBoard_ShouldThrowNotFoundException_WhenDoesNotExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task GetAllLeaderBoards_ShouldReturnAllLeaderBoards_WhenExist()
        {
            // Arrange
            var leaderBoard1 = new LeaderBoard
            {
                LeaderBoardName = "LeaderBoard 1",
                Categories = 0
            };
            var leaderBoard2 = new LeaderBoard
            {
                LeaderBoardName = "LeaderBoard 2",
                Categories = 0
            };
            await _repository.Add(leaderBoard1);
            await _repository.Add(leaderBoard2);

            // Act
            var leaderBoards = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(leaderBoards);
            Assert.AreEqual(2, ((System.Collections.ICollection)leaderBoards).Count);
        }

        [Test]
        public void GetAllLeaderBoards_ShouldThrowCollectionEmptyException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.GetAll());
            Assert.AreEqual("Collection empty", ex.Message);
        }

        [Test]
        public async Task DeleteLeaderBoard_ShouldDeleteLeaderBoardSuccessfully()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardName = "Test LeaderBoard",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(leaderBoard);

            // Act
            var deletedLeaderBoard = await _repository.Delete(addedLeaderBoard.LeaderBoardId);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(addedLeaderBoard.LeaderBoardId));

            // Assert
            Assert.IsNotNull(deletedLeaderBoard);
            Assert.AreEqual(addedLeaderBoard.LeaderBoardId, deletedLeaderBoard.LeaderBoardId);
            Assert.AreEqual("LeaderBoard not found", ex.Message);
        }

        [Test]
        public void DeleteLeaderBoard_ShouldThrowNotFoundException()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await _repository.Delete(999));
            Assert.AreEqual("Not able to delete Question", ex.Message);
        }

        [Test]
        public async Task UpdateLeaderBoard_ShouldUpdateSuccessfully()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardName = "Test LeaderBoard",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(leaderBoard);
            addedLeaderBoard.LeaderBoardName = "Updated LeaderBoard";

            // Act
            var updatedLeaderBoard = await _repository.Update(addedLeaderBoard.LeaderBoardId, addedLeaderBoard);

            // Assert
            Assert.IsNotNull(updatedLeaderBoard);
            Assert.AreEqual("Updated LeaderBoard", updatedLeaderBoard.LeaderBoardName);
        }

        [Test]
        public void UpdateLeaderBoard_ShouldThrowArgumentException_OnIdMismatch()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardName = "Test LeaderBoard",
                Categories = 0
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(999, leaderBoard));
            Assert.AreEqual("LeaderBoard not found", ex.Message);
        }

        [Test]
        public async Task UpdateLeaderBoard_ShouldThrowNotFoundException_WhenLeaderBoardDoesNotExist()
        {
            // Arrange
            var leaderBoard = new LeaderBoard
            {
                LeaderBoardId = 999,
                LeaderBoardName = "Non-existent LeaderBoard",
                Categories = 0
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(999, leaderBoard));
            Assert.AreEqual("LeaderBoard not found", ex.Message);
        }
    }
}
