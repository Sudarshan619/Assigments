using AutoMapper;
using Moq;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject1
{
    public class LeaderBoardServiceTest
    {
        private readonly Mock<IRepository<int, LeaderBoard>> _mockLeaderBoardRepository;
        private readonly Mock<IRepository<int, ScoreCard>> _mockScoreCardRepository;
        private readonly Mock<IRepository<string, User>> _mockUserRepository;
        private readonly Mock<IRepository<int, Quiz>> _mockQuizRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LeaderBoardService _leaderBoardService;

        public LeaderBoardServiceTest()
        {
            _mockLeaderBoardRepository = new Mock<IRepository<int, LeaderBoard>>();
            _mockScoreCardRepository = new Mock<IRepository<int, ScoreCard>>();
            _mockQuizRepository = new Mock<IRepository<int, Quiz>>();
            _mockUserRepository = new Mock<IRepository<string, User>>();
            _mockMapper = new Mock<IMapper>();

            _leaderBoardService = new LeaderBoardService(
                _mockQuizRepository.Object,
                _mockLeaderBoardRepository.Object,              
                _mockScoreCardRepository.Object,
                _mockUserRepository.Object,
                _mockMapper.Object);
        }

        [Test]
        public async Task CreateLeaderBoard_ValidLeaderBoardDTO_ReturnsTrue()
        {
            // Arrange
            var leaderBoardDto = new LeaderBoardDTO();
            var leaderBoard = new LeaderBoard();

            _mockMapper.Setup(m => m.Map<LeaderBoard>(leaderBoardDto)).Returns(leaderBoard);
            _mockLeaderBoardRepository.Setup(repo => repo.Add(It.IsAny<LeaderBoard>())).ReturnsAsync(leaderBoard);

            // Act
            var result = await _leaderBoardService.CreateLeaderBoard(leaderBoardDto);

            // Assert
            Assert.True(result);
            _mockLeaderBoardRepository.Verify(repo => repo.Add(It.IsAny<LeaderBoard>()), Times.Once);
        }

        [Test]
        public async Task DeleteLeaderBoard_ValidId_ReturnsTrue()
        {
            // Arrange
            int leaderBoardId = 1;
            var leaderBoard = new LeaderBoard { LeaderBoardId = leaderBoardId };

            _mockLeaderBoardRepository.Setup(repo => repo.Delete(leaderBoardId)).ReturnsAsync(leaderBoard);

            // Act
            var result = await _leaderBoardService.DeleteLeaderBoard(leaderBoardId);

            // Assert
            Assert.True(result);
            _mockLeaderBoardRepository.Verify(repo => repo.Delete(leaderBoardId), Times.Once);
        }

        [Test]
        public async Task GetLeaderBoard_ValidId_ReturnsLeaderBoardDTO()
        {
            // Arrange
            int leaderBoardId = 1;
            var leaderBoard = new LeaderBoard { LeaderBoardId = leaderBoardId };
            var scoreCards = new List<ScoreCard> { new ScoreCard { ScoreCardId = leaderBoardId } };

            _mockLeaderBoardRepository.Setup(repo => repo.Get(leaderBoardId)).ReturnsAsync(leaderBoard);
            _mockScoreCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(scoreCards);

            // Act
            var result = await _leaderBoardService.GetLeaderBoard(leaderBoardId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(leaderBoardId, result.ScoreCards.First().ScoreCardId);
        }

        [Test]
        public async Task SortLeaderBoard_ByScore_SortsCorrectly()
        {
            // Arrange
            int leaderBoardId = 1;
            var leaderBoard = new LeaderBoardDTO()
            {

                LeaderBoardName = "Test",
                Category = 0
            };
            var leaderBoardDto = new LeaderBoardResponseDTO
            {
                ScoreCards = new List<ScoreCardResponseDTO>
              {
             new ScoreCardResponseDTO { ScoreCardId = 1, Username = "Alice", Score = 100, Acuuracy = 0.9 },
             new ScoreCardResponseDTO { ScoreCardId = 2, Username = "Bob", Score = 200, Acuuracy = 0.8 }
               }
            };

            // Mock the repository to add and retrieve ScoreCards within the LeaderBoard
            _mockLeaderBoardRepository.Setup(repo => repo.Add(It.IsAny<LeaderBoard>())).ReturnsAsync(new LeaderBoard { LeaderBoardId = leaderBoardId });
            _mockScoreCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ScoreCard>
    {
        new ScoreCard { ScoreCardId = 1, UserId = 1, Score = 100, Acuuracy = 0.9 },
        new ScoreCard { ScoreCardId = 2, UserId = 2, Score = 200, Acuuracy = 0.8 }
    });

            // Act: First create the leaderboard with ScoreCards, then sort
            await _leaderBoardService.CreateLeaderBoard(leaderBoard);
            await _leaderBoardService.CreateLeaderBoard(leaderBoard);// Creates leaderboard and ScoreCards
            var result = await _leaderBoardService.SortLeaderBoard(Choice.Score, leaderBoardId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(200, result.ScoreCards.Last().Score);
           
        }


        [Test]
        public async Task SortLeaderBoard_ByUsername_SortsCorrectly()
        {
            // Arrange
            int leaderBoardId = 1;
            var leaderBoard = new LeaderBoardDTO()
            {
                LeaderBoardName = "Test",
                Category = 0
            };
            var leaderBoardDto = new LeaderBoardResponseDTO
            {
                ScoreCards = new List<ScoreCardResponseDTO>
        {
             new ScoreCardResponseDTO { ScoreCardId = 1, Username = "Alice", Score = 100, Acuuracy = 0.9 },
             new ScoreCardResponseDTO { ScoreCardId = 2, Username = "Bob", Score = 200, Acuuracy = 0.8 }
        }
            };

            _mockLeaderBoardRepository.Setup(repo => repo.Add(It.IsAny<LeaderBoard>())).ReturnsAsync(new LeaderBoard { LeaderBoardId = leaderBoardId });
            _mockScoreCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ScoreCard>
    {
        new ScoreCard { ScoreCardId = 1, UserId = 1, Score = 100, Acuuracy = 0.9 },
        new ScoreCard { ScoreCardId = 2, UserId = 2, Score = 200, Acuuracy = 0.8 }
    });

            await _leaderBoardService.CreateLeaderBoard(leaderBoard); 
            var result = await _leaderBoardService.SortLeaderBoard(Choice.Username, leaderBoardId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Alice", result.ScoreCards.First().Username);  
        }
    }
}
