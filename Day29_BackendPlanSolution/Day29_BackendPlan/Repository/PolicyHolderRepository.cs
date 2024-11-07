using Day29_BackendPlan.Context;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Day29_BackendPlan.Repository
{
    public class PolicyHolderRepository : IRepository<int,PolicyHolder>
    {
        private readonly InsuranceContext _insuranceContext;
        private readonly ILogger<PolicyHolderRepository> _logger;
        public PolicyHolderRepository(InsuranceContext insuranceContext, ILogger<PolicyHolderRepository> logger)
        {
            _insuranceContext = insuranceContext;
            _logger = logger;

        }
        public async Task<PolicyHolder> Add(PolicyHolder entity)
        {
            try
            {
                _insuranceContext.PolicyHolders.Add(entity);
                await _insuranceContext.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PolicyHolder> Delete(int id)
        {

            try
            {
                var PolicyHolder = await Get(id);
                if (PolicyHolder == null)
                {
                    throw new NotFoundException("Not found");
                }
                _insuranceContext.PolicyHolders.Remove(PolicyHolder);
                await _insuranceContext.SaveChangesAsync();
                return PolicyHolder;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PolicyHolder> Get(int id)
        {

            try
            {
                var PolicyHolder = await _insuranceContext.PolicyHolders.FirstOrDefaultAsync(e => e.Id == id);
                await _insuranceContext.SaveChangesAsync();
                return PolicyHolder;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PolicyHolder> GetByName(string name)
        {

            try
            {
                var PolicyHolder = await _insuranceContext.PolicyHolders.FirstOrDefaultAsync(e => e.Name == name);
                await _insuranceContext.SaveChangesAsync();
                return PolicyHolder;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<PolicyHolder>> GetAll()
        {
            try
            {
                var policies = await _insuranceContext.PolicyHolders.ToListAsync();
                await _insuranceContext.SaveChangesAsync();
                return policies;

            }
            catch (Exception ex)
            {

                throw new Exception($"{ex.Message}");
            }
        }

        public async Task<PolicyHolder> Update(int id, PolicyHolder entity)
        {
            var PolicyHolder = await Get(id);
            if (PolicyHolder == null)
            {
                throw new NotFoundException("Cart");
            }
            _insuranceContext.PolicyHolders.Update(entity);
            await _insuranceContext.SaveChangesAsync();
            return PolicyHolder;
        }

    }
}
