using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    public class ScoreCardControllerTest
    {
        private Mock<IScoreCardService> _mockScoreCardService;
        private ScoreCardController _scoreCardController;

        [SetUp]
        public void Setup()
        {
            _mockScoreCardService = new Mock<IScoreCardService>();
            _scoreCardController = new ScoreCardController(_mockScoreCardService.Object);
        }

        [Test]
        public async Task CreateScoreCard_ReturnsOk()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 30 };
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
             {
                 new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
                    new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
              }
            };
            _mockScoreCardService.Setup(service => service.CreateScoreCard(submittedOptionDto)).ReturnsAsync(true);

            // Act
            var result = await _scoreCardController.CreateScoreCard(submittedOptionDto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("Succesfully created", okResult.Value);
        }

        [Test]
        public async Task CreateScoreCard_ReturnsBadRequest()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 30 };
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
             {
                 new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
                    new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
              }
            };
            _mockScoreCardService.Setup(service => service.CreateScoreCard(submittedOptionDto)).ReturnsAsync(false);

            // Act
            var result = await _scoreCardController.CreateScoreCard(submittedOptionDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Error while creating score card.", badRequestResult.Value);
        }
        [Test]
        public async Task CreateScoreCard_ReturnsBadRequest_Error()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 30 };
            var submittedOptionDto = new SubmittedOptionDTO
            {
                Options = new List<SelectedOptionDTO>
             {
                 new SelectedOptionDTO { OptionId = 1, OptionName = "Option 1" },
                    new SelectedOptionDTO { OptionId = 2, OptionName = "Option 2" }
              }
            };
            _mockScoreCardService.Setup(service => service.CreateScoreCard(submittedOptionDto)).ReturnsAsync(false);

            // Act
            var result = await _scoreCardController.CreateScoreCard(null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("ScoreCard data is required.", badRequestResult.Value);
        }

        [Test]
        public async Task UpdateScoreCard_ReturnsNoContent()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 35 };
            _mockScoreCardService.Setup(service => service.UpdateScoreCard(1, scoreCardDto)).ReturnsAsync(true);

            // Act
            var result = await _scoreCardController.UpdateScoreCard(1, scoreCardDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateScoreCard_ReturnsNotFound()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 35 };
            _mockScoreCardService.Setup(service => service.UpdateScoreCard(1, scoreCardDto)).ReturnsAsync(false);

            // Act
            var result = await _scoreCardController.UpdateScoreCard(1, scoreCardDto);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("ScoreCard with ID 1 not found.", notFoundResult.Value);
        }

        [Test]
        public async Task UpdateScoreCard_ReturnsBadrequest_Error()
        {
            // Arrange
            var scoreCardDto = new ScoreCardDTO { QuizId = 1, UserId = 1, Score = 35 };
            _mockScoreCardService.Setup(service => service.UpdateScoreCard(1, scoreCardDto)).ReturnsAsync(false);

            // Act
            var result = await _scoreCardController.UpdateScoreCard(1,null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("ScoreCard data is required.", badRequestResult.Value);
        }

        [Test]
        public async Task DeleteScoreCard_ReturnsOk()
        {
            // Arrange
            _mockScoreCardService.Setup(service => service.DeleteScoreCard(1)).ReturnsAsync(true);

            // Act
            var result = await _scoreCardController.DeleteScoreCard(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual("successfull deleted for id 1", okResult.Value);
        }

        [Test]
        public async Task DeleteScoreCard_ReturnsNotFound()
        {
            // Arrange
            _mockScoreCardService.Setup(service => service.DeleteScoreCard(1)).ReturnsAsync(false);

            // Act
            var result = await _scoreCardController.DeleteScoreCard(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("ScoreCard with ID 1 not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetScoreCard_ReturnsOk()
        {
            // Arrange
            var scoreCardResponseDto = new ScoreCardResponseDTO { QuizId = 1, Username = "sudu", Score = 30, Acuuracy= 50 };
             _mockScoreCardService.Setup( service => service.GetScoreCard(1)).ReturnsAsync(scoreCardResponseDto);

            // Act
            var result = await _scoreCardController.GetScoreCard(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(scoreCardResponseDto, okResult.Value);
        }

        [Test]
        public async Task GetScoreCard_ReturnsNotFound()
        {
            // Arrange
            _mockScoreCardService.Setup(service => service.GetScoreCard(1)).ThrowsAsync(new NotFoundException("Not found"));

            // Act
            var result = await _scoreCardController.GetScoreCard(1);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual("ScoreCard with ID 1 not found.", notFoundResult.Value);
        }
    }
}
