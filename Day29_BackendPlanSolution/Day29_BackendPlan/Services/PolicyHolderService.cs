using AutoMapper;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Exceptions;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.DTO;

namespace Day29_BackendPlan.Services
{
    public class PolicyHolderService:IPolicyHolderService
    {
        IRepository<int,PolicyHolder> _policyHolderRepository;
        IMapper _mapper;
        public PolicyHolderService(IRepository<int,PolicyHolder> repository, IMapper mapper)
        {
            _policyHolderRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddPolicyHolder(PolicyHolderDTO PolicyHolder)
        {
            PolicyHolder newPolicyHolder = _mapper.Map<PolicyHolder>(PolicyHolder);
            var addedPolicyHolder = await _policyHolderRepository.Add(newPolicyHolder);
            return addedPolicyHolder != null;
        }

        public async Task<int> UpdatePolicyHolder(int id, PolicyHolderDTO PolicyHolder)
        {
            try
            {
                var prod = _policyHolderRepository.Get(id);
                if (prod != null)
                {
                    PolicyHolder newPolicyHolder = _mapper.Map<PolicyHolder>(PolicyHolder);
                    var updatedPolicyHolder = await _policyHolderRepository.Update(id, newPolicyHolder);
                    return updatedPolicyHolder.Id;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Some error occured");
            }

        }

        public async Task<PolicyHolder> GetPolicyHolder(int id)
        {
            try
            {
                var PolicyHolder = await _policyHolderRepository.Get(id);
                if (PolicyHolder != null)
                {
                    return PolicyHolder;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");
            }

        }

        public async Task<IEnumerable<PolicyHolder>> GetAllPolicyHolder()
        {
            try
            {
                var PolicyHolder = await _policyHolderRepository.GetAll();
                if (PolicyHolder != null)
                {
                    return PolicyHolder;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }

    }
}
