using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using QuizzApplicationBackend.Controllers;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using System;
using System.Threading.Tasks;

namespace TestProject1
{
    public class QueryControllerTest
    {
        private readonly Mock<IQueryService> _mockQueryService;
        private readonly QueryController _controller;

        public QueryControllerTest()
        {
            _mockQueryService = new Mock<IQueryService>();
            _controller = new QueryController(_mockQueryService.Object);
        }

        [Test]
        public async Task CreateQuery_ValidQuery_ReturnsOk()
        {
            var queryDto = new QueryDTO { Description = "Test Query" };
            _mockQueryService.Setup(service => service.CreateQuery(queryDto)).ReturnsAsync(true);

            var result = await _controller.CreateQuery(queryDto);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual("Query created successfully", okResult.Value);
        }

        [Test]
        public async Task CreateQuery_NullQuery_ReturnsBadRequest()
        {
            var result = await _controller.CreateQuery(null);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.NotNull(badRequestResult);
            Assert.AreEqual("Query data is null", badRequestResult.Value);
        }

        [Test]
        public async Task CreateQuery_ServiceThrowsException_ReturnsInternalServerError()
        {
            var queryDto = new QueryDTO { Description = "Test Query" };
            _mockQueryService.Setup(service => service.CreateQuery(queryDto))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.CreateQuery(queryDto);

            var statusCodeResult = result as ObjectResult;
            Assert.NotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("A problem occurred while creating the query.", statusCodeResult.Value);
        }

        [Test]
        public async Task GetQuery_ValidId_ReturnsQuery()
        {
            int id = 1;
            var query = new QueryDTO { Description = "Test Query" };
            _mockQueryService.Setup(service => service.GetQuery(id)).ReturnsAsync(query);

            var result = await _controller.GetQuery(id);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(query, okResult.Value);
        }

        [Test]
        public async Task GetQuery_InvalidId_ReturnsNotFound()
        {
            int id = 99;
            _mockQueryService.Setup(service => service.GetQuery(id)).ReturnsAsync((QueryDTO)null);

            var result = await _controller.GetQuery(id);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual($"Query with ID {id} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetQuery_ServiceThrowsException_ReturnsInternalServerError()
        {
            int id = 1;
            _mockQueryService.Setup(service => service.GetQuery(id))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.GetQuery(id);

            var statusCodeResult = result as ObjectResult;
            Assert.NotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("A problem occurred while retrieving the query.", statusCodeResult.Value);
        }

        [Test]
        public async Task EditQuery_ValidQuery_ReturnsNoContent()
        {
            int id = 1;
            var queryDto = new QueryDTO { Description = "Updated Query" };
            _mockQueryService.Setup(service => service.EditQuery(id, queryDto)).ReturnsAsync(true);

            var result = await _controller.EditQuery(id, queryDto);

            var noContentResult = result as NoContentResult;
            Assert.NotNull(noContentResult);
        }

        [Test]
        public async Task EditQuery_InvalidId_ReturnsNotFound()
        {
            int id = 99;
            var queryDto = new QueryDTO { Description = "Updated Query" };
            _mockQueryService.Setup(service => service.EditQuery(id, queryDto)).ReturnsAsync(false);

            var result = await _controller.EditQuery(id, queryDto);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual($"Query with ID {id} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task EditQuery_ServiceThrowsException_ReturnsInternalServerError()
        {
            int id = 1;
            var queryDto = new QueryDTO { Description = "Updated Query" };
            _mockQueryService.Setup(service => service.EditQuery(id, queryDto))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.EditQuery(id, queryDto);

            var statusCodeResult = result as ObjectResult;
            Assert.NotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("A problem occurred while updating the query.", statusCodeResult.Value);
        }

        [Test]
        public async Task DeleteQuery_ValidId_ReturnsOk()
        {
            int id = 1;
            _mockQueryService.Setup(service => service.DeleteQuery(id)).ReturnsAsync(true);

            var result = await _controller.DeleteQuery(id);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual("Deleted Succesfully", okResult.Value);
        }

        [Test]
        public async Task DeleteQuery_InvalidId_ReturnsNotFound()
        {
            int id = 99;
            _mockQueryService.Setup(service => service.DeleteQuery(id)).ReturnsAsync(false);

            var result = await _controller.DeleteQuery(id);

            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual($"Query with ID {id} not found.", notFoundResult.Value);
        }

        [Test]
        public async Task DeleteQuery_ServiceThrowsException_ReturnsInternalServerError()
        {
            int id = 1;
            _mockQueryService.Setup(service => service.DeleteQuery(id))
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.DeleteQuery(id);

            var statusCodeResult = result as ObjectResult;
            Assert.NotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("A problem occurred while deleting the query.", statusCodeResult.Value);
        }

        [Test]
        public async Task GetAllQuery_ValidQuery_ReturnsOk()
        {
            var queries = new List<QueryDTO>
    {
        new QueryDTO {  Description = "Test Query 1" },
        new QueryDTO {  Description = "Test Query 2" }
    };
            _mockQueryService.Setup(service => service.GetAllQuery()).ReturnsAsync(queries);

            var result = await _controller.GetAllQuery();

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(queries, okResult.Value);
        }

        [Test]
        public async Task GetAllQuery_NoQueriesFound_ReturnsNotFound()
        {
            _mockQueryService.Setup(service => service.GetAllQuery()).ReturnsAsync((List<QueryDTO>)null);

            var result = await _controller.GetAllQuery();

            var notFoundResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundResult);
            Assert.AreEqual("Queries not found.", notFoundResult.Value);
        }

        [Test]
        public async Task GetAllQuery_ServiceThrowsException_ReturnsInternalServerError()
        {
            _mockQueryService.Setup(service => service.GetAllQuery())
                .ThrowsAsync(new Exception("Database error"));

            var result = await _controller.GetAllQuery();

            var statusCodeResult = result as ObjectResult;
            Assert.NotNull(statusCodeResult);
            Assert.AreEqual(500, statusCodeResult.StatusCode);
            Assert.AreEqual("A problem occurred while retrieving the query.", statusCodeResult.Value);
        }
    }
}
