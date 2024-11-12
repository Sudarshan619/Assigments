using Moq;
using AutoMapper;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
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

        // Exception test for CreateQuestion method
        [Test]
        public void CreateQuestion_ShouldThrowException_WhenExceptionOccurs()
        {
            var questionDto = new QuestionDTO { QuestionName = "Test Question", Category = 0 };

            _mockMapper.Setup(m => m.Map<Question>(questionDto)).Throws(new Exception("Error while creating question"));

            var exception = Assert.ThrowsAsync<Exception>(() => _questionService.CreateQuestion(questionDto));
            Assert.AreEqual("Error while creating question", exception.Message);
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

        [Test]
        public void DeleteQuestion_ShouldThrowException_WhenOtherExceptionOccurs()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Delete(questionId)).ThrowsAsync(new Exception("Database error"));

            var exception = Assert.ThrowsAsync<Exception>(() => _questionService.DeleteQuestion(questionId));
            Assert.AreEqual("Error while deleting question: Database error", exception.Message);
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

        [Test]
        public void AddOptionsToQuestion_ShouldThrowException_WhenOtherExceptionOccurs()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Get(questionId)).ThrowsAsync(new Exception("Database error"));

            var exception = Assert.ThrowsAsync<Exception>(() => _questionService.AddOptionsToQuestion(questionId));
            Assert.AreEqual("Database error", exception.Message);
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
        public void GetQuestion_ShouldThrowNotFoundException_WhenQuestionNotFound()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Get(questionId)).ThrowsAsync(new NotFoundException("Question not found"));

            Assert.ThrowsAsync<NotFoundException>(() => _questionService.GetQuestion(questionId));
        }

        [Test]
        public void GetQuestion_ShouldThrowException_WhenOtherExceptionOccurs()
        {
            var questionId = 1;
            _mockQuestionRepository.Setup(r => r.Get(questionId)).ThrowsAsync(new Exception("Database error"));

            Assert.ThrowsAsync<Exception>(() => _questionService.GetQuestion(questionId));
        }

        // Test for GetAllQuestions method
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
        }

        [Test]
        public void GetAllQuestions_ShouldThrowCollectionEmptyException_WhenNoQuestionsAvailable()
        {
            _mockQuestionRepository.Setup(r => r.GetAll()).ThrowsAsync(new CollectionEmptyException("No questions available."));

            Assert.ThrowsAsync<CollectionEmptyException>(() => _questionService.GetAllQuestions(1));
        }

        [Test]
        public void GetAllQuestions_ShouldThrowException_WhenOtherExceptionOccurs()
        {
            _mockQuestionRepository.Setup(r => r.GetAll()).ThrowsAsync(new Exception("Database error"));

            var exception = Assert.ThrowsAsync<Exception>(() => _questionService.GetAllQuestions(1));
            Assert.AreEqual("Error while retrieving questions: Database error", exception.Message);
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

        [Test]
        public void EditQuestion_ShouldThrowNotFoundException_WhenQuestionNotFound()
        {
            var questionId = 1;
            var updatedQuestionDto = new QuestionDTO { QuestionName = "Updated Question", Category = 0 };

            _mockQuestionRepository.Setup(r => r.Get(questionId)).ThrowsAsync(new NotFoundException("Question not found"));

            var exception = Assert.ThrowsAsync<NotFoundException>(() => _questionService.EditQuestion(questionId, updatedQuestionDto));
            Assert.AreEqual("Question with ID 1 not found.", exception.Message);
        }

        [Test]
        public void EditQuestion_ShouldThrowException_WhenOtherExceptionOccurs()
        {
            var questionId = 1;
            var updatedQuestionDto = new QuestionDTO { QuestionName = "Updated Question", Category = 0 };

            _mockQuestionRepository.Setup(r => r.Get(questionId)).ThrowsAsync(new Exception("Database error"));

            var exception = Assert.ThrowsAsync<Exception>(() => _questionService.EditQuestion(questionId, updatedQuestionDto));
            Assert.AreEqual("Error while editing question: Database error", exception.Message);
        }
    }
}
