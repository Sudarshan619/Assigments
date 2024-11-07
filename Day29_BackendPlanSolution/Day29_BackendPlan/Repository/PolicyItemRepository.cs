using Day29_BackendPlan.Context;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day29_BackendPlan.Repository
{
    public class PolicyItemRepository : IRepository<int,PolicyItem>
    {
        private readonly InsuranceContext _insuranceContext;
        private readonly ILogger<PolicyItemRepository> _logger;
        public PolicyItemRepository(InsuranceContext insuranceContext, ILogger<PolicyItemRepository> logger)
        {
            _insuranceContext = insuranceContext;
            _logger = logger;

        }
        public async Task<PolicyItem> Add(PolicyItem entity)
        {
            try
            {
                _insuranceContext.PolicyItems.Add(entity);
                await _insuranceContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PolicyItem> Delete(int id)
        {

            try
            {
                var PolicyItem = await Get(id);
                if (PolicyItem == null)
                {
                    throw new NotFoundException("Not found");
                }
                _insuranceContext.PolicyItems.Remove(PolicyItem);
                await _insuranceContext.SaveChangesAsync();
                return PolicyItem;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PolicyItem> Get(int id)
        {

            try
            {
                var PolicyItem = await _insuranceContext.PolicyItems.FirstOrDefaultAsync(e => e.SNo == id);
                await _insuranceContext.SaveChangesAsync();
                if(PolicyItem == null)
                {
                    throw new NotFoundException("Policy item not found");
                }
                return PolicyItem;

            }
            catch (NotFoundException ex)
            {

                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<PolicyItem>> GetAll()
        {
            try
            {
                var policies = await _insuranceContext.PolicyItems.ToListAsync();
                await _insuranceContext.SaveChangesAsync();
                if(policies.Count == 0)
                {
                    throw new CollectionEmptyException("Policy item is empty");
                }
                return policies;

            }
            catch (CollectionEmptyException ex)
            {

                throw new CollectionEmptyException($"{ex.Message}");
            }
        }

        public Task<PolicyItem> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<PolicyItem> Update(int id, PolicyItem entity)
        {
            var PolicyItem = await Get(id);
            if (PolicyItem == null)
            {
                throw new NotFoundException("Cart");
            }
            _insuranceContext.PolicyItems.Update(entity);
            await _insuranceContext.SaveChangesAsync();
            return PolicyItem;
        }

    }
}
