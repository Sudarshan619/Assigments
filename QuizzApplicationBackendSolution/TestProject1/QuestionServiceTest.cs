using Moq;
using AutoMapper;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuizzApplicationBackend.Exceptions;

namespace QuizzApplicationBackend.Tests
{
    public class QuestionServiceTest
    {
        private readonly Mock<IRepository<int, Question>> _mockQuestionRepository;
        private readonly Mock<IRepository<int, Option>> _mockOptionRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly QuestionService _questionService;

        public QuestionServiceTest()
        {
            _mockQuestionRepository = new Mock<IRepository<int, Question>>();
            _mockOptionRepository = new Mock<IRepository<int, Option>>();
            _mockMapper = new Mock<IMapper>();
            _questionService = new QuestionService(_mockQuestionRepository.Object, _mockOptionRepository.Object, _mockMapper.Object);
        }

        // Test for CreateQuestion method
        [Test]
        public async Task CreateQuestion_ShouldReturnTrue_WhenQuestionIsAdded()
        {
            var questionDto = new QuestionDTO { QuestionName = "Test Question", Category = 0 };
            var questionEntity = new Question { QuestionName = "Test Question", Category = 0 };

            _mockMapper.Setup(m => m.Map<Question>(questionDto)).Returns(questionEntity);
            _mockQuestionRepository.Setup(r => r.Add(It.IsAny<Question>())).ReturnsAsync(questionEntity);

            var result = await _questionService.CreateQuestion(questionDto);

            Assert.True(result);
        }

        [Test]
        public async Task CreateQuestion_ShouldReturnFalse_WhenQuestionNotAdded()
        {
            var questionDto = new QuestionDTO { QuestionName = "Test Question", Category = 0 };
            var questionEntity = new Question { QuestionName = "Test Question", Category = 0 };

            _mockMapper.Setup(m => m.Map<Question>(questionDto)).Returns(questionEntity);
            _mockQuestionRepository.Setup(r => r.Add(It.IsAny<Question>())).ReturnsAsync((Question)null);

            var result = await _questionService.CreateQuestion(questionDto);

            Assert.False(result);
        }

        // Test for DeleteQuestion method
        [Test]
        public async Task DeleteQuestion_ShouldReturnTrue_WhenQuestionIsDeleted()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Delete(questionId)).ReturnsAsync(new Question());

            var result = await _questionService.DeleteQuestion(questionId);

            Assert.True(result);
        }

        [Test]
        public async Task DeleteQuestion_ShouldThrowNotFoundException_WhenQuestionNotFound()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Delete(questionId)).ThrowsAsync(new NotFoundException("Question not found"));

            var exception = Assert.ThrowsAsync<NotFoundException>(() => _questionService.DeleteQuestion(questionId));
            Assert.AreEqual("Question with ID 1 not found.", exception.Message);
        }

        // Test for AddOptionsToQuestion method
        [Test]
        public async Task AddOptionsToQuestion_ShouldReturnTrue_WhenOptionsAreAdded()
        {
            var questionId = 1;
            var questionEntity = new Question { QuestionId = questionId, QuestionName = "Test Question" };
            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = questionId, Text = "Option 1" },
                new Option { OptionId = 2, QuestionId = questionId, Text = "Option 2" }
            };

            _mockQuestionRepository.Setup(r => r.Get(questionId)).ReturnsAsync(questionEntity);
            _mockOptionRepository.Setup(r => r.GetAll()).ReturnsAsync(options);

            var result = await _questionService.AddOptionsToQuestion(questionId);

            Assert.True(result);
            Assert.AreEqual(2, questionEntity.Options.Count);
        }

        // Test for GetQuestion method
        [Test]
        public async Task GetQuestion_ShouldReturnQuestionWithOptions_WhenValidIdIsProvided()
        {
            var questionId = 1;
            var questionEntity = new Question { QuestionId = questionId, QuestionName = "Test Question", Category = 0 };
            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = questionId, Text = "Option 1" }
            };

            _mockQuestionRepository.Setup(r => r.Get(questionId)).ReturnsAsync(questionEntity);
            _mockOptionRepository.Setup(r => r.GetAll()).ReturnsAsync(options);

            var result = await _questionService.GetQuestion(questionId);

            Assert.AreEqual("Test Question", result.QuestionName);
           
        }

        [Test]
        public async Task GetAllQuestions_ShouldReturnQuestionsWithOptions()
        {
            var questions = new List<Question>
            {
                new Question { QuestionId = 1, QuestionName = "Question 1", Category = 0 },
                new Question { QuestionId = 2, QuestionName = "Question 2",Category = 0 }
            };
            var options = new List<Option>
            {
                new Option { OptionId = 1, QuestionId = 1,Text = "Option 1" },
                new Option { OptionId = 2, QuestionId = 2, Text = "Option 2" }
            };

            _mockQuestionRepository.Setup(r => r.GetAll()).ReturnsAsync(questions);
            _mockOptionRepository.Setup(r => r.GetAll()).ReturnsAsync(options);

            var result = await _questionService.GetAllQuestions(1);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Option 1", result.First().Options.First().Text);
        }

        // Test for EditQuestion method
        [Test]
        public async Task EditQuestion_ShouldReturnTrue_WhenQuestionIsUpdated()
        {
            var questionId = 1;
            var existingQuestion = new Question { QuestionId = questionId, QuestionName = "Old Question", Category = 0 };
            var updatedQuestionDto = new QuestionDTO { QuestionName = "Updated Question", Category = 0 };
            var updatedQuestion = new Question { QuestionId = questionId, QuestionName = "Updated Question", Category = 0 };

            _mockQuestionRepository.Setup(r => r.Get(questionId)).ReturnsAsync(existingQuestion);
            _mockMapper.Setup(m => m.Map(updatedQuestionDto, existingQuestion)).Returns(updatedQuestion);
            _mockQuestionRepository.Setup(r => r.Update(questionId, updatedQuestion)).ReturnsAsync(updatedQuestion);

            var result = await _questionService.EditQuestion(questionId, updatedQuestionDto);

            Assert.True(result);
        }
    }
}
