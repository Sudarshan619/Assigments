using AutoMapper;
using Day29_BackendPlan.Context;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Services;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    internal class PolicyHolderServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyHolderRepository repository;
        Mock<ILogger<PolicyHolderRepository>> logger;
        Mock<IMapper> mapper;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyHolderRepository>>();
            mapper = new Mock<IMapper>();
            repository = new PolicyHolderRepository(context, logger.Object);
        }

        [Test]

        public async Task AddPolicyHolderServiceTest()
        {
            var PolicyHolder = new PolicyHolderDTO()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            var PolicyHolderEntity = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            mapper.Setup(m => m.Map<PolicyHolder>(PolicyHolder)).Returns(PolicyHolderEntity);//dummying the method to return the result for testing
            IPolicyHolderService PolicyHolderService = new PolicyHolderService(repository, mapper.Object);
            // Act
            var result = await PolicyHolderService.AddPolicyHolder(PolicyHolder);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task GetPolicyHolderServiceTest(int id, int result)
        {
            var PolicyHolder = new PolicyHolderDTO()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            var PolicyHolderEntity = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            mapper.Setup(m => m.Map<PolicyHolder>(PolicyHolder)).Returns(PolicyHolderEntity);//dummying the method to return the result for testing
            IPolicyHolderService PolicyHolderService = new PolicyHolderService(repository, mapper.Object);

            // Act
            await PolicyHolderService.AddPolicyHolder(PolicyHolder);
            var res = await PolicyHolderService.GetPolicyHolder(id);

            // Assert
            Assert.AreEqual(result, res.Id);
        }
        [Test]

        public async Task GetAllPolicyHolderServiceTest()
        {
            var PolicyHolder = new PolicyHolderDTO()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            var PolicyHolderEntity = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            mapper.Setup(m => m.Map<PolicyHolder>(PolicyHolder)).Returns(PolicyHolderEntity);//dummying the method to return the result for testing
            IPolicyHolderService PolicyHolderService = new PolicyHolderService(repository, mapper.Object);

            // Act
            await PolicyHolderService.AddPolicyHolder(PolicyHolder);
            var res = await PolicyHolderService.GetAllPolicyHolder();

            // Assert
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdatePolicyHolderServiceTest(int id, int result)
        {
            var PolicyHolder = new PolicyHolderDTO()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };
            var PolicyHolderEntity = new PolicyHolder()
            {
                Name = "jeevan",
                Phone = 324345453,
                Email = "s@gmail.com"
            };

            mapper.Setup(m => m.Map<PolicyHolder>(PolicyHolder)).Returns(PolicyHolderEntity);//dummying the method to return the result for testing
            IPolicyHolderService PolicyHolderService = new PolicyHolderService(repository, mapper.Object);

            // Act
            await PolicyHolderService.AddPolicyHolder(PolicyHolder);

            var res = await PolicyHolderService.UpdatePolicyHolder(id, PolicyHolder);

            // Assert
            Assert.AreEqual(result, res);
        }
    }
}
