using AutoMapper;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Tests.Services
{
    [TestFixture]
    public class ScoreCardServiceTests
    {
        private Mock<IRepository<int, ScoreCard>> _mockScoreCardRepository;
        private Mock<IRepository<int, Quiz>> _mockQuizRepository;
        private Mock<IQuestionService> _mockQuestionService;
        private Mock<IRepository<int,Option>> _optionRepo;
        private Mock<IQuizService<IEnumerable<Question>, int>> _mockQuizService;
        private Mock<IRepository<string, User>> _mockUserRepository;
        private Mock<IMapper> _mockMapper;
        private IScoreCardService _scoreCardService;

        [SetUp]
        public void SetUp()
        {
            _mockScoreCardRepository = new Mock<IRepository<int, ScoreCard>>();
            _mockQuizRepository = new Mock<IRepository<int, Quiz>>();
            _mockMapper = new Mock<IMapper>();

            _mockUserRepository = new Mock<IRepository<string, User>>();
            _mockQuizService = new Mock<IQuizService<IEnumerable<Question>, int>>();
            _mockQuestionService = new Mock<IQuestionService>();
            _optionRepo = new Mock<IRepository<int, Option>>();
            _scoreCardService = new ScoreCardService(
                _optionRepo.Object,
                _mockQuestionService.Object,
                _mockUserRepository.Object,
                _mockScoreCardRepository.Object,
                _mockQuizService.Object,
                _mockQuizRepository.Object,
                _mockMapper.Object
            );
        }

        [Test]
        public async Task CreateScoreCard_ValidScoreCardDTO_ReturnsTrue()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId= 1,Score = 80 };
            var scoreCard = new ScoreCard();
            var quiz = new Quiz { QuizId = 1, MaxPoint = 100 };
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
             {
                 new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
                    new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
              }
            };
            var expectedAccuracy = 80.0;

            _mockMapper.Setup(m => m.Map<ScoreCard>(scoreCardDto)).Returns(scoreCard);
            _mockQuizRepository.Setup(q => q.Get(scoreCardDto.QuizId)).ReturnsAsync(quiz);
            _mockScoreCardRepository.Setup(repo => repo.Add(scoreCard)).ReturnsAsync(scoreCard);

            // Act
            var result = await _scoreCardService.CreateScoreCard(submittedOptionDto);
            var score = await _scoreCardService.GetScoreCard(scoreCard.ScoreCardId);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(expectedAccuracy, score.Acuuracy);

        }

        [Test]
        public void CreateScoreCard_WhenQuizNotFound_ThrowsException()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 99 };
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
             {
                 new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
                    new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
              }
            };
            _mockQuizRepository.Setup(q => q.Get(scoreCardDto.QuizId)).ThrowsAsync(new NotFoundException("Quiz not found"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _scoreCardService.CreateScoreCard(submittedOptionDto));
        }

        [Test]
        public async Task UpdateScoreCard_ValidScoreCardDTO_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, Score = 90 };
            var existingScoreCard = new ScoreCard();
            var updatedScoreCard = new ScoreCard();

            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ReturnsAsync(existingScoreCard);
            _mockMapper.Setup(m => m.Map(scoreCardDto, existingScoreCard)).Returns(updatedScoreCard);
            _mockScoreCardRepository.Setup(repo => repo.Update(id, updatedScoreCard)).ReturnsAsync(updatedScoreCard);

            // Act
            var result = await _scoreCardService.UpdateScoreCard(id, scoreCardDto);

            // Assert
            Assert.IsTrue(result);
            _mockScoreCardRepository.Verify(repo => repo.Update(id, updatedScoreCard), Times.Once);
        }

        [Test]
        public void UpdateScoreCard_WhenScoreCardNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var id = 1;
            var scoreCardDto = new ScoreCardDTO();
            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ThrowsAsync(new NotFoundException("ScoreCard not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _scoreCardService.UpdateScoreCard(id, scoreCardDto));
        }

        [Test]
        public async Task DeleteScoreCard_ValidId_ReturnsTrue()
        {
            // Arrange
            var id = 1;
            var scoreCard = new ScoreCard();

            _mockScoreCardRepository.Setup(repo => repo.Delete(id)).ReturnsAsync(scoreCard);

            // Act
            var result = await _scoreCardService.DeleteScoreCard(id);

            // Assert
            Assert.IsTrue(result);
            _mockScoreCardRepository.Verify(repo => repo.Delete(id), Times.Once);
        }

        [Test]
        public void DeleteScoreCard_WhenScoreCardNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var id = 1;
            _mockScoreCardRepository.Setup(repo => repo.Delete(id)).ThrowsAsync(new NotFoundException("ScoreCard not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _scoreCardService.DeleteScoreCard(id));
        }

        [Test]
        public async Task GetScoreCard_ValidId_ReturnsScoreCardDTO()
        {
            // Arrange
            var id = 1;
            var scoreCard = new ScoreCard { ScoreCardId = id };
            var scoreCardDto = new ScoreCardDTO();
                

            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ReturnsAsync(scoreCard);
            _mockMapper.Setup(m => m.Map<ScoreCardDTO>(scoreCard)).Returns(scoreCardDto);

            // Act
            var result = await _scoreCardService.GetScoreCard(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.ScoreCardId);
        }

        [Test]
        public void GetScoreCard_WhenScoreCardNotFound_ThrowsNotFoundException()
        {
            // Arrange
            var id = 1;
            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ThrowsAsync(new NotFoundException("ScoreCard not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _scoreCardService.GetScoreCard(id));
        }
    }
}
