using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Interface
{
    public interface IPolicyHolderService
    {
        public Task<bool> AddPolicyHolder(PolicyHolderDTO policy);

        public Task<int> UpdatePolicyHolder(int id, PolicyHolderDTO policy);

        public Task<PolicyHolder> GetPolicyHolder(int id);

        public Task<IEnumerable<PolicyHolder>> GetAllPolicyHolder();
    }
}
