using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var quizDto = new QuizDTO { Title = "Sample Quiz" };
            _mockQuizService.Setup(service => service.CreateQuiz(quizDto)).ReturnsAsync(true);

            var result = await _controller.CreateQuiz(quizDto);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnOk()
        {
            int quizId = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ReturnsAsync(true);

            var result = await _controller.DeleteQuiz(quizId);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }


        [Test]
        public async Task DeleteQuiz_ShouldReturnNotFound()
        {
            int quizId = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizId)).ReturnsAsync(false);

            var result = await _controller.DeleteQuiz(quizId);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Quiz with ID {quizId} not found.", notFoundResult.Value);
        }


        [Test]
        public async Task EditQuiz_ShouldReturnNoContent()
        {
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            _mockQuizService.Setup(service => service.EditQuiz(quizId, quizDto)).ReturnsAsync(true);

            var result = await _controller.EditQuiz(quizId, quizDto);

            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task EditQuiz_ShouldReturnNotFound()
        {
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            _mockQuizService.Setup(service => service.EditQuiz(quizId, quizDto)).ReturnsAsync(false);

            var result = await _controller.EditQuiz(quizId, quizDto);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Quiz with ID {quizId} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetQuiz_ShouldReturnQuiz()
        {
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
                            new OptionResponseDTO { OptionId = 1, Text = "Option 1 text" },
                            new OptionResponseDTO { OptionId = 2, Text = "Option 2 text" }
                        }
                    }
                }
            };
            _mockQuizService.Setup(service => service.GetQuiz(quizId)).ReturnsAsync(quiz);

            var result = await _controller.GetQuiz(quizId);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(quiz, okResult.Value);
        }

        [Test]
        public async Task GetQuiz_ShouldReturnNotFound()
        {
            int quizId = 1;
            _mockQuizService.Setup(service => service.GetQuiz(quizId)).ThrowsAsync(new NotFoundException("Quiz not found."));

            var result = await _controller.GetQuiz(quizId);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Quiz not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllQuizzes_ShouldReturnOk()
        {
            var quizzes = new List<QuizQuestionReponseDTO>
            {
                new QuizQuestionReponseDTO
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
                                new OptionResponseDTO { OptionId = 1, Text = "Option 1 text" },
                                new OptionResponseDTO { OptionId = 2, Text = "Option 2 text" }
                            }
                        }
                    }
                }
            };
            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestions()).ReturnsAsync(quizzes);

            var result = await _controller.GetAllQuizzes();

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(quizzes, okResult.Value);
        }

        [Test]
        public async Task GetAllQuizzes_ShouldReturnNotFound()
        {
            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestions()).ThrowsAsync(new CollectionEmptyException("No quizzes found."));

            var result = await _controller.GetAllQuizzes();

            Assert.IsNotNull(result);
            var notFoundResult = result as ObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No quizzes found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllQuizzes_ShouldReturnInternalServerError()
        {
            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestions()).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.GetAllQuizzes();

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task GetQuiz_ShouldReturnInternalServerError()
        {
            int quizid = 1;
            _mockQuizService.Setup(service => service.GetQuiz(quizid)).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.GetQuiz(quizid);

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task EditQuiz_ShouldReturnInternalServerError()
        {
            int quizid = 1;
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            _mockQuizService.Setup(service => service.EditQuiz(quizid,quizDto)).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.EditQuiz(quizid,quizDto);

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task DeleteQuiz_ShouldReturnInternalServerError()
        {
            int quizid = 1;
            _mockQuizService.Setup(service => service.DeleteQuiz(quizid)).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.DeleteQuiz(quizid);

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task CreateQuiz_ShouldReturnBadRequest()
        {
            int quizid = 1;
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            _mockQuizService.Setup(service => service.CreateQuiz(null)).ReturnsAsync(false);

            var result = await _controller.CreateQuiz(null);

            Assert.IsNotNull(result);
            var objectResult = result as BadRequestObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(400, objectResult.StatusCode);
            Assert.AreEqual("Quiz data is null.", objectResult.Value);
        }

        [Test]

        public async Task EditQuiz_ShouldReturnBadRequest()
        {
            int quizid = 1;
            _mockQuizService.Setup(service => service.EditQuiz(quizid, null)).ReturnsAsync(false);

            var result = await _controller.EditQuiz(quizid, null);

            Assert.IsNotNull(result);
            var objResult = result as BadRequestObjectResult;
            Assert.IsNotNull(objResult);
            Assert.AreEqual("Quiz data is null.", objResult.Value);
        }

        [Test]
        public async Task GetAllQuizzesByCategory_ShouldReturnQuizzes()
        {
            var category = Categories.Geography;
            var quizzes = new List<QuizQuestionReponseDTO>
    {
        new QuizQuestionReponseDTO
        {
            MaxPoints = 100,
            questions = new List<QuestionResponseDTO>
            {
                new QuestionResponseDTO
                {
                    QuestionId = 1,
                    Category = category,
                    QuestionName = "Sample question",
                    Points = 5,
                    Options = new List<OptionResponseDTO>
                    {
                        new OptionResponseDTO { OptionId = 1, Text = "Option 1" },
                        new OptionResponseDTO { OptionId = 2, Text = "Option 2" }
                    }
                }
            }
        }
    };

            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestionsByCategory(category)).ReturnsAsync(quizzes);

            var result = await _controller.GetAllQuizzes(category);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(quizzes, okResult.Value);
        }

        [Test]
        public async Task GetAllQuizzesByCategory_ShouldReturnNotFound()
        {
            var category = Categories.Geography;
            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestionsByCategory(category)).ThrowsAsync(new CollectionEmptyException("No quizzes found in this category."));

            var result = await _controller.GetAllQuizzes(category);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No quizzes found in this category.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllQuizzesByCategory_ShouldReturnInternalServerError()
        {
            var category = Categories.GeneralKnowledge;
            _mockQuizService.Setup(service => service.GetAllQuizzesWithQuestionsByCategory(category)).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.GetAllQuizzes(category);

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task CreateQuiz_ShouldReturnInternalServerError()
        {
            var quizDto = new QuizDTO { Title = "Sample Quiz" };
            _mockQuizService.Setup(service => service.CreateQuiz(quizDto)).ThrowsAsync(new Exception("Internal server error."));

            var result = await _controller.CreateQuiz(quizDto);

            Assert.IsNotNull(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error: Internal server error.", objectResult.Value);
        }

        [Test]
        public async Task EditQuiz_ShouldReturnNotFoundException()
        {
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Non-existent Quiz" };
            _mockQuizService.Setup(service => service.EditQuiz(quizId, quizDto)).ThrowsAsync(new NotFoundException("Quiz not found."));

            var result = await _controller.EditQuiz(quizId, quizDto);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Quiz not found.", notFoundResult.Value);
        }

        [Test]
        public async Task CreateQuiz_ShouldReturnBadRequest_EmptyTitle()
        {
            var quizDto = new QuizDTO { Title = "" };
            _mockQuizService.Setup(service => service.CreateQuiz(quizDto)).ReturnsAsync(false);

            var result = await _controller.CreateQuiz(quizDto);

            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Failed to create quiz.", badRequestResult.Value);
        }

        [Test]
        public async Task EditQuiz_ShouldReturnBadRequest_InvalidId()
        {
            int quizId = -1; // Invalid ID
            var quizDto = new QuizDTO { Title = "Invalid ID Quiz" };

            var result = await _controller.EditQuiz(quizId, null);

            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Quiz data is null.", badRequestResult.Value);
        }

    }
}
