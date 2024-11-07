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
using System.Globalization;

namespace TestProject
{
    internal class ClaimReportServiceTest
    {
        DbContextOptions options;
        InsuranceContext context;
        ClaimReportRepository repository;
        PolicyHolderRepository prepository;
        Mock<ILogger<ClaimReportRepository>> logger;
        Mock<IMapper> mapper;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<ClaimReportRepository>>();
            mapper = new Mock<IMapper>();
            repository = new ClaimReportRepository(context, logger.Object);

        }

        [Test]

        public async Task AddClaimReportServiceTest()
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReportDTO()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date,
                SettlementForm = "sdsdd",
                DeathCertificate = "sdss",
                PolicyCertificate = "ddssd",
                Photo = "dsds",
                AddressProof = "sdss",
                CancelledCheck = "sdsd",
                Others = "dsds",
            };
            DateTime date1 = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReportEntity = new ClaimReport()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date1,
                SettlementForm = "sgd",
                DeathCertificate = "sdd",
                PolicyCertificate = "dssdd",
                Photo = "sdz",
                AddressProof = "zsd",
                CancelledCheck = "zsd",
                Others = "zsd",
            };

            mapper.Setup(m => m.Map<ClaimReport>(ClaimReport)).Returns(ClaimReportEntity);//dummying the method to return the result for testing
            IClaimReportService ClaimReportService = new ClaimReportService(repository, mapper.Object);
            // Act

            var result = await ClaimReportService.AddClaimReport(ClaimReport);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task GetClaimReportServiceTest(int id, int result)
        {
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
            IClaimReportService ClaimReportService = new ClaimReportService(repository, mapper.Object);

            // Act
            await ClaimReportService.AddClaimReport(ClaimReport);
            var res = await ClaimReportService.GetReport(id);

            // Assert
            Assert.AreEqual(result, res.ReportId);
        }
        [Test]

        public async Task GetAllClaimReportServiceTest()
        {
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
            IClaimReportService ClaimReportService = new ClaimReportService(repository, mapper.Object);

            // Act
            await ClaimReportService.AddClaimReport(ClaimReport);
            var res = await ClaimReportService.GetAllReport();

            // Assert
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdateClaimReportServiceTest(int id, int result)
        {
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
            IClaimReportService ClaimReportService = new ClaimReportService(repository, mapper.Object);

            // Act
            await ClaimReportService.AddClaimReport(ClaimReport);

            var res = await ClaimReportService.UpdateClaimReport(id, ClaimReport);

            // Assert
            Assert.AreEqual(result, res);
        }
    }
}
