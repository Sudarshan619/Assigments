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
    internal class PolicyItemServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        PolicyItemRepository repository;
        Mock<ILogger<PolicyItemRepository>> logger;
        Mock<IMapper> mapper;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<PolicyItemRepository>>();
            mapper = new Mock<IMapper>();
            repository = new PolicyItemRepository(context, logger.Object);
        }

        [Test]

        public async Task AddPolicyItemServiceTest()
        {
            var PolicyItem = new PolicyItemDTO()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            var PolicyItemEntity = new PolicyItem
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            mapper.Setup(m => m.Map<PolicyItem>(PolicyItem)).Returns(PolicyItemEntity);//dummying the method to return the result for testing
            IPolicyItemService PolicyItemService = new PolicyItemService(repository, mapper.Object);
            // Act
            var result = await PolicyItemService.AddPolicyItem(PolicyItem);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task GetPolicyItemServiceTest(int id, int result)
        {
            var PolicyItem = new PolicyItemDTO()
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            var PolicyItemEntity = new PolicyItem
            {
                PolicyId = 1,
                PolicyHolderId = 1
            };
            mapper.Setup(m => m.Map<PolicyItem>(PolicyItem)).Returns(PolicyItemEntity);//dummying the method to return the result for testing
            IPolicyItemService PolicyItemService = new PolicyItemService(repository, mapper.Object);

            // Act
            await PolicyItemService.AddPolicyItem(PolicyItem);
            var res = await PolicyItemService.GetPolicyItem(id);

            // Assert
            Assert.AreEqual(result, res.SNo);
        }
        [Test]

        public async Task GetAllPolicyItemServiceTest()
        {
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
            IPolicyItemService PolicyItemService = new PolicyItemService(repository, mapper.Object);

            // Act
            await PolicyItemService.AddPolicyItem(PolicyItem);
            var res = await PolicyItemService.GetAllPolicyItem();

            // Assert
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdatePolicyItemServiceTest(int id, int result)
        {
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
            IPolicyItemService PolicyItemService = new PolicyItemService(repository, mapper.Object);

            // Act
            await PolicyItemService.AddPolicyItem(PolicyItem);

            var res = await PolicyItemService.UpdatePolicyItem(id, PolicyItem);

            // Assert
            Assert.AreEqual(result, res);
        }
    }
}
