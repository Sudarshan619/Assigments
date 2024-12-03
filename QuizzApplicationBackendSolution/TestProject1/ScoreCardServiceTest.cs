using AutoMapper;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Tests.Services
{
    [TestFixture]
    public class ScoreCardServiceTests
    {
        private Mock<IRepository<int, ScoreCard>> _mockScoreCardRepository;
        private Mock<IRepository<int, Quiz>> _mockQuizRepository;
        private Mock<IQuestionService> _mockQuestionService;
        private Mock<IRepository<int, Option>> _optionRepo;
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
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 80 };
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
            //var score = await _scoreCardService.GetScoreCard(scoreCard.ScoreCardId);

            // Assert
            Assert.IsTrue(result);
            //Assert.AreEqual(expectedAccuracy, score.Acuuracy);
        }

        [Test]
        public void CreateScoreCard_WhenQuizNotFound_ThrowsException()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 99,UserId =1 };
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
            Assert.ThrowsAsync<NotFoundException>(async () => await _scoreCardService.CreateScoreCard(submittedOptionDto));
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

        [Test]
        public async Task CalculateAccuracy_ValidData_ReturnsAccuracy()
        {
            // Arrange
            var submittedOptionDTO = new SubmittedOptionDTO { QuizId = 1 };
            var score = 80;
            var quiz = new Quiz { MaxPoint = 100 };

            _mockQuizRepository.Setup(q => q.Get(submittedOptionDTO.QuizId)).ReturnsAsync(quiz);

            // Act
            var result = await _scoreCardService.CalculateAccuracy(submittedOptionDTO, score);

            // Assert
            Assert.AreEqual(80, result);
        }

        [Test]
        public void CalculateAccuracy_ThrowsException()
        {
            // Arrange
            var submittedOptionDTO = new SubmittedOptionDTO { QuizId = 1 };
            var score = 80;

            _mockQuizRepository.Setup(q => q.Get(submittedOptionDTO.QuizId)).ThrowsAsync(new NotFoundException("Quiz not found"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _scoreCardService.CalculateAccuracy(submittedOptionDTO, score));
        }

    //    [Test]
    //    public async Task CalculateScore_AllOptionsCorrect_ReturnsTotalScore()
    //    {
    //        // Arrange
    //        var submittedOptionDto = new SubmittedOptionDTO
    //        {
    //            QuizId = 1,
    //            Options = new List<SelectedOptionDTO>
    //    {
    //        new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
    //        new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
    //    }
    //        };

    //        var quiz = new Quiz
    //        {
    //            QuizId = 1,
    //            questions = new List<Question>
    //    {
    //        new Question { QuestionId = 1, Points = 50, Options = new List<Option> { new Option { OptionId = 1, IsCorrect = true } } },
    //        new Question { QuestionId = 2, Points = 50, Options = new List<Option> { new Option { OptionId = 2, IsCorrect = true } } }
    //    }
    //        };

    //        var options = new List<Option>
    //{
    //    new Option { OptionId = 1, QuestionId = 1, IsCorrect = true },
    //    new Option { OptionId = 2, QuestionId = 2, IsCorrect = true }
    //};

    //        _mockQuizService.Setup(q => q.GetQuiz(submittedOptionDto.QuizId)).ReturnsAsync(quiz);
    //        _optionRepo.Setup(o => o.GetAll()).ReturnsAsync(options);

    //        // Act
    //        var result = await _scoreCardService.CalculateScore(submittedOptionDto);

    //        // Assert
    //        Assert.AreEqual(100, result);
    //    }

        [Test]
        public async Task CalculateAccuracy_ValidScore_ReturnsCorrectAccuracy()
        {
            // Arrange
            var submittedOptionDTO = new SubmittedOptionDTO { QuizId = 1 };
            var score = 80;
            var quiz = new Quiz { MaxPoint = 100 };

            _mockQuizRepository.Setup(q => q.Get(submittedOptionDTO.QuizId)).ReturnsAsync(quiz);

            // Act
            var result = await _scoreCardService.CalculateAccuracy(submittedOptionDTO, score);

            // Assert
            Assert.AreEqual(80, result);
        }

        [Test]
        public async Task GetScoreCard_ValidId_ReturnsScoreCardDTOWithUsername()
        {
            // Arrange
            var id = 1;
            var scoreCard = new ScoreCard { ScoreCardId = id, UserId = 1, Score = 80 };
            var user = new User { Id = 1, Name = "John Doe" };
            var scoreCardDto = new ScoreCardResponseDTO { ScoreCardId = id, Username = "John Doe" };

            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ReturnsAsync(scoreCard);
            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<User> { user });
            _mockMapper.Setup(m => m.Map<ScoreCardResponseDTO>(scoreCard)).Returns(scoreCardDto);

            // Act
            var result = await _scoreCardService.GetScoreCard(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(id, result.ScoreCardId);
            Assert.AreEqual("John Doe", result.Username);
        }

        [Test]
        public async Task GetAllScoreCards_ReturnsScoreCardsWithUsernamesAndQuizNames()
        {
            // Arrange
            var scoreCard1 = new ScoreCard { UserId = 1, QuizId = 1 };
            var scoreCard2 = new ScoreCard { UserId = 2, QuizId = 2 };

            var users = new List<User>
           {
        new User { Id = 1, Name = "User 1" },
        new User { Id = 2, Name = "User 2" }
    };

            var quizzes = new List<Quiz>
    {
        new Quiz { QuizId = 1, Title = "Quiz 1" },
        new Quiz { QuizId = 2, Title = "Quiz 2" }
    };

            _mockScoreCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ScoreCard> { scoreCard1, scoreCard2 });
            _mockUserRepository.Setup(repo => repo.GetAll()).ReturnsAsync(users);
            _mockQuizRepository.Setup(repo => repo.GetAll()).ReturnsAsync(quizzes);
            _mockMapper.Setup(m => m.Map<ScoreCardResponseDTO>(It.IsAny<ScoreCard>())).Returns(new ScoreCardResponseDTO());

            // Act
            var result = await _scoreCardService.GetAllScoreCards();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("User 1", result.First().Username);
            Assert.AreEqual("Quiz 1", result.First().QuizName);
        }

        [Test]
        public void CreateScoreCard_WhenInvalidScoreCardDTO_ThrowsException()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO(); 
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
            {
                new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" }
            }
            };

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _scoreCardService.CreateScoreCard(submittedOptionDto));
        }

        [Test]
        public async Task UpdateScoreCard_WhenInvalidDTO_ThrowsException()
        {
            // Arrange
            var id = 1;
            var scoreCardDto = new ScoreCardDTO(); // Invalid DTO (e.g., missing necessary data)
            var existingScoreCard = new ScoreCard();

            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ReturnsAsync(existingScoreCard);

            // Act & Assert
            Assert.ThrowsAsync<ValidationException>(async () => await _scoreCardService.UpdateScoreCard(id, scoreCardDto));
        }

        [Test]
        public async Task GetAllScoreCards_WhenNoScoreCards_ReturnsEmptyList()
        {
            // Arrange
            _mockScoreCardRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<ScoreCard>()); // No score cards in DB

            // Act
            var result = await _scoreCardService.GetAllScoreCards();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count());
        }

        //[Test]
        //public async Task CalculateScore_WhenNotAllOptionsCorrect_ReturnsPartialScore()
        //{
        //    // Arrange
        //    var submittedOptionDto = new SubmittedOptionDTO
        //    {
        //        QuizId = 1,
        //        Options = new List<SelectedOptionDTO>
        //    {
        //        new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" }, // Correct option
        //        new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }  // Incorrect option
        //    }
        //    };

        //    var quiz = new Quiz
        //    {
        //        QuizId = 1,
        //        NoOfQuestions = 2
        //    };

        //    var options = new List<Option>
        //{
        //    new Option { OptionId = 1, QuestionId = 1, IsCorrect = true },
        //    new Option { OptionId = 2, QuestionId = 2, IsCorrect = false }
        //};

        //    _mockQuizService.Setup(q => q.GetQuiz(submittedOptionDto.QuizId)).ReturnsAsync(quiz);
        //    _optionRepo.Setup(o => o.GetAll()).ReturnsAsync(options);

        //    // Act
        //    var result = await _scoreCardService.CalculateScore(submittedOptionDto);

        //    // Assert
        //    Assert.AreEqual(50, result); // Since one option was correct, the score should be 50
        //}

        [Test]
        public void CalculateAccuracy_WhenScoreIsZero_ReturnsZeroAccuracy()
        {
            // Arrange
            var submittedOptionDTO = new SubmittedOptionDTO { QuizId = 1 };
            var score = 10;
            var quiz = new Quiz { MaxPoint = 100 };

            _mockQuizRepository.Setup(q => q.Get(submittedOptionDTO.QuizId)).ReturnsAsync(quiz);

            // Act
            var result = _scoreCardService.CalculateAccuracy(submittedOptionDTO, score);

            // Assert
            Assert.AreEqual(10, result);
        }

        [Test]
        public void GetScoreCard_WhenDatabaseThrowsException_ThrowsInternalServerErrorException()
        {
            // Arrange
            var id = 1;
            _mockScoreCardRepository.Setup(repo => repo.Get(id)).ThrowsAsync(new System.Exception("Database error"));

            // Act & Assert
            Assert.ThrowsAsync<System.Exception>(async () => await _scoreCardService.GetScoreCard(id));
        }

    }
}
