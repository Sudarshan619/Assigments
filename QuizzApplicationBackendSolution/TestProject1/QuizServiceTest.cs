using Moq;
using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Exceptions;


namespace QuizzApplicationBackend.Services.Tests
{
    [TestFixture]
    public class QuizServiceTest
    {
        private readonly Mock<IRepository<int, Quiz>> _quizRepositoryMock;
        private readonly Mock<IRepository<int, Question>> _questionRepositoryMock;
        private readonly Mock<IRepository<int, Option>> _optionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly QuizService _service;

        public QuizServiceTest()
        {
            _quizRepositoryMock = new Mock<IRepository<int, Quiz>>();
            _questionRepositoryMock = new Mock<IRepository<int, Question>>();
            _optionRepositoryMock = new Mock<IRepository<int, Option>>();
            _mapperMock = new Mock<IMapper>();
            _service = new QuizService(_quizRepositoryMock.Object, _questionRepositoryMock.Object, _optionRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateQuiz_ReturnsTrue()
        {
            // Arrange
            var quizDto = new QuizDTO { CreatorId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, Category = 0, Points = 10 },
                new Question { QuestionId = 2, Category = 0, Points = 20 }
            };

            _mapperMock.Setup(m => m.Map<Quiz>(quizDto)).Returns(quiz);
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);
            _quizRepositoryMock.Setup(r => r.Add(It.IsAny<Quiz>())).ReturnsAsync(quiz);

            // Act
            var result = await _service.CreateQuiz(quizDto);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task DeleteQuiz_WhenQuizIsDeleted_ReturnsTrue()
        {
            // Arrange
            var quiz = new Quiz { QuizId = 1 };
            _quizRepositoryMock.Setup(r => r.Delete(1)).ReturnsAsync(quiz);

            // Act
            var result = await _service.DeleteQuiz(1);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task DeleteQuiz_ThrowsNotFoundException()
        {
            // Arrange
            _quizRepositoryMock.Setup(r => r.Delete(It.IsAny<int>())).ThrowsAsync(new NotFoundException("Quiz not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _service.DeleteQuiz(999));
        }

        [Test]
        public async Task GetQuiz_ReturnsQuizQuestionResponseDTO()
        {
            // Arrange
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 1, MaxPoint = 10 };
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, Category = 0, Points = 10, QuestionName = "Sample Question" }
            };
            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = 1, Text = "Option 1" }
            };

            _quizRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(quiz);
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);
            _optionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(options);

            // Act
            var result = await _service.GetQuiz(1);

            // Assert
            Assert.AreEqual("Sample Question", result.questions.First().QuestionName);
            Assert.AreEqual(10, result.MaxPoints);
        }

        [Test]
        public async Task EditQuiz_ReturnsTrue()
        {
            // Arrange
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            var quiz = new Quiz { QuizId = 1, Title = "Original Quiz" };

            _quizRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(quiz);
            _mapperMock.Setup(m => m.Map(quizDto, quiz)).Returns(quiz);
            _quizRepositoryMock.Setup(r => r.Update(1, quiz)).ReturnsAsync(quiz);

            // Act
            var result = await _service.EditQuiz(1, quizDto);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task GetRandomQuestionsByCategory_ReturnsRandomQuestions()
        {
            // Arrange
            var category = Categories.Geography;
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, Category = category, Points = 10 },
                new Question { QuestionId = 2, Category = category, Points = 20 }
            };
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);

            // Act
            var result = await _service.GetRandomQuestionsByCategory(category, 1);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.First().Category == category);
        }

        [Test]
        public void GetRandomQuestionsByCategory_ThrowsCollectionEmptyException()
        {
            // Arrange
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Question>());

            // Act & Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () =>
                await _service.GetRandomQuestionsByCategory(Categories.Politics, 1));
        }

        [Test]
        public void GetQuiz_ThrowsException()
        {
            // Arrange
            int quizId = 1;
            _quizRepositoryMock.Setup(r => r.Get(1)).ThrowsAsync(new Exception("Internal server error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _service.GetQuiz(1));
        }

        [Test]
        public void DeleteQuiz_ThrowsException()
        {
            // Arrange
            int quizId = 1;
            _quizRepositoryMock.Setup(r => r.Get(1)).ThrowsAsync(new Exception("Internal server error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _service.GetQuiz(1));
        }

        [Test]
        public void CreateQuiz_ThrowsException()
        {
            // Arrange
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            _quizRepositoryMock.Setup(r => r.Add(quiz)).ThrowsAsync(new Exception("Internal server error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () =>
                await _service.CreateQuiz(quizDto));
        }

        [Test]
        public async Task DeleteQuiz_ThrowsInternalServerError()
        {
            //Arrange
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Test" };
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            _quizRepositoryMock.Setup(e => e.Delete(1)).ThrowsAsync(new Exception("Internal server error"));

            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _service.DeleteQuiz(quizId));
        }

        [Test]

        public async Task EditQuiz_ThrowsAsync()
        {
            //Arrange
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };

            _quizRepositoryMock.Setup(e => e.Update(quizId, quiz)).ThrowsAsync(new Exception("Internal server error"));

            //Assert
            Assert.ThrowsAsync<Exception>(async () => await _service.EditQuiz(quizId, quizDto));
        }

        public async Task EditQuiz_ThrowsNotFound()
        {

            //Arrange
            int quizId = 1;
            var quizDto = new QuizDTO { Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };
            var quiz = new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 5 };

            _quizRepositoryMock.Setup(e => e.Get(quizId)).ThrowsAsync(new NotFoundException("Quiz not found"));


            //Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _service.EditQuiz(999,quizDto));
        }

        [Test]
        public async Task GetQuiz_ThrowsNotFound()
        {
            //Arrange
            int quizId = 1;
            _quizRepositoryMock.Setup(e=>e.Get(quizId)).ThrowsAsync(new NotFoundException("Quiz not found"));

            //Assert
            Assert.ThrowsAsync<NotFoundException>(async ()=> await _service.GetQuiz(999));
        }
    }
}
