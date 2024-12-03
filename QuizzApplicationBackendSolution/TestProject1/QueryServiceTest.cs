using AutoMapper;
using Moq;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;

namespace TestProject1
{
    public class QueryServiceTest
    {
        private readonly Mock<IRepository<int, Query>> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly QueryService _queryService;

        public QueryServiceTest()
        {
            _repositoryMock = new Mock<IRepository<int, Query>>();
            _mapperMock = new Mock<IMapper>();
            _queryService = new QueryService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task CreateQuery_ShouldReturnTrue_WhenQueryIsCreatedSuccessfully()
        {
            // Arrange
            var queryDto = new QueryDTO { Description = "Test Query" };
            var query = new Query { QueryId = 1, Description = "Test Query" };

            _mapperMock.Setup(m => m.Map<Query>(queryDto)).Returns(query);
            _repositoryMock.Setup(r => r.Add(query)).ReturnsAsync(query);

            // Act
            var result = await _queryService.CreateQuery(queryDto);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.Add(It.IsAny<Query>()), Times.Once);
        }

        [Test]
        public async Task CreateQuery_ShouldThrowException_WhenRepositoryThrowsException()
        {
            // Arrange
            var queryDto = new QueryDTO {Description = "Test Query" };

            _mapperMock.Setup(m => m.Map<Query>(queryDto)).Throws(new Exception("Error"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _queryService.CreateQuery(queryDto));
        }

        [Test]
        public async Task DeleteQuery_ShouldReturnTrue_WhenQueryIsDeletedSuccessfully()
        {
            // Arrange
            var queryId = 1;
            _repositoryMock.Setup(r => r.Delete(queryId)).ReturnsAsync(new Query());

            // Act
            var result = await _queryService.DeleteQuery(queryId);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.Delete(queryId), Times.Once);
        }

        [Test]
        public async Task DeleteQuery_ShouldThrowNotFoundException_WhenQueryDoesNotExist()
        {
            // Arrange
            var queryId = 1;
            _repositoryMock.Setup(r => r.Delete(queryId)).ThrowsAsync(new NotFoundException("Query not found."));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _queryService.DeleteQuery(queryId));
        }

        [Test]
        public async Task EditQuery_ShouldReturnTrue_WhenQueryIsUpdatedSuccessfully()
        {
            // Arrange
            var queryId = 1;
            var queryDto = new QueryDTO { Description = "Updated Query" };
            var existingQuery = new Query { QueryId = 1, Description = "Old Query" };
            var updatedQuery = new Query { QueryId = 1, Description = "Updated Query" };

            _repositoryMock.Setup(r => r.Get(queryId)).ReturnsAsync(existingQuery);
            _mapperMock.Setup(m => m.Map(queryDto, existingQuery)).Returns(updatedQuery);
            _repositoryMock.Setup(r => r.Update(queryId, updatedQuery)).ReturnsAsync(updatedQuery);

            // Act
            var result = await _queryService.EditQuery(queryId, queryDto);

            // Assert
            Assert.True(result);
            _repositoryMock.Verify(r => r.Update(queryId, updatedQuery), Times.Once);
        }

        [Test]
        public async Task EditQuery_ShouldThrowNotFoundException_WhenQueryDoesNotExist()
        {
            // Arrange
            var queryId = 1;
            var queryDto = new QueryDTO {  Description = "Updated Query" };

            _repositoryMock.Setup(r => r.Get(queryId)).ThrowsAsync(new NotFoundException("Query not found."));

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(() => _queryService.EditQuery(queryId, queryDto));
        }

        [Test]
        public async Task GetQuery_ShouldReturnQueryDto_WhenQueryExists()
        {
            // Arrange
            var queryId = 1;
            var query = new Query { QueryId = 1, Description = "Test Query" };
            var queryDto = new QueryDTO {  Description = "Test Query" };

            _repositoryMock.Setup(r => r.Get(queryId)).ReturnsAsync(query);
            _mapperMock.Setup(m => m.Map<QueryDTO>(query)).Returns(queryDto);

            // Act
            var result = await _queryService.GetQuery(queryId);

            // Assert
            Assert.AreEqual(queryDto, result);
            _repositoryMock.Verify(r => r.Get(queryId), Times.Once);
        }

        [Test]
        public async Task GetQuery_ShouldThrowNotFoundException_WhenQueryDoesNotExist()
        {
            // Arrange
            var queryId = 1;

            _repositoryMock.Setup(r => r.Get(queryId)).ThrowsAsync(new NotFoundException("Query not found."));

            // Act & Assert
             Assert.ThrowsAsync<NotFoundException>(() => _queryService.GetQuery(queryId));
        }
    }
}
