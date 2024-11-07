using AutoMapper;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Mapper
{
    public class PolicyItemProfile:Profile
    {
        public PolicyItemProfile()
        {
            CreateMap<PolicyItem, PolicyItemDTO>();
            CreateMap<PolicyItemDTO, PolicyItem>();

        }
    }
}
