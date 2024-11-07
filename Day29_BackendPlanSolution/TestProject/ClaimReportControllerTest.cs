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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstTest
{
    public class ClaimReportControllerTest
    {
        DbContextOptions options;
        InsuranceContext context;
        ClaimReportRepository repository;
        Mock<ILogger<ClaimReportRepository>> logger;
        private Mock<ILogger<ClaimReportController>> loggerController;
        Mock<IMapper> mapper;
        IClaimReportService ClaimReportService;
        ClaimReportDTO ClaimReport;
        ClaimReport ClaimReportEntity;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<ClaimReportRepository>>();
            loggerController = new Mock<ILogger<ClaimReportController>>();
            repository = new ClaimReportRepository(context, logger.Object);
            mapper = new Mock<IMapper>();
            ClaimReportService = new ClaimReportService(repository, mapper.Object);
        }

        [Test]
        public async Task AddClaimReportTest()
        {
            // Arrange
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReportDTO()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };
            DateTime date1 = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReportEntity = new ClaimReport()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date1,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };

            mapper.Setup(m => m.Map<ClaimReport>(ClaimReport)).Returns(ClaimReportEntity);//dummying the method to return the result for testing
            var controller = new ClaimReportController(ClaimReportService, loggerController.Object);
            // Act
            var result = await controller.AddClaimReport(ClaimReport);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            Assert.IsNotNull(resultObject);
            Assert.AreEqual(200, resultObject.StatusCode);
        }


        [Test]
        [TestCase(1, 200)]
        public async Task GetClaimReportTest(int id, int res)
        {
            // Arrange
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReportDTO()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };
            DateTime date1 = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReportEntity = new ClaimReport()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date1,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };
            mapper.Setup(m => m.Map<ClaimReport>(ClaimReport)).Returns(ClaimReportEntity);//dummying the method to return the result for testing
            var controller = new ClaimReportController(ClaimReportService, loggerController.Object);
            // Act
            await controller.AddClaimReport(ClaimReport);
            var result = await controller.GetClaimReport(id);
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }

        [Test]
        [TestCase(200)]
        public async Task GetClaimReportAllTest(int res)
        {
            // Arrange
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReportDTO()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };
            DateTime date1 = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReportEntity = new ClaimReport()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date1,
                SettlementForm = "",
                DeathCertificate = "",
                PolicyCertificate = "",
                Photo = "",
                AddressProof = "",
                CancelledCheck = "",
                Others = "",
            };
            mapper.Setup(m => m.Map<ClaimReport>(ClaimReport)).Returns(ClaimReportEntity);//dummying the method to return the result for testing
            var controller = new ClaimReportController(ClaimReportService, loggerController.Object);
            // Act
            await controller.AddClaimReport(ClaimReport);
            var result = await controller.GetAllClaimReports();
            Assert.IsNotNull(result);
            var resultObject = result as OkObjectResult;
            // Assert
            //Assert.IsNotNull(resultObject);
            Assert.AreEqual(res, resultObject.StatusCode);
        }
    }
}
