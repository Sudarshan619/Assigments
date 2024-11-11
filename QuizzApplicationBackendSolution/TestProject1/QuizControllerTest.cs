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
        public async Task CreateQuiz_ShouldReturnOk()
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
        public async Task DeleteQuiz_ShouldReturnOk()
        {
            // Arrange
            int quizId = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as OkObjectResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(200, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnNotFound()
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
        public async Task EditQuiz_ShouldReturnNoContent()
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
        public async Task GetQuiz_ShouldReturnQuiz()
        {
            // Arrange
            int quizId = 1;
            var quiz = new QuizQuestionReponseDTO
            {
                MaxPoints = 100,
                questions = new List<QuestionResponseDTO>

          {
          new QuestionResponseDTO
          {
            QuestionId = 1,
            Category = 0,
            QuestionName = "Sample question 1",
            Points = 4,
            Options = new List<OptionResponseDTO>
            {
                new OptionResponseDTO
                {
                    OptionId = 1,
                    Text = "Option 1 text"
                },
                new OptionResponseDTO
                {
                    OptionId = 2,
                    Text = "Option 2 text"
                }
            }
           }
         
        
          }
            };
        
            _mockQuizService.Setup(service => service.GetQuiz(quizId)).ReturnsAsync(quiz);

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
        public async Task GetQuiz_ShouldReturnNotFound()
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

        [Test]
        public async Task GetQuiz_ShouldReturnInternalServerError()
        {
            // Arrange
            int quizId = 1;
           
            
            _mockQuizService.Setup(service => service.GetQuiz(quizId)).ThrowsAsync(new Exception("Internal server error."));

            // Act
            var result = await _controller.GetQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(500, notFoundResult.StatusCode);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnInternalServerError()
        {
            // Arrange
            int quizId = 1;


            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ThrowsAsync(new Exception("Internal server error."));

            // Act
            var result = await _controller.DeleteQuiz(quizId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(500, notFoundResult.StatusCode);
        }
    }
}
