using Day29_BackendPlan.Context;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestProject
{
    internal class ClaimReportRepositoryTest
    {
        DbContextOptions options;
        InsuranceContext context;
        ClaimReportRepository repository;
        Mock<ILogger<ClaimReportRepository>> logger;
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<InsuranceContext>()
              .UseInMemoryDatabase("TestAdd")
              .Options;
            context = new InsuranceContext(options);
            logger = new Mock<ILogger<ClaimReportRepository>>();
            repository = new ClaimReportRepository(context, logger.Object);
        }

        [Test]
        
        public async Task AddClaimReportTest()
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReport()
            {

                PolicyHolderId = 1,
                Description = "some desc",
                CreatedDate = date,
                SettlementForm ="",
                DeathCertificate = "",
               PolicyCertificate = "",
               Photo = "",
               AddressProof = "",
               CancelledCheck = "",
               Others = "",
            };

            var res = repository.Add(ClaimReport);
            Assert.AreEqual(1, res.Id);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task GetClaimReportTest(int id, int key)
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReport()
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

            await repository.Add(ClaimReport);
            var res = await repository.Get(id);
            Assert.AreEqual(key, res.ReportId);
        }

        [Test]
        public async Task GetAllClaimReportTest()
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReport()
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

            await repository.Add(ClaimReport);
            var res = await repository.GetAll();
            Assert.IsNotNull(res);
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task UpdateClaimReportTest(int id, int key)
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReport()
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

            await repository.Add(ClaimReport);
            DateTime date1 = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport1 = new ClaimReport()
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
            var res = await repository.Update(id, ClaimReport1);
            Assert.AreEqual(key, res.ReportId);
        }

        [TestCase(1, 1)]
        [TestCase(2, 2)]
        public async Task DeleteClaimReportTest(int id, int key)
        {
            DateTime date = DateTime.ParseExact("2-2-2024", "d-M-yyyy", CultureInfo.InvariantCulture);
            var ClaimReport = new ClaimReport()
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

            await repository.Add(ClaimReport);

            var res = await repository.Delete(id);
            Assert.AreEqual(key, res.ReportId);
        }

    }
}
