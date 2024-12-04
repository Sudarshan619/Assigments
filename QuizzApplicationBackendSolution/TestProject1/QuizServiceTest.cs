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
        private Mock<IRepository<int, Quiz>> _quizRepositoryMock;
        private Mock<IRepository<int, Question>> _questionRepositoryMock;
        private Mock<IRepository<int, Option>> _optionRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private QuizService _service;

        [SetUp]
        public void Setup()
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

            var result = await _service.CreateQuiz(quizDto);

            Assert.True(result);
        }

        [Test]
        public async Task CreateQuiz_ThrowsException()
        {
            var quizDto = new QuizDTO { Title = "Sample Quiz" };
            _quizRepositoryMock.Setup(r => r.Add(It.IsAny<Quiz>())).ThrowsAsync(new Exception("Internal server error"));

            Assert.ThrowsAsync<Exception>(async () => await _service.CreateQuiz(quizDto));
        }

        [Test]
        public async Task DeleteQuiz_WhenQuizIsDeleted_ReturnsTrue()
        {
            var quiz = new Quiz { QuizId = 1 };
            _quizRepositoryMock.Setup(r => r.Delete(1)).ReturnsAsync(quiz);

            var result = await _service.DeleteQuiz(1);

            Assert.True(result);
        }

        [Test]
        public void DeleteQuiz_ThrowsNotFoundException()
        {
            _quizRepositoryMock.Setup(r => r.Delete(It.IsAny<int>())).ThrowsAsync(new NotFoundException("Quiz not found"));

            Assert.ThrowsAsync<NotFoundException>(async () => await _service.DeleteQuiz(999));
        }

        [Test]
        public void DeleteQuiz_ThrowsException()
        {
            _quizRepositoryMock.Setup(r => r.Delete(It.IsAny<int>())).ThrowsAsync(new Exception("Internal server error"));

            Assert.ThrowsAsync<Exception>(async () => await _service.DeleteQuiz(1));
        }

        [Test]
        public async Task GetQuiz_ReturnsQuizQuestionResponseDTO()
        {
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

            var result = await _service.GetQuiz(1);

            Assert.AreEqual("Sample Question", result.questions.First().QuestionName);
            Assert.AreEqual(10, result.MaxPoints);
        }

        [Test]
        public void GetQuiz_ThrowsNotFoundException()
        {
            _quizRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ReturnsAsync((Quiz)null);

            Assert.ThrowsAsync<NotFoundException>(async () => await _service.GetQuiz(999));
        }

        [Test]
        public void GetQuiz_ThrowsException()
        {
            _quizRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ThrowsAsync(new Exception("Internal server error"));

            Assert.ThrowsAsync<Exception>(async () => await _service.GetQuiz(1));
        }

        [Test]
        public async Task EditQuiz_ReturnsTrue()
        {
            var quizDto = new QuizDTO { Title = "Updated Quiz" };
            var quiz = new Quiz { QuizId = 1, Title = "Original Quiz" };

            _quizRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(quiz);
            _mapperMock.Setup(m => m.Map(quizDto, quiz)).Returns(quiz);
            _quizRepositoryMock.Setup(r => r.Update(1, quiz)).ReturnsAsync(quiz);

            var result = await _service.EditQuiz(1, quizDto);

            Assert.True(result);
        }

        [Test]
        public void EditQuiz_ThrowsNotFoundException()
        {
            _quizRepositoryMock.Setup(r => r.Get(It.IsAny<int>())).ThrowsAsync(new NotFoundException("Quiz not found"));

            Assert.ThrowsAsync<NotFoundException>(async () => await _service.EditQuiz(999, new QuizDTO()));
        }

        [Test]
        public void EditQuiz_ThrowsException()
        {
            _quizRepositoryMock.Setup(r => r.Update(It.IsAny<int>(), It.IsAny<Quiz>())).ThrowsAsync(new Exception("Internal server error"));

            Assert.ThrowsAsync<Exception>(async () => await _service.EditQuiz(1, new QuizDTO()));
        }

        [Test]
        public async Task GetRandomQuestionsByCategory_ReturnsRandomQuestions()
        {
            var category = Categories.Geography;
            var difficulty = Difficulties.Easy;
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, Category = category, Points = 10 },
                new Question { QuestionId = 2, Category = category, Points = 20 }
            };
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);

            var result = await _service.GetRandomQuestionsByCategory(category, 1,difficulty,"rocket");

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(result.First().Category == category);
        }

        [Test]
        public void GetRandomQuestionsByCategory_ThrowsCollectionEmptyException()
        {
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Question>());

            Assert.ThrowsAsync<CollectionEmptyException>(async () =>
                await _service.GetRandomQuestionsByCategory(Categories.Politics, 1,Difficulties.Easy, "rocket"));
        }

        [Test]
        public async Task GetAllQuizzesWithQuestions_ReturnsQuizList()
        {
            var quizzes = new List<Quiz>
            {
                new Quiz { QuizId = 1, Title = "Sample Quiz", Category = 0, NoOfQuestions = 1, MaxPoint = 10 }
            };
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, Category = 0, Points = 10, QuestionName = "Sample Question" }
            };
            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = 1, Text = "Option 1" }
            };

            _quizRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(quizzes);
            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);
            _optionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(options);

            var result = await _service.GetAllQuizzesWithQuestions();

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Sample Quiz", result.First().Title);
        }

        [Test]
        public void GetAllQuizzesWithQuestions_ThrowsCollectionEmptyException()
        {
            _quizRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<Quiz>());

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _service.GetAllQuizzesWithQuestions());
        }

        [Test]
        public void GetAllQuizzesWithQuestions_ThrowsException()
        {
            _quizRepositoryMock.Setup(r => r.GetAll()).ThrowsAsync(new Exception("Internal server error"));

            Assert.ThrowsAsync<Exception>(async () => await _service.GetAllQuizzesWithQuestions());
        }

        [Test]
        public async Task GetAllQuizzesWithQuestionsByCategory_ValidCategory_ReturnsQuizzesWithQuestionsAndOptions()
        {
            // Arrange
            var category = Categories.Geography;

            var quizzes = new List<Quiz>
            {
                new Quiz { QuizId = 1, Title = "Science Quiz", Category = category, NoOfQuestions = 2, MaxPoint = 100, Difficulty = 0},
            };

            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionName = "What is H2O?", Category = category, Points = 50 },
                new Question { QuestionId = 2, QuestionName = "What is CO2?", Category = category, Points = 50 }
            };

            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = 1, Text = "Water" },
                new Option { OptionId = 2, QuestionId = 1, Text = "Oxygen" },
                new Option { OptionId = 3, QuestionId = 2, Text = "Carbon Dioxide" },
                new Option { OptionId = 4, QuestionId = 2, Text = "Nitrogen" }
            };

            _quizRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(quizzes);
            _questionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(questions);
            _optionRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(options);

            // Act
            var result = await _service.GetAllQuizzesWithQuestionsByCategory(category);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
            var quizResult = result.First();
            Assert.AreEqual("Science Quiz", quizResult.Title);
            Assert.AreEqual(100, quizResult.MaxPoints);
            Assert.AreEqual(2, quizResult.questions.Count);

            var firstQuestion = quizResult.questions.First();
            Assert.AreEqual("What is H2O?", firstQuestion.QuestionName);
            Assert.AreEqual(2, firstQuestion.Options.Count);
            Assert.AreEqual("Water", firstQuestion.Options.First().Text);
        }

        [Test]
        public void GetAllQuizzesWithQuestionsByCategory_NoQuizzesAvailable_ThrowsCollectionEmptyException()
        {
            // Arrange
            var category = Categories.Geography;
            var emptyQuizList = new List<Quiz>();

            _quizRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(emptyQuizList);

            // Act & Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () =>
                await _service.GetAllQuizzesWithQuestionsByCategory(category));
        }

        [Test]
        public void GetAllQuizzesWithQuestionsByCategory_UnexpectedException_ThrowsException()
        {
            // Arrange
            var category = Categories.Geography;
            _quizRepositoryMock.Setup(repo => repo.GetAll()).ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () =>
                await _service.GetAllQuizzesWithQuestionsByCategory(category));
            Assert.AreEqual("Error while retrieving quizzes: Database error", ex.Message);
        }
    }

}
