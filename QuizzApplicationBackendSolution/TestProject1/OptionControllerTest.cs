﻿using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Exceptions;
using System.Threading.Tasks;

namespace TestProject1
{
    [TestFixture]
    internal class OptionControllerTest
    {
        private Mock<IOptionService> _mockOptionService;
        private OptionController _controller;

        [SetUp]
        public void Setup()
        {
            // Arrange mock dependencies
            _mockOptionService = new Mock<IOptionService>();
            _controller = new OptionController(_mockOptionService.Object);
        }

        [Test]
        public async Task GetAllOptions_ShouldReturnOk()
        {
            // Arrange
            var options = new List<OptionResponseDTO> { new OptionResponseDTO { Text = "Option 1" } };
            _mockOptionService.Setup(service => service.GetAllOptions(1)).ReturnsAsync(options);

            // Act
            var result = await _controller.GetAllOptions(1);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(options, okResult.Value);
        }

        [Test]
        public async Task GetAllOptions_ShouldReturnNotFound()
        {
            // Arrange
            _mockOptionService.Setup(service => service.GetAllOptions(1)).ThrowsAsync(new CollectionEmptyException("No options available."));

            // Act
            var result = await _controller.GetAllOptions(1);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task GetOption_ShouldReturnOk_WhenOptionExists()
        {
            // Arrange
            int optionId = 1;
            var option = new OptionResponseDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.GetOption(optionId)).ReturnsAsync(option);

            // Act
            var result = await _controller.GetOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(option, okResult.Value);
        }

        [Test]
        public async Task GetOption_ShouldReturnNotFound_WhenOptionDoesNotExist()
        {
            // Arrange
            int optionId = 1;
            _mockOptionService.Setup(service => service.GetOption(optionId)).ThrowsAsync(new NotFoundException("Option not found."));

            // Act
            var result = await _controller.GetOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Option not found.", notFoundResult.Value.ToString());
        }

        [Test]
        public async Task CreateOption_ShouldReturnOk_WhenOptionCreatedSuccessfully()
        {
            // Arrange
            var optionDto = new OptionDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.CreateOption(optionDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateOption(optionDto);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task CreateOption_ShouldReturnBadRequest_WhenOptionCreationFails()
        {
            // Arrange
            var optionDto = new OptionDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.CreateOption(optionDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.CreateOption(optionDto);

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Could not create option.", badRequestResult.Value);
        }

        [Test]
        public async Task EditOption_ShouldReturnNoContent_WhenOptionEditedSuccessfully()
        {
            // Arrange
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option" };
            _mockOptionService.Setup(service => service.EditOption(optionId, optionDto)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditOption(optionId, optionDto);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task EditOption_ShouldReturnNotFound_WhenOptionEditFails()
        {
            // Arrange
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option" };
            _mockOptionService.Setup(service => service.EditOption(optionId, optionDto)).ReturnsAsync(false);

            // Act
            var result = await _controller.EditOption(optionId, optionDto);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Option with ID {optionId} not found.", notFoundResult.Value.ToString());
        }

        [Test]
        public async Task DeleteOption_ShouldReturnNoContent()
        {
            // Arrange
            int optionId = 1;
            _mockOptionService.Setup(service => service.DeleteOption(optionId)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteOption_ShouldReturnNotFound()
        {
            // Arrange
            int optionId = 1;
            _mockOptionService.Setup(service => service.DeleteOption(optionId)).ThrowsAsync(new NotFoundException("Option with ID 1 not found."));

            // Act
            var result = await _controller.DeleteOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Option with ID {optionId} not found.", notFoundResult.Value.ToString());
        }

        [Test]
        public async Task GetOption_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            int optionId = 1;
            _mockOptionService.Setup(service => service.GetOption(optionId)).ThrowsAsync(new System.Exception("Unexpected error"));

            // Act
            var result = await _controller.GetOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Unexpected error", internalServerErrorResult.Value.ToString());
        }

        [Test]
        public async Task EditOption_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option" };
            _mockOptionService.Setup(service => service.EditOption(optionId, optionDto)).ThrowsAsync(new System.Exception("Unexpected error"));

            // Act
            var result = await _controller.EditOption(optionId, optionDto);

            // Assert
            Assert.IsNotNull(result);
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Unexpected error", internalServerErrorResult.Value.ToString());
        }

        [Test]
        public async Task DeleteOption_ShouldReturnInternalServerError()
        {
            // Arrange
            int optionId = 1;
            _mockOptionService.Setup(service => service.DeleteOption(optionId)).ThrowsAsync(new System.Exception("Unexpected error"));

            // Act
            var result = await _controller.DeleteOption(optionId);

            // Assert
            Assert.IsNotNull(result);
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Unexpected error", internalServerErrorResult.Value.ToString());
        }
       
     }
    

}
