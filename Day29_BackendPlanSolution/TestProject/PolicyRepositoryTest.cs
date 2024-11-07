using Day29_BackendPlan.Context;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestProject
{
    internal class PolicyRepositoryTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository repository;
        Mock<ILogger<PolicyRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyRepository>>();
            repository = new PolicyRepository(context, logger.Object);
        }

        [Test]
        [TestCase("jeevan","health",1000,1)]
        [TestCase("predential", "life", 2000, 2)]
        public async Task AddPolicyTest(string name,string type,double value, int key)
        {
            var policy = new Policy()
            {
                PolicyName = name,
                PolicyType = type,
                PolicyValue = value
            };

            var res = repository.Add(policy);
            Assert.AreEqual(key,res.Id);
        }

        [Test]
        public async Task AddException()
        {
            var policy = new Policy()
            {
                PolicyName = null,
                PolicyType = "life",
                PolicyValue = 1000
            };

          
            Assert.ThrowsAsync<CouldNotAddException>(async ()=> await  repository.Add(policy));
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2, 2)]
        public async Task GetPolicyTest(int id, int key)
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue =1000
            };

            await repository.Add(policy);
            var res  = await repository.Get(id);
            Assert.AreEqual(key, res.PolicyId);
        }

        [Test]
        [TestCase(1)]
        public async Task ExceptionGetPolicyTest(int id)
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };

            Assert.ThrowsAsync<NotFoundException>(async ()=> await repository.Get(id));
        }

        [Test]
        public async Task GetAllPolicyTest()
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };

            await repository.Add(policy);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        public async Task ExceptionGetAllPolicyTest()
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdatePolicyTest(int id,int key)
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };

            await repository.Add(policy);
            var policy1 = new Policy()
            {
                PolicyName = "jeevan_bheema",
                PolicyType = "life",
                PolicyValue = 2000
            };
            var res = await repository.Update(id,policy1);
            Assert.AreEqual(key,res.PolicyId);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task DeletePolicyTest(int id, int key)
        {
            var policy = new Policy()
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };

            await repository.Add(policy);
           
            var res = await repository.Delete(id);
            Assert.AreEqual(key, res.PolicyId);
        }

    }
}
