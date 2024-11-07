using Day29_BackendPlan.Context;
using Day29_BackendPlan.Exceptions;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class PolicyItemRepositoryTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyItemRepository repository;
        Mock<ILogger<PolicyItemRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyItemRepository>>();
            repository = new PolicyItemRepository(context, logger.Object);
        }

        [Test]
        [TestCase(1,1,1)]
        [TestCase(1,2,2)]
        public async Task AddPolicyItemTest(int policyId,int policyHolderId, int key)
        {
            var PolicyItem = new PolicyItem()
            {
                PolicyId = policyId,
                PolicyHolderId = policyHolderId
            };

            var res = repository.Add(PolicyItem);
            Assert.AreEqual(key, res.Id);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,2)]
        public async Task GetPolicyItemTest(int id, int key)
        {
            var PolicyItem = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };

            await repository.Add(PolicyItem);
            var res = await repository.Get(id);
            Assert.AreEqual(key, res.SNo);
        }

        [Test]
        [TestCase(1)]
        public async Task ExceptionGetPolicyItemTest(int id)
        {

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(id));
        }

        [Test]
        public async Task GetAllPolicyItemTest()
        {
            var PolicyItem = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };

            await repository.Add(PolicyItem);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        public async Task ExceptionGetAllPolicyItemTest()
        {

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdatePolicyItemTest(int id, int key)
        {
            var PolicyItem = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };

            await repository.Add(PolicyItem);

            var PolicyItem1 = new PolicyItem()
            {
                PolicyId = 2,
                PolicyHolderId = 1
            };
            var res = await repository.Update(id, PolicyItem1);
            Assert.AreEqual(key, res.SNo);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task DeletePolicyItemTest(int id, int key)
        {
            var PolicyItem = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };

            await repository.Add(PolicyItem);
            
            var res = await repository.Delete(id);
            Assert.AreEqual(key, res.SNo);
        }

    }
}
