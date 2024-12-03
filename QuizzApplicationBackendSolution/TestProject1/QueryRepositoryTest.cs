using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using QuizzApplicationBackend.Context;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Repositories;
using QuizzApplicationBackend.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;

namespace TestProject1
{
    [TestFixture]
    public class QueryRepositoryTest
    {
        private QuizContext _context;
        private QueryRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<QuizContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new QuizContext(options);
            var logger = new Mock<ILogger<QueryRepository>>();
            _repository = new QueryRepository(_context, logger.Object);
        }

        [TearDown]
        public void Cleanup()
        {
            _context.Dispose();
        }

        // Add Method Tests
        [Test]
        public async Task Add_ShouldAddQuery()
        {
            var query = new Query
            {
                QueryId = 1,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            var result = await _repository.Add(query);

            Assert.NotNull(result);
            Assert.AreEqual(query.Description, result.Description);
        }

        [Test]
        public void Add_ShouldThrowException_OnFailure()
        {
            _context.Dispose(); // Simulate DbContext failure
            var query = new Query
            {
                QueryId = 1,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            Assert.ThrowsAsync<Exception>(async () => await _repository.Add(query), "Not able to add Query");
        }

        // Delete Method Tests
        [Test]
        public async Task Delete_ShouldDeleteQuery()
        {
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

            var result = await _repository.Delete(query.QueryId);

            Assert.NotNull(result);
            Assert.AreEqual(query.QueryId, result.QueryId);
        }

        [Test]
        public void Delete_ShouldThrowException_WhenQueryNotFound()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(99), "Query not found");
        }

        // Get Method Tests
        [Test]
        public async Task Get_ShouldReturnQuery()
        {
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

            var result = await _repository.Get(query.QueryId);

            Assert.NotNull(result);
            Assert.AreEqual(query.QueryId, result.QueryId);
        }

        [Test]
        public void Get_ShouldThrowNotFoundException_WhenQueryNotFound()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(99), "Query not found");
        }

        // GetAll Method Tests
        [Test]
        public async Task GetAll_ShouldReturnAllQueries()
        {
            var queries = new List<Query>
            {
                new Query { QueryId = 4, ReportedBy = 123, QueryType = "Technical", Description = "Description 1" },
                new Query { QueryId = 5, ReportedBy = 456, QueryType = "Functional", Description = "Description 2" }
            };

            _context.Queries.AddRange(queries);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.NotNull(result);
            Assert.AreEqual(queries.Count, result.Count());
        }

        [Test]
        public void GetAll_ShouldThrowNotFoundException_WhenNoQueriesExist()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.GetAll(), "Collection empty");
        }

        // Update Method Tests
        [Test]
        public async Task Update_ShouldUpdateQuery()
        {
            var query = new Query
            {
                QueryId = 6,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Test Description",
                IsResolved = false
            };

            _context.Queries.Add(query);
            await _context.SaveChangesAsync();

            var updatedQuery = new Query
            {
                QueryId = 6,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Updated Description",
                IsResolved = true
            };

            var result = await _repository.Update(query.QueryId, updatedQuery);

            Assert.NotNull(result);
            Assert.AreEqual(updatedQuery.Description, result.Description);
        }

        [Test]
        public void Update_ShouldThrowException_WhenQueryNotFound()
        {
            var updatedQuery = new Query
            {
                QueryId = 99,
                ReportedBy = 123,
                QueryType = "Technical",
                Description = "Updated Description",
                IsResolved = true
            };

            Assert.ThrowsAsync<Exception>(async () => await _repository.Update(99, updatedQuery), "Query does not exist");
        }
    }
}
