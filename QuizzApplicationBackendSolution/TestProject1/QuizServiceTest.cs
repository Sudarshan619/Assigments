using Moq;
using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Exceptions;

namespace QuizzApplicationBackend.Services.Tests
{
    public class QuizServiceTest
    {
        private readonly Mock<IRepository<int, Question>> _questionRepositoryMock;
        private readonly Mock<IRepository<int, Option>> _optionRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly QuestionService _service;

        public QuizServiceTest()
        {
            _questionRepositoryMock = new Mock<IRepository<int, Question>>();
            _optionRepositoryMock = new Mock<IRepository<int, Option>>();
            _mapperMock = new Mock<IMapper>();
            _service = new QuestionService(_questionRepositoryMock.Object, _optionRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateQuestion_WhenQuestionIsCreated()
        {
            // Arrange
            var questionDto = new QuestionDTO { QuestionName = "Sample Question", Category = 0 };
            var question = new Question { QuestionId = 1, QuestionName = "Sample Question", Category = 0 };
            _mapperMock.Setup(m => m.Map<Question>(questionDto)).Returns(question);
            _questionRepositoryMock.Setup(r => r.Add(It.IsAny<Question>())).ReturnsAsync(question);

            // Act
            var result = await _service.CreateQuestion(questionDto);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task DeleteQuestion_WhenQuestionIsDeleted()
        {
            // Arrange
            var question = new Question { QuestionId = 1 };
            _questionRepositoryMock.Setup(r => r.Delete(1)).ReturnsAsync(question);

            // Act
            var result = await _service.DeleteQuestion(1);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task DeleteQuestion_WhenQuestionDoesNotExist()
        {
            // Arrange
            _questionRepositoryMock.Setup(r => r.Delete(It.IsAny<int>())).ThrowsAsync(new NotFoundException("Question not found"));

            // Act & Assert
             Assert.ThrowsAsync<NotFoundException>(async ()=> await _service.DeleteQuestion(999));
        }

        [Test]
        public async Task GetQuestion_WhenQuestionExists()
        {
            // Arrange
            var question = new Question { QuestionId = 1, QuestionName = "Sample Question", Category = 0 };
            var options = new List<Option> { new Option { OptionId = 1, QuestionId = 1, Text = "Option 1" } };
            var questionResponseDto = new QuestionResponseDTO { QuestionName = "Sample Question", Category = 0, Options = new List<OptionDTO>() };

            _questionRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(question);
            _optionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(options);
            _mapperMock.Setup(m => m.Map<ICollection<OptionDTO>>(options)).Returns(questionResponseDto.Options);

            // Act
            var result = await _service.GetQuestion(1);

            // Assert
            Assert.AreEqual(questionResponseDto.QuestionName, result.QuestionName);
            Assert.AreEqual(questionResponseDto.Category, result.Category);
        }

        [Test]
        public async Task GetAllQuestions_WhenQuestionsExist()
        {
            // Arrange
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionName = "Question 1" },
                new Question { QuestionId = 2, QuestionName = "Question 2" }
            };
            var questionDtos = new List<QuestionDTO>
            {
                new QuestionDTO { QuestionName = "Question 1" },
                new QuestionDTO { QuestionName = "Question 2" }
            };

            _questionRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(questions);
            _mapperMock.Setup(m => m.Map<IEnumerable<QuestionDTO>>(questions)).Returns(questionDtos);

            // Act
            var result = await _service.GetAllQuestions();

            // Assert
            Assert.AreEqual(questionDtos.Count, result.Count());
        }

        [Test]
        public async Task EditQuestion_WhenQuestionIsUpdated()
        {
            // Arrange
            var questionDto = new QuestionDTO { QuestionName = "Updated Question" };
            var question = new Question { QuestionId = 1, QuestionName = "Original Question" };

            _questionRepositoryMock.Setup(r => r.Get(1)).ReturnsAsync(question);
            _mapperMock.Setup(m => m.Map(questionDto, question)).Returns(question);
            _questionRepositoryMock.Setup(r => r.Update(1, question)).ReturnsAsync(question);

            // Act
            var result = await _service.EditQuestion(1, questionDto);

            // Assert
            Assert.True(result);
        }
    }
}
