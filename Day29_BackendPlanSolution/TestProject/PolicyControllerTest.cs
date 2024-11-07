using AutoMapper;
using Day29_BackendPlan.Context;
using Day29_BackendPlan.Controllers;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTest
{
    public class PolicyControllerTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyRepository repository;
        Mock<ILogger<PolicyRepository>> logger;
        private Mock<ILogger<PolicyController>> loggerController;
        Mock<IMapper> mapper;
        IPolicyService PolicyService;
        //PolicyDTO Policy;
        //Policy PolicyEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyRepository>>();
            loggerController = new Mock<ILogger<PolicyController>>();
            repository = new PolicyRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            PolicyService = new PolicyService(repository, mapper.Object);
        }

        [Test]
        public async Task AddPolicyTest()
        {
            // Arrange
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
            var controller = new PolicyController(PolicyService, loggerController.Object);
            // Act
            var result = await controller.AddPolicy(policy);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        [TestCase(1, 200)]
        public async Task GetPolicyTest(int id, int res)
        {
            // Arrange
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
            var controller = new PolicyController(PolicyService, loggerController.Object);
            // Act
            await controller.AddPolicy(policy);
            var result = await controller.GetPolicy(id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }

        [Test]
        [TestCase(200)]
        public async Task GetPolicyAllTest(int res)
        {
            // Arrange
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
            var controller = new PolicyController(PolicyService, loggerController.Object);
            // Act
            await controller.AddPolicy(policy);
            var result = await controller.GetAllPolicies();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }
    }
}
