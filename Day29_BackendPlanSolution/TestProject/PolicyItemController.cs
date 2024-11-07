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
    public class PolicyItemControllerTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyItemRepository repository;
        Mock<ILogger<PolicyItemRepository>> logger;
        private Mock<ILogger<PolicyItemController>> loggerController;
        Mock<IMapper> mapper;
        IPolicyItemService PolicyItemService;
        PolicyItemDTO PolicyItem;
        PolicyItem PolicyItemEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyItemRepository>>();
            loggerController = new Mock<ILogger<PolicyItemController>>();
            repository = new PolicyItemRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            PolicyItemService = new PolicyItemService(repository, mapper.Object);
        }

        [Test]
        public async Task AddPolicyItemTest()
        {
            // Arrange
            var PolicyItem = new PolicyItemDTO()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            var PolicyItemEntity = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            mapper.Setup(m => m.Map<PolicyItem>(PolicyItem)).Returns(PolicyItemEntity);//dummying the method to return the result for testing
            var controller = new PolicyItemController(PolicyItemService, loggerController.Object);
            // Act
            var result = await controller.AddPolicyItem(PolicyItem);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        [TestCase(1, 200)]
        public async Task GetPolicyItemTest(int id, int res)
        {
            // Arrange
            var PolicyItem = new PolicyItemDTO()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            var PolicyItemEntity = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            mapper.Setup(m => m.Map<PolicyItem>(PolicyItem)).Returns(PolicyItemEntity);//dummying the method to return the result for testing
            var controller = new PolicyItemController(PolicyItemService, loggerController.Object);
            // Act
            await controller.AddPolicyItem(PolicyItem);
            var result = await controller.GetPolicyItem(id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }

        [Test]
        [TestCase(200)]
        public async Task GetPolicyItemAllTest(int res)
        {
            // Arrange
            var PolicyItem = new PolicyItemDTO()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            var PolicyItemEntity = new PolicyItem()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            mapper.Setup(m => m.Map<PolicyItem>(PolicyItem)).Returns(PolicyItemEntity);//dummying the method to return the result for testing
            var controller = new PolicyItemController(PolicyItemService, loggerController.Object);
            // Act
            await controller.AddPolicyItem(PolicyItem);
            var result = await controller.GetAllPolicies();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }
    }
}
