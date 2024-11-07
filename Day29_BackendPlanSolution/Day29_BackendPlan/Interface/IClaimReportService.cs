using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Interface
{
    public interface IClaimReportService
    {
        public Task<bool> AddClaimReport(ClaimReportDTO ClaimReport);

        public Task<int> UpdateClaimReport(int id,ClaimReportDTO ClaimReport);

        public Task<ClaimReport> GetReport(int id);

        public Task<IEnumerable<ClaimReport>> GetAllReport();


    }
}
