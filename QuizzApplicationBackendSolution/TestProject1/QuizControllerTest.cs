using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

namespace TestProject1
{
    [TestFixture]
    internal class QuizControllerTest
    {
        private Mock<IQuizService<IEnumerable<Question>, int>> _mockQuizService;
        private Mock<ILogger<QuizController>> _logger;
        private QuizController _controller;

        [SetUp]
        public void Setup()
        {
            
            _mockQuizService = new Mock<IQuizService<IEnumerable<Question>, int>>();
            _logger = new Mock<ILogger<QuizController>>();
            _controller = new QuizController(_mockQuizService.Object);
        }

        [Test]
        public async Task CreateQuiz_ShouldReturnOk_WhenQuizCreatedSuccessfully()
        {
            // Arrange
            var quizDto = new QuizDTO { Title = "Sample Quiz" };
            _mockQuizService.Setup(service => service.CreateQuiz(quizDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateQuiz(quizDto);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnNoContent_WhenQuizDeletedSuccessfully()
        {
            // Arrange
            int quizId = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnNotFound_WhenQuizDoesNotExist()
        {
            // Arrange
            int quizId = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Quiz with ID {quizId} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task EditQuiz_ShouldReturnNoContent_WhenQuizEditedSuccessfully()
        {
            // Arrange
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            _mockQuizService.Setup(service => service.EditQuiz(quizId, quizDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditQuiz(quizId, quizDto);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task GetQuiz_ShouldReturnQuiz_WhenQuizExists()
        {
            // Arrange
            int quizId = 1;
            var quiz = new QuizDTO { Title = "Sample Quiz" };
            //_mockQuizService.Setup(service => service.GetQuiz(quizId)).ReturnsAsync(quiz);

            // Act
            var result = await _controller.GetQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(quiz, okResult.Value);
        }

        [Test]
        public async Task GetQuiz_ShouldReturnNotFound_WhenQuizDoesNotExist()
        {
            // Arrange
            int quizId = 1;
            _mockQuizService.Setup(service => service.GetQuiz(quizId)).ThrowsAsync(new NotFoundException("Quiz not found."));

            // Act
            var result = await _controller.GetQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Quiz not found.", notFoundResult.Value);
        }
    }
}
