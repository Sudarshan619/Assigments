using Day29_BackendPlan.Context;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Microsoft.EntityFrameworkCore;
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
    internal class PolicyHolderRepositoryTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyHolderRepository repository;
        Mock<ILogger<PolicyHolderRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyHolderRepository>>();
            repository = new PolicyHolderRepository(context, logger.Object);
        }

        [Test]
        [TestCase("jeevan", 324345453, "s@gmail", 1)]
        [TestCase("predential", 23274903, "g@gmail", 2)]
        public async Task AddPolicyHolderTest(string name, long type, string value, int key)
        {
            var PolicyHolder = new PolicyHolder()
            {
                Name = name,
                Phone= type,
                Email = value
            };

            var res = repository.Add(PolicyHolder);
            Assert.AreEqual(key, res.Id);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task GetPolicyHolderTest(int id, int key)
        {
            var PolicyHolder = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            await repository.Add(PolicyHolder);
            var res = await repository.Get(id);
            Assert.AreEqual(key, res.Id);
        }

        [Test]
        public async Task GetAllPolicyHolderTest()
        {
            var PolicyHolder = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            await repository.Add(PolicyHolder);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdatePolicyHolderTest(int id, int key)
        {
            var PolicyHolder = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            await repository.Add(PolicyHolder);
            var PolicyHolder1 = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            var res = await repository.Update(id, PolicyHolder1);
            Assert.AreEqual(key, res.Id);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task DeletePolicyHolderTest(int id, int key)
        {
            var PolicyHolder = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            await repository.Add(PolicyHolder);
           
            var res = await repository.Delete(id);
            Assert.AreEqual(key, res.Id);
        }

    }
}
