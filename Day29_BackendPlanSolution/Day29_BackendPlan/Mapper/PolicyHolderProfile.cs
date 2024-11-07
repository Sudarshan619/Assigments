using AutoMapper;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Mapper
{
    public class PolicyHolderProfile:Profile
    {
        public PolicyHolderProfile() {
            CreateMap<PolicyHolder, PolicyHolderDTO>();
            CreateMap<PolicyHolderDTO, PolicyHolder>();

        }
    }
}
