using Day29_BackendPlan.Context;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day29_BackendPlan.Repository
{
    public class ClaimReportRepository : IRepository<int, ClaimReport>
    {
        private readonly InsuranceContext _insuranceContext;
        private readonly ILogger<ClaimReportRepository> _logger;
        public ClaimReportRepository(InsuranceContext insuranceContext, ILogger<ClaimReportRepository> logger)
        {
            _insuranceContext = insuranceContext;
            _logger = logger;

        }
        public async Task<ClaimReport> Add(ClaimReport entity)
        {
            try
            {
                _insuranceContext.ClaimReports.Add(entity);
                await _insuranceContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ClaimReport> Delete(int id)
        {

            try
            {
                var ClaimReport = await Get(id);
                if (ClaimReport == null)
                {
                    throw new NotFoundException("Not found");
                }
                _insuranceContext.ClaimReports.Remove(ClaimReport);
                await _insuranceContext.SaveChangesAsync();
                return ClaimReport;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ClaimReport> Get(int id)
        {

            try
            {
                var ClaimReport = await _insuranceContext.ClaimReports.FirstOrDefaultAsync(e => e.ReportId == id);
                await _insuranceContext.SaveChangesAsync();
                return ClaimReport;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<ClaimReport>> GetAll()
        {
            try
            {
                var policies = await _insuranceContext.ClaimReports.ToListAsync();
                await _insuranceContext.SaveChangesAsync();
                return policies;

            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public Task<ClaimReport> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<ClaimReport> Update(int id, ClaimReport entity)
        {
            var ClaimReport = await Get(id);
            if (ClaimReport == null)
            {
                throw new NotFoundException("Cart");
            }
            _insuranceContext.ClaimReports.Update(entity);
            await _insuranceContext.SaveChangesAsync();
            return ClaimReport;
        }

    }
}
