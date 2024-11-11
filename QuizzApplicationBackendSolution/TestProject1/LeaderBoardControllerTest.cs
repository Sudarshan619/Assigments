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

    public class LeaderBoardControllerTest
    {
        private Mock<ILeaderBoardService> _mockLeaderBoardService;
        private LeaderBoardController _LeaderBoardController;

        [SetUp]
        public void Setup()
        {
            _mockLeaderBoardService = new Mock<ILeaderBoardService>();
            _LeaderBoardController = new LeaderBoardController(_mockLeaderBoardService.Object);
        }

        [Test]
        public async Task CreateLeaderBoard_ReturnsOk()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.CreateLeaderBoard(leaderBoardDto)).ReturnsAsync(true);

            var result = await _LeaderBoardController.CreateLeaderBoard(leaderBoardDto);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual("LeaderBoard created successfully.", okResult.Value);
        }

        [Test]
        public async Task CreateLeaderBoard_ReturnsBadRequest()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.CreateLeaderBoard(leaderBoardDto)).ReturnsAsync(false);

            var result = await _LeaderBoardController.CreateLeaderBoard(leaderBoardDto);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Failed to create LeaderBoard.", badRequestResult.Value);
        }

        [Test]
        public async Task CreateLeaderBoard_ReturnsBadRequest_NullInput()
        {
            var result = await _LeaderBoardController.CreateLeaderBoard(null);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("LeaderBoardDTo is required", badRequestResult.Value);
        }

        [Test]
        public async Task UpdateLeaderBoard_ReturnsNoContent()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.UpdateLeaderBoard(1, leaderBoardDto)).ReturnsAsync(true);

            var result = await _LeaderBoardController.UpdateLeaderBoard(1, leaderBoardDto);

            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateLeaderBoard_ReturnsNotFound()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.UpdateLeaderBoard(1, leaderBoardDto)).ReturnsAsync(false);

            var result = await _LeaderBoardController.UpdateLeaderBoard(1, leaderBoardDto);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("LeaderBoard with ID 1 not found.", notFoundResult.Value);
        }

        [Test]
        public async Task UpdateLeaderBoard_ReturnsBadRequest_NullInput()
        {
            var result = await _LeaderBoardController.UpdateLeaderBoard(1, null);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("LeaderBoardDTO is required", badRequestResult.Value);
        }

        [Test]
        public async Task DeleteLeaderBoard_ReturnsOk()
        {
            _mockLeaderBoardService.Setup(service => service.DeleteLeaderBoard(1)).ReturnsAsync(true);

            var result = await _LeaderBoardController.DeleteLeaderBoard(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual("LeaderBoard with ID 1 successfully deleted.", okResult.Value);
        }

        [Test]
        public async Task DeleteLeaderBoard_ReturnsNotFound()
        {
            _mockLeaderBoardService.Setup(service => service.DeleteLeaderBoard(1)).ReturnsAsync(false);

            var result = await _LeaderBoardController.DeleteLeaderBoard(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("LeaderBoard with ID 1 not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetLeaderBoard_ReturnsOk()
        {
            var leaderBoardResponseDto = new LeaderBoardResponseDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.GetLeaderBoard(1)).ReturnsAsync(leaderBoardResponseDto);

            var result = await _LeaderBoardController.GetLeaderBoard(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(leaderBoardResponseDto, okResult.Value);
        }

        [Test]
        public async Task GetLeaderBoard_ReturnsNotFound()
        {
            _mockLeaderBoardService.Setup(service => service.GetLeaderBoard(1)).ThrowsAsync(new NotFoundException("LeaderBoard with ID 1 not found."));

            var result = await _LeaderBoardController.GetLeaderBoard(1);

            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual("LeaderBoard with ID 1 not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllLeaderBoards_ReturnsOk()
        {
            var leaderBoardResponseDtos = new List<LeaderBoardResponseDTO>
            {
                new LeaderBoardResponseDTO { LeaderBoardName = "GK", Category = 0 }
            };
            _mockLeaderBoardService.Setup(service => service.GetAllLeaderBoard(1,5)).ReturnsAsync(leaderBoardResponseDtos);

            var result = await _LeaderBoardController.GetAllLeaderBoards(1,5);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(leaderBoardResponseDtos, okResult.Value);
        }

        [Test]
        public async Task GetAllLeaderBoards_ReturnsNotFound()
        {
            _mockLeaderBoardService.Setup(service => service.GetAllLeaderBoard(1, 5)).ThrowsAsync(new CollectionEmptyException("No leaderboards available."));

            var result = await _LeaderBoardController.GetAllLeaderBoards(1,5);

            Assert.IsNotNull(result);
            var notFoundResult = result as NotFoundObjectResult;
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }

        [Test]
        public async Task SortLeaderBoard_ReturnsOk()
        {
            var sortedLeaderBoard =  new LeaderBoardResponseDTO { LeaderBoardName = "GK", Category = 0 };
            
            _mockLeaderBoardService.Setup(service => service.SortLeaderBoard(Choice.Score, 1)).ReturnsAsync(sortedLeaderBoard);

            var result = await _LeaderBoardController.SortLeaderBoard(1, Choice.Score);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(sortedLeaderBoard, okResult.Value);
        }

        [Test]
        public async Task CreateLeaderBoard_ReturnsInternalServerError()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.CreateLeaderBoard(leaderBoardDto)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.CreateLeaderBoard(leaderBoardDto);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [Test]
        public async Task UpdateLeaderBoard_ReturnsInternalServerError()
        {
            var leaderBoardDto = new LeaderBoardDTO { LeaderBoardName = "GK", Category = 0 };
            _mockLeaderBoardService.Setup(service => service.UpdateLeaderBoard(1, leaderBoardDto)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.UpdateLeaderBoard(1, leaderBoardDto);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [Test]
        public async Task DeleteLeaderBoard_ReturnsInternalServerError()
        {
            _mockLeaderBoardService.Setup(service => service.DeleteLeaderBoard(1)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.DeleteLeaderBoard(1);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [Test]
        public async Task GetLeaderBoard_ReturnsInternalServerError()
        {
            _mockLeaderBoardService.Setup(service => service.GetLeaderBoard(1)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.GetLeaderBoard(1);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [Test]
        public async Task GetAllLeaderBoards_ReturnsInternalServerError()
        {
            _mockLeaderBoardService.Setup(service => service.GetAllLeaderBoard(1, 5)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.GetAllLeaderBoards(1,5);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

        [Test]
        public async Task SortLeaderBoard_ReturnsInternalServerError()
        {
            _mockLeaderBoardService.Setup(service => service.SortLeaderBoard(Choice.Score, 1)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _LeaderBoardController.SortLeaderBoard(1, Choice.Score);

            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult.StatusCode);
            Assert.AreEqual("Internal server error", objectResult.Value);
        }

    }
}
