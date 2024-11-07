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
            var LeaderBoard = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0

            };

            // Act
            var addedLeaderBoard = await _repository.Add(LeaderBoard);

            // Assert
            Assert.IsNotNull(addedLeaderBoard);
            Assert.AreEqual(LeaderBoard.LeaderBoardId, addedLeaderBoard.LeaderBoardId);
        }

        [Test]
        public void AddLeaderBoard_ShouldThrowCouldNotAddException_OnDbUpdateException()
        {
            // Arrange
            var LeaderBoard = new LeaderBoard
            {    
                Categories = 0
            };

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(LeaderBoard));
        }

        [Test]
        public async Task GetLeaderBoard_ShouldReturnLeaderBoard_WhenExists()
        {
            // Arrange
            var LeaderBoard = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(LeaderBoard);

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
            var LeaderBoard1 = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };
            var LeaderBoard2 = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };
            await _repository.Add(LeaderBoard1);
            await _repository.Add(LeaderBoard2);

            // Act
            var LeaderBoards = await _repository.GetAll();

            // Assert
            Assert.IsNotNull(LeaderBoards);
            Assert.AreEqual(2, ((System.Collections.ICollection)LeaderBoards).Count);
        }

        [Test]
        public void GetAllLeaderBoards_ShouldThrowCollectionEmptyException_WhenNoLeaderBoards()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
            Assert.AreEqual("Collection empty.", ex.Message);
        }

        [Test]
        public async Task DeleteLeaderBoard_ShouldDeleteLeaderBoardSuccessfully()
        {
            // Arrange
            var LeaderBoard = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(LeaderBoard);

            // Act
            var deletedLeaderBoard = await _repository.Delete(addedLeaderBoard.LeaderBoardId);
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(addedLeaderBoard.LeaderBoardId));

            // Assert
            Assert.IsNotNull(deletedLeaderBoard);
            Assert.AreEqual(addedLeaderBoard.LeaderBoardId, deletedLeaderBoard.LeaderBoardId);
            Assert.AreEqual("LeaderBoard not found.", ex.Message);
        }

        [Test]
        public void DeleteLeaderBoard_ShouldThrowNotFoundException_WhenDoesNotExist()
        {
            // Act & Assert
            var ex = Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(999));
            Assert.AreEqual("LeaderBoard not found.", ex.Message);
        }

        [Test]
        public async Task UpdateLeaderBoard_ShouldUpdateSuccessfully()
        {
            // Arrange
            var LeaderBoard = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };
            var addedLeaderBoard = await _repository.Add(LeaderBoard);
            //addedLeaderBoard.LeaderBoardName = "What is the capital of Portugal updated?";

            // Act
            //var updatedLeaderBoard = await _repository.Update(addedLeaderBoard.LeaderBoardId, addedLeaderBoard);

            //// Assert
            //Assert.IsNotNull(updatedLeaderBoard);
            //Assert.AreEqual("What is the capital of Portugal updated?", updatedLeaderBoard.LeaderBoardName);
        }

        [Test]
        public void UpdateLeaderBoard_ShouldThrowArgumentException_OnIdMismatch()
        {
            // Arrange
            var LeaderBoard = new LeaderBoard
            {
                LeaderBoardName = "test",
                Categories = 0
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<ArgumentException>(async () => await _repository.Update(2, LeaderBoard));
            Assert.AreEqual("LeaderBoard ID mismatch.", ex.Message);
        }
    }
}
