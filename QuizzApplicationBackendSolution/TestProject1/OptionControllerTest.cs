using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using System;
using System.Collections.Generic;
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
            _mockOptionService = new Mock<IOptionService>();
            _controller = new OptionController(_mockOptionService.Object);
        }

        [Test]
        public async Task GetAllOptions_ShouldReturnOk()
        {
            var options = new List<OptionResponseDTO> { new OptionResponseDTO { Text = "Option 1" } };
            _mockOptionService.Setup(service => service.GetAllOptions(1)).ReturnsAsync(options);

            var result = await _controller.GetAllOptions(1);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(options, okResult.Value);
        }

        [Test]
        public async Task GetAllOptions_ShouldReturnNotFound()
        {
            _mockOptionService.Setup(service => service.GetAllOptions(1)).ThrowsAsync(new CollectionEmptyException("No options available."));

            var result = await _controller.GetAllOptions(1);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("No options available.", notFoundResult.Value);
        }

        [Test]
        public async Task GetOption_ShouldReturnOk_WhenOptionExists()
        {
            int optionId = 1;
            var option = new OptionResponseDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.GetOption(optionId)).ReturnsAsync(option);

            var result = await _controller.GetOption(optionId);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(option, okResult.Value);
        }

        [Test]
        public async Task GetOption_ShouldReturnNotFound_WhenOptionDoesNotExist()
        {
            int optionId = 1;
            _mockOptionService.Setup(service => service.GetOption(optionId)).ThrowsAsync(new NotFoundException("Option not found."));

            var result = await _controller.GetOption(optionId);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Option not found.", notFoundResult.Value);
        }

        [Test]
        public async Task CreateOption_ShouldReturnOk_WhenOptionCreatedSuccessfully()
        {
            var optionDto = new OptionDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.CreateOption(optionDto)).ReturnsAsync(true);

            var result = await _controller.CreateOption(optionDto);

            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task CreateOption_ShouldReturnBadRequest_WhenOptionCreationFails()
        {
            var optionDto = new OptionDTO { Text = "Option 1" };
            _mockOptionService.Setup(service => service.CreateOption(optionDto)).ReturnsAsync(false);

            var result = await _controller.CreateOption(optionDto);

            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("Could not create option.", badRequestResult.Value);
        }

        [Test]
        public async Task CreateOption_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            _controller.ModelState.AddModelError("Text", "Text is required");

            var result = await _controller.CreateOption(new OptionDTO());

            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual(_controller.ModelState, badRequestResult.Value);
        }

        [Test]
        public async Task EditOption_ShouldReturnNoContent_WhenOptionEditedSuccessfully()
        {
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option" };
            _mockOptionService.Setup(service => service.EditOption(optionId, optionDto)).ReturnsAsync(true);

            var result = await _controller.EditOption(optionId, optionDto);

            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task EditOption_ShouldReturnNotFound_WhenOptionEditFails()
        {
            int optionId = 1;
            var optionDto = new OptionDTO { Text = "Updated Option" };
            _mockOptionService.Setup(service => service.EditOption(optionId, optionDto)).ReturnsAsync(false);

            var result = await _controller.EditOption(optionId, optionDto);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual($"Option with ID {optionId} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task DeleteOption_ShouldReturnNoContent_WhenDeletedSuccessfully()
        {
            int optionId = 1;
            _mockOptionService.Setup(service => service.DeleteOption(optionId)).ReturnsAsync(true);

            var result = await _controller.DeleteOption(optionId);

            Assert.IsNotNull(result);
            var noContentResult = result as NoContentResult;
            Assert.IsNotNull(noContentResult);
            Assert.AreEqual(204, noContentResult.StatusCode);
        }

        [Test]
        public async Task DeleteOption_ShouldReturnNotFound_WhenDeletionFails()
        {
            int optionId = 1;
            _mockOptionService.Setup(service => service.DeleteOption(optionId)).ThrowsAsync(new NotFoundException("Option not found."));

            var result = await _controller.DeleteOption(optionId);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(404, notFoundResult.StatusCode);
            Assert.AreEqual("Option not found.", notFoundResult.Value);
        }

        [Test]
        public async Task CreateMultipleOption_ShouldReturnOk_WhenOptionsCreatedSuccessfully()
        {
            // Arrange
            var optionDtos = new List<OptionDTO>
    {
        new OptionDTO { Text = "Option 1" },
        new OptionDTO { Text = "Option 2" }
    };
            _mockOptionService.Setup(service => service.CreateOptionBulk(optionDtos)).ReturnsAsync(true);

            // Act
            var result = await _controller.CreateMultipleOption(optionDtos);

            // Assert
            Assert.IsNotNull(result);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(true, okResult.Value);
        }

        [Test]
        public async Task CreateMultipleOption_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Text", "Required");

            // Act
            var result = await _controller.CreateMultipleOption(new List<OptionDTO>());

            // Assert
            Assert.IsNotNull(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [Test]
        public async Task CreateMultipleOption_ShouldReturnInternalServerError_WhenExceptionOccurs()
        {
            // Arrange
            var optionDtos = new List<OptionDTO>
    {
        new OptionDTO { Text = "Option 1" },
        new OptionDTO { Text = "Option 2" }
    };
            _mockOptionService.Setup(service => service.CreateOptionBulk(optionDtos)).ThrowsAsync(new System.Exception("Unexpected error"));

            // Act
            var result = await _controller.CreateMultipleOption(optionDtos);

            // Assert
            Assert.IsNotNull(result);
            var internalServerErrorResult = result as ObjectResult;
            Assert.IsNotNull(internalServerErrorResult);
            Assert.AreEqual(500, internalServerErrorResult.StatusCode);
            Assert.AreEqual("Unexpected error", internalServerErrorResult.Value.ToString());
        }



    }
}
