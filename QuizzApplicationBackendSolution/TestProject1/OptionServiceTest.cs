using Moq;
using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Exceptions;

namespace QuizzApplicationBackend.Tests
{
    public class OptionServiceTest
    {
        private Mock<IRepository<int, Option>> _mockOptionRepository;
        private Mock<IMapper> _mockMapper;
        private OptionService _optionService;

        [SetUp]
        public void Setup()
        {
            _mockOptionRepository = new Mock<IRepository<int, Option>>();
            _mockMapper = new Mock<IMapper>();
            _optionService = new OptionService(_mockOptionRepository.Object, _mockMapper.Object);
        }

        [Test]
        public async Task CreateOption_ValidOptionDTO_ReturnsTrue()
        {
            // Arrange
            var optionDto = new OptionDTO { Text = "Option 1", IsCorrect = true };
            var option = new Option { OptionId = 1, Text = "Option 1", IsCorrect = true };

            _mockMapper.Setup(m => m.Map<Option>(optionDto)).Returns(option);
            _mockOptionRepository.Setup(repo => repo.Add(option)).ReturnsAsync(option);

            // Act
            var result = await _optionService.CreateOption(optionDto);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void CreateOption_ThrowsException_ReturnsFalse()
        {
            // Arrange
            var optionDto = new OptionDTO { Text = "Option 1", IsCorrect = true };

            _mockMapper.Setup(m => m.Map<Option>(optionDto)).Throws(new Exception("Mapping error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _optionService.CreateOption(optionDto));
        }

        [Test]
        public async Task DeleteOption_ValidId_ReturnsTrue()
        {
            // Arrange
            int optionId = 1;
            var option = new Option { OptionId = optionId, Text = "Option 1" };

            _mockOptionRepository.Setup(repo => repo.Delete(optionId)).ReturnsAsync(option);

            // Act
            var result = await _optionService.DeleteOption(optionId);

            // Assert
            Assert.IsTrue(result);
            _mockOptionRepository.Verify(repo => repo.Delete(optionId), Times.Once);
        }

        [Test]
        public void DeleteOption_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            int optionId = 999;

            _mockOptionRepository.Setup(repo => repo.Delete(optionId)).ThrowsAsync(new NotFoundException("Option not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _optionService.DeleteOption(optionId));
        }

        [Test]
        public async Task EditOption_ValidIdAndDTO_ReturnsTrue()
        {
            // Arrange
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option", IsCorrect = false };
            var existingOption = new Option { OptionId = optionId, Text = "Old Option", IsCorrect = true };
            var updatedOption = new Option { OptionId = optionId, Text = "Updated Option", IsCorrect = false };

            _mockOptionRepository.Setup(repo => repo.Get(optionId)).ReturnsAsync(existingOption);
            _mockMapper.Setup(m => m.Map(optionDto, existingOption)).Returns(updatedOption);
            _mockOptionRepository.Setup(repo => repo.Update(optionId, updatedOption)).ReturnsAsync(updatedOption);

            // Act
            var result = await _optionService.EditOption(optionId, optionDto);

            // Assert
            Assert.IsTrue(result);
            _mockOptionRepository.Verify(repo => repo.Update(optionId, updatedOption), Times.Once);
        }

        [Test]
        public void EditOption_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            int optionId = 999;
            var optionDto = new OptionDTO { Text = "Updated Option", IsCorrect = false };

            _mockOptionRepository.Setup(repo => repo.Get(optionId)).ThrowsAsync(new NotFoundException("Option not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _optionService.EditOption(optionId, optionDto));
        }

        [Test]
        public async Task GetOption_ValidId_ReturnsOptionDTO()
        {
            // Arrange
            int optionId = 1;
            var option = new Option { OptionId = optionId, Text = "Option 1", IsCorrect = true };
            var optionDto = new OptionDTO { Text = "Option 1", IsCorrect = true };

            _mockOptionRepository.Setup(repo => repo.Get(optionId)).ReturnsAsync(option);
            _mockMapper.Setup(m => m.Map<OptionDTO>(option)).Returns(optionDto);

            // Act
            var result = await _optionService.GetOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Option 1", result.Text);
        }

        [Test]
        public void GetOption_InvalidId_ThrowsNotFoundException()
        {
            // Arrange
            int optionId = 999;

            _mockOptionRepository.Setup(repo => repo.Get(optionId)).ThrowsAsync(new NotFoundException("Option not found"));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _optionService.GetOption(optionId));
        }

        [Test]
        public async Task GetAllOptions_ReturnsListOfOptionDTOs()
        {
            // Arrange
            var options = new List<Option>
            {
                new Option { OptionId = 1, Text = "Option 1", IsCorrect = true },
                new Option { OptionId = 2, Text = "Option 2", IsCorrect = false }
            };
            var optionDtos = new List<OptionDTO>
            {
                new OptionDTO { Text = "Option 1", IsCorrect = true },
                new OptionDTO { Text = "Option 2", IsCorrect = false }
            };

            _mockOptionRepository.Setup(repo => repo.GetAll()).ReturnsAsync(options);
            _mockMapper.Setup(m => m.Map<IEnumerable<OptionDTO>>(options)).Returns(optionDtos);

            // Act
            var result = await _optionService.GetAllOptions(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public void GetAllOptions_EmptyCollection_ThrowsCollectionEmptyException()
        {
            // Arrange
            _mockOptionRepository.Setup(repo => repo.GetAll()).ThrowsAsync(new CollectionEmptyException("No options available"));

            // Act & Assert
            Assert.ThrowsAsync<CollectionEmptyException>(() => _optionService.GetAllOptions(1));
        }
    }
}
