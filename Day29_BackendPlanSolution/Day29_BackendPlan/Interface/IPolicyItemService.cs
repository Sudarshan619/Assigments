using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Interface
{
    public interface IPolicyItemService
    {
        public Task<bool> AddPolicyItem(PolicyItemDTO policy);

        public Task<int> UpdatePolicyItem(int id, PolicyItemDTO policy);

        public Task<PolicyItem> GetPolicyItem(int id);

        public Task<IEnumerable<PolicyItem>> GetAllPolicyItem();
    }
}
