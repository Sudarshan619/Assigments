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
    internal class PolicyServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository repository;
        Mock<ILogger<PolicyRepository>> logger;
        Mock<IMapper> mapper;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyRepository>>();
            mapper = new Mock<IMapper>();
            repository = new PolicyRepository(context, logger.Object);
        }

        [Test]

        public async Task AddPolicyServiceTest()
        {
            var policy = new PolicyDTO
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            var policyEntity = new Policy
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            mapper.Setup(m => m.Map<Policy>(policy)).Returns(policyEntity);//dummying the method to return the result for testing
            IPolicyService policyService = new PolicyService(repository, mapper.Object);
            // Act
            var result = await policyService.AddPolicy(policy);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,2)]
        public async Task GetPolicyServiceTest(int id, int result)
        {
            var policy = new PolicyDTO
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            var policyEntity = new Policy
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            mapper.Setup(m => m.Map<Policy>(policy)).Returns(policyEntity);//dummying the method to return the result for testing
            IPolicyService policyService = new PolicyService(repository, mapper.Object);

            // Act
            await policyService.AddPolicy(policy);
            var res =await  policyService.GetPolicy(id);

            // Assert
            Assert.AreEqual(result,res.PolicyId);
        }
        [Test]

        public async Task GetAllPolicyServiceTest()
        {
            var policy = new PolicyDTO
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            var policyEntity = new Policy
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            mapper.Setup(m => m.Map<Policy>(policy)).Returns(policyEntity);//dummying the method to return the result for testing
            IPolicyService policyService = new PolicyService(repository, mapper.Object);

            // Act
            await policyService.AddPolicy(policy);
            var res = await policyService.GetAllPolicy();

            // Assert
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1,1)]
        [TestCase(2,2)]
        public async Task UpdatePolicyServiceTest(int id,int result)
        {
            var policy = new PolicyDTO
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            var policyEntity = new Policy
            {
                PolicyName = "jeevan",
                PolicyType = "health",
                PolicyValue = 1000
            };
            mapper.Setup(m => m.Map<Policy>(policy)).Returns(policyEntity);//dummying the method to return the result for testing
            IPolicyService policyService = new PolicyService(repository, mapper.Object);

            // Act
            await policyService.AddPolicy(policy);
            
            var res = await policyService.UpdatePolicy(id,policy);

            // Assert
            Assert.AreEqual(result,res);
        }
    }
}
