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
    public class PolicyHolderControllerTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyHolderRepository repository;
        Mock<ILogger<PolicyHolderRepository>> logger;
        private Mock<ILogger<PolicyHolderController>> loggerController;
        Mock<IMapper> mapper;
        IPolicyHolderService PolicyHolderService;
        PolicyHolderDTO PolicyHolder;
        PolicyHolder PolicyHolderEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyHolderRepository>>();
            loggerController = new Mock<ILogger<PolicyHolderController>>();
            repository = new PolicyHolderRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            PolicyHolderService = new PolicyHolderService(repository, mapper.Object);
        }

        [Test]
        public async Task AddPolicyHolderTest()
        {
            // Arrange
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
            var controller = new PolicyHolderController(PolicyHolderService, loggerController.Object);
            // Act
            var result = await controller.AddPolicyHolder(PolicyHolder);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        [TestCase(1, 200)]
        public async Task GetPolicyHolderTest(int id, int res)
        {
            // Arrange
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
            var controller = new PolicyHolderController(PolicyHolderService, loggerController.Object);
            // Act
            await controller.AddPolicyHolder(PolicyHolder);
            var result = await controller.GetPolicyHolder(id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }

        [Test]
        [TestCase(200)]
        public async Task GetPolicyHolderAllTest(int res)
        {
            // Arrange
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
            var controller = new PolicyHolderController(PolicyHolderService, loggerController.Object);
            // Act
            await controller.AddPolicyHolder(PolicyHolder);
            var result = await controller.GetAllPolicies();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }
    }
}
