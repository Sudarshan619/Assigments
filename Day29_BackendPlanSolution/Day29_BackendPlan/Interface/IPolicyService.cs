using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Model;

namespace Day29_BackendPlan.Interface
{
    public interface IPolicyService
    {
        public Task<bool> AddPolicy(PolicyDTO policy);

        public Task<int> UpdatePolicy(int id, PolicyDTO policy);

        public Task<Policy> GetPolicy(int id);

        public Task<IEnumerable<Policy>> GetAllPolicy();
    }
}
