using Day29_BackendPlan.Context;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Microsoft.EntityFrameworkCore;
using Day29_BackendPlan.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day29_BackendPlan.Repository
{
    public class PolicyRepository : IRepository<int, Policy>
    {
        private readonly InsuranceContext _insuranceContext; 
        private readonly ILogger<PolicyRepository> _logger;
        public PolicyRepository(InsuranceContext insuranceContext,ILogger<PolicyRepository> logger) { 
          _insuranceContext = insuranceContext;
          _logger = logger;
        
        }
        public async Task<Policy> Add(Policy entity)
        {
            try
            {
                _insuranceContext.Policies.Add(entity);
                await _insuranceContext.SaveChangesAsync();
                return entity;

            }
            catch{

                throw new CouldNotAddException("Could not add policy due to invalid data");
            }
        }

        public async Task<Policy> Delete(int id)
        {

            try
            {
                var policy = await Get(id);
                if (policy == null) {
                    throw new NotFoundException("Not found");
                }
                _insuranceContext.Policies.Remove(policy);
                await _insuranceContext.SaveChangesAsync();
                return policy;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Policy> Get(int id)
        {

            try
            {
                var policy =  await _insuranceContext.Policies.FirstOrDefaultAsync(e=> e.PolicyId == id);
                await _insuranceContext.SaveChangesAsync();
                if (policy != null) {
                    return policy;
                }
                throw new NotFoundException("policy not found");

            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException(ex.Message);
            }
        }

        public async Task<IEnumerable<Policy>> GetAll()
        {
            try
            {
                var policies = await _insuranceContext.Policies.ToListAsync();
                await _insuranceContext.SaveChangesAsync();
                if (policies.Count == 0) {
                    throw new CollectionEmptyException("Policies empty");               
                }
                return policies;

            }
            catch(CollectionEmptyException ex)
            {

                throw new CollectionEmptyException($"{ex.Message}");
            }
        }


        public async Task<Policy> Update(int id, Policy entity)
            {
                var policy = await Get(id);
                if (policy == null)
                {
                    throw new NotFoundException("Cart");
                }
                _insuranceContext.Policies.Update(entity);
                await _insuranceContext.SaveChangesAsync();
                return policy;
            }
        
    }
}
