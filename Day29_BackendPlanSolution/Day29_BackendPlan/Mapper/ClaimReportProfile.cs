using AutoMapper;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;
using System.Security.Claims;

namespace Day29_BackendPlan.Mapper
{
    public class ClaimReportProfile:Profile
    {
        public ClaimReportProfile()
        {
            CreateMap<ClaimReport, ClaimReportDTO>();
            CreateMap<ClaimReportDTO, ClaimReport>();
        }
    }
}
