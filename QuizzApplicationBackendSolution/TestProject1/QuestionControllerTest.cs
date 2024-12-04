using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Exceptions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TestProject1
{
    [TestFixture]
    internal class QuestionControllerTest
    {
        private Mock<IQuestionService> _mockQuestionService;
        private Mock<ILogger<QuestionController>> _logger;
        private QuestionController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange mock dependencies
            _mockQuestionService = new Mock<IQuestionService>();
            _logger = new Mock<ILogger<QuestionController>>();
            _controller = new QuestionController(_mockQuestionService.Object);
        }

        [Test]
        public async Task CreateQuestion_ShouldReturnOk()
        {
            // Arrange
            var questionDto = new QuestionDTO { QuestionName = "Sample question?" };
            _mockQuestionService.Setup(service => service.CreateQuestion(questionDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateQuestion(questionDto);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task CreateQuestion_ShouldReturnBadRequest()
        {
            // Arrange
            var questionDto = new QuestionDTO { QuestionName = "Sample question?" };
            _mockQuestionService.Setup(service => service.CreateQuestion(questionDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.CreateQuestion(questionDto);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to create question", badRequestResult.Value);
        }

        [Test]
        public async Task GetQuestion_ShouldReturnOk()
        {
            // Arrange
            int questionId = 1;
            var question = new QuestionDTO { QuestionName = "Sample question?" };
            

            // Act
            var result = await _controller.GetQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(question, okResult.Value);
        }

        [Test]
        public async Task GetQuestion_ShouldReturnNotFound_WhenQuestionDoesNotExist()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.GetQuestion(questionId)).ThrowsAsync(new NotFoundException("Question not found."));

            // Act
            var result = await _controller.GetQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Question not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllQuestions_ShouldReturnOk_WhenQuestionsExist()
        {
            // Arrange
            var questions = new List<QuestionResponseDTO> { new QuestionResponseDTO { QuestionName = "Sample question?" } };
            _mockQuestionService.Setup(service => service.GetAllQuestions(1)).ReturnsAsync(questions);

            // Act
            var result = await _controller.GetAllQuestions(1);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(questions, okResult.Value);
        }

        [Test]
        public async Task GetAllQuestions_ShouldReturnNotFound_WhenQuestionsDoNotExist()
        {
            // Arrange
            _mockQuestionService.Setup(service => service.GetAllQuestions(1)).ThrowsAsync(new CollectionEmptyException("No questions available."));

            // Act
            var result = await _controller.GetAllQuestions(1);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No questions available.", notFoundResult.Value);
        }

        [Test]
        public async Task EditQuestion_ShouldReturnNoContent_WhenQuestionEditedSuccessfully()
        {
            // Arrange
            int questionId = 1;
            var questionDto = new EditQuestionDTO { QuestionName = "Updated question?" };
            _mockQuestionService.Setup(service => service.EditQuestion(questionId, questionDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditQuestion(questionId, questionDto);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task EditQuestion_ShouldReturnBadRequest_WhenQuestionEditFails()
        {
            // Arrange
            int questionId = 1;
            var questionDto = new EditQuestionDTO { QuestionName = "Updated question?" };
            _mockQuestionService.Setup(service => service.EditQuestion(questionId, questionDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.EditQuestion(questionId, questionDto);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to edit question", badRequestResult.Value);
        }

        [Test]
        public async Task DeleteQuestion_ShouldReturnNoContent_WhenQuestionDeletedSuccessfully()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.DeleteQuestion(questionId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteQuestion_ShouldReturnBadRequest_WhenQuestionDeleteFails()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.DeleteQuestion(questionId)).ReturnsAsync(false);

            // Act
            var result = await _controller.DeleteQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to delete question", badRequestResult.Value);
        }

        [Test]
        public async Task AddOptionsToQuestion_ShouldReturnOk_WhenOptionsAddedSuccessfully()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.AddOptionsToQuestion(questionId)).ReturnsAsync(true);

            // Act
            var result = await _controller.AddOptionsToQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual("Options added to question successfully", okResult.Value);
        }

        [Test]
        public async Task AddOptionsToQuestion_ShouldReturnBadRequest_WhenOptionsAddFails()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.AddOptionsToQuestion(questionId)).ReturnsAsync(false);

            // Act
            var result = await _controller.AddOptionsToQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to add options to question", badRequestResult.Value);
        }

        [Test]
        public async Task GetQuestion_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.GetQuestion(questionId)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.GetQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Unexpected error", objectResult.Value);
        }

        [Test]
        public async Task GetAllQuestions_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            _mockQuestionService.Setup(service => service.GetAllQuestions(1)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.GetAllQuestions(1);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Unexpected error", objectResult.Value);
        }

        [Test]
        public async Task EditQuestion_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            int questionId = 1;
            var questionDto = new EditQuestionDTO { QuestionName = "Updated question?" };
            _mockQuestionService.Setup(service => service.EditQuestion(questionId, questionDto)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.EditQuestion(questionId, questionDto);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Unexpected error", objectResult.Value);
        }

        [Test]
        public async Task DeleteQuestion_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.DeleteQuestion(questionId)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.DeleteQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Unexpected error", objectResult.Value);
        }

        [Test]
        public async Task AddOptionsToQuestion_ShouldReturnInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.AddOptionsToQuestion(questionId)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.AddOptionsToQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Unexpected error", objectResult.Value);
        }

        [Test]
        public async Task EditQuestion_ShouldReturnNotFound_WhenQuestionDoesNotExist()
        {
            // Arrange
            int questionId = 1;
            var questionDto = new EditQuestionDTO { QuestionName = "Updated question?" };
            _mockQuestionService.Setup(service => service.EditQuestion(questionId, questionDto))
                                .ThrowsAsync(new NotFoundException("Question not found."));

            // Act
            var result = await _controller.EditQuestion(questionId, questionDto);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Question not found.", notFoundResult.Value);
        }

        [Test]
        public async Task DeleteQuestion_ShouldReturnNotFound_WhenQuestionDoesNotExist()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.DeleteQuestion(questionId))
                                .ThrowsAsync(new NotFoundException("Question not found."));

            // Act
            var result = await _controller.DeleteQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Question not found.", notFoundResult.Value);
        }

        [Test]
        public async Task AddOptionsToQuestion_ShouldReturnNotFound_WhenQuestionDoesNotExist()
        {
            // Arrange
            int questionId = 1;
            _mockQuestionService.Setup(service => service.AddOptionsToQuestion(questionId))
                                .ThrowsAsync(new NotFoundException("Question not found."));

            // Act
            var result = await _controller.AddOptionsToQuestion(questionId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Question not found.", notFoundResult.Value);
        }


    }
}
