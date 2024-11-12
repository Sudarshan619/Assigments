using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Exceptions;
using Moq;

namespace TestProject1
{
    public class QueryRepositoryTest
    {
        private readonly QuizContext _context;
        private readonly QueryRepository _repository;

        public QueryRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<QuizContext>()
                 .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: "QuizTestDb")
                .Options;

            _context = new QuizContext(options);
            var logger = new Mock<ILogger<QueryRepository>>();
            _repository = new QueryRepository(_context, logger.Object);
        }

        [Test]
        public async Task Add_ShouldAddQuery()
        {
            // Arrange
            var query = new Query
            {
                QueryId = 1,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            // Act
            var result = await _repository.Add(query);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Test Description", result.Description);
        }

        [Test]
        public async Task Delete_ShouldDeleteQuery()
        {
            // Arrange
            var query = new Query
            {
                QueryId = 2,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Delete(2);

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task Delete_ShouldDeleteQuery_ReturnNotFound()
        {

            int id = 1;
            Assert.ThrowsAsync<Exception>(async () => await _repository.Delete(id));
        }

        [Test]
        public async Task Get_ShouldReturnQuery()
        {
            // Arrange
            var query = new Query
            {
                QueryId = 3,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.Get(query.QueryId);

            // Assert
            Assert.NotNull(result);

        }

        [Test]
        public async Task Get_ShouldGetQuery_ReturnNotFound()
        {

            int id = 1;
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(id));
        }

        [Test]
        public async Task GetAll_ShouldReturnAllQueries()
        {
            // Arrange
            var queries = new List<Query>
            {
                new Query { QueryId = 4, ReportedBy = 123, QueryType = "Technical", Description = "Description 1" },
                new Query { QueryId = 5, ReportedBy = 456, QueryType = "Functional", Description = "Description 2" }
            };

            _context.Queries.AddRange(queries);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAll();

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public async Task GetAll_ShouldGetallQuery_ReturnNotFound()
        {

            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.GetAll());
        }

        [Test]  

        public async Task Update_ShouldUpdateQuery_ReturnQuery()
        {
            var query = new Query
            {
                QueryId = 3,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            var newQuery = new Query
            {
                QueryId = 3,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "New Description",
                IsResolved = false
            };


            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            var result = await _repository.Update(query.QueryId, newQuery);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.QueryId,query.QueryId);
        }

        [Test]

        public async Task Update_ReturnException()
        {
            var newQuery = new Query
            {
                QueryId = 3,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "New Description",
                IsResolved = false
            };
            Assert.ThrowsAsync<Exception>(async () => await _repository.Update(99,newQuery));
        }
    }
}
