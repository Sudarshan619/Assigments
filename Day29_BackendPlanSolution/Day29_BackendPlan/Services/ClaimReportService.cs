using AutoMapper;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Exceptions;

namespace Day29_BackendPlan.Services
{
    public class ClaimReportService:IClaimReportService
    {
        IRepository<int,ClaimReport> _ClaimReportRepository;
        IRepository<int,PolicyHolder> _PolicyHolderRepository;
        IMapper _mapper;
        private ClaimReportRepository repository;
        private IMapper @object;

        public ClaimReportService(IRepository<int, ClaimReport> repository,IRepository<int,PolicyHolder> policyHolderRepo, IMapper mapper)
        {
            _ClaimReportRepository = repository;
            _PolicyHolderRepository = policyHolderRepo;
            _mapper = mapper;

        }

        public ClaimReportService(ClaimReportRepository repository, IMapper @object)
        {
            this.repository = repository;
            this.@object = @object;
        }

        public async Task<bool> AddClaimReport(ClaimReportDTO ClaimReport)
        {
            try
            {
                ClaimReport newClaimReport = _mapper.Map<ClaimReport>(ClaimReport);
                var holder = await _PolicyHolderRepository.Get(ClaimReport.PolicyHolderId);
                if (holder == null)
                {
                    throw new AlreadyExistException("empty policy holder");
                }
                
                var reports = await _ClaimReportRepository.GetAll();
                var report = reports.FirstOrDefault(e => e.PolicyHolderId == holder.Id);
                if (report != null)
                {
                    throw new AlreadyExistException("Claim with the id already exist");
                }
                var addedClaimReport = await _ClaimReportRepository.Add(newClaimReport);
                return addedClaimReport != null;
            }
            catch (AlreadyExistException ex) {
                throw new AlreadyExistException(ex.Message);
            }
           
        }

        public async Task<int> UpdateClaimReport(int id, ClaimReportDTO ClaimReport)
        {
            try
            {
                var prod = await _ClaimReportRepository.Get(id);
                if (prod != null)
                {
                    ClaimReport newClaimReport = _mapper.Map<ClaimReport>(ClaimReport);
                    var updatedClaimReport = await _ClaimReportRepository.Update(id, newClaimReport);
                    return updatedClaimReport.ReportId;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Some error occured");
            }

        }

        public async Task<ClaimReport> GetReport(int id)
        {
            try
            {
                var ClaimReport = await _ClaimReportRepository.Get(id);
                if (ClaimReport != null)
                {
                    return ClaimReport;
                }
                else
                {
                    throw new NotFoundException("Report not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");
            }

        }

        public async Task<IEnumerable<ClaimReport>> GetAllReport()
        {
            try
            {
                var ClaimReport = await _ClaimReportRepository.GetAll();
                if (ClaimReport != null)
                {
                    return ClaimReport;
                }
                else
                {
                    throw new NotFoundException("Report not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }

    }
}
