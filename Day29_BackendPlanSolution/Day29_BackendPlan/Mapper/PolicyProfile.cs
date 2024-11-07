using AutoMapper;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Mapper
{
    public class PolicyProfile:Profile
    {
        public PolicyProfile()
        {
            CreateMap<Policy, PolicyDTO>();
            CreateMap<PolicyDTO, Policy>();

        }
    }
}
