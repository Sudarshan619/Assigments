using AutoMapper;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.DTO;
using Day29_BackendPlan.Exceptions;

namespace Day29_BackendPlan.Services
{
    public class PolicyService:IPolicyService
    {
        IRepository<int,Policy> _policyRepository;
        IMapper _mapper;
        public PolicyService(IRepository<int,Policy> repository, IMapper mapper)
        {
            _policyRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddPolicy(PolicyDTO Policy)
        {

            Policy newPolicy = _mapper.Map<Policy>(Policy);
            var addedPolicy = await _policyRepository.Add(newPolicy);
            return addedPolicy != null;
        }

        public async Task<int> UpdatePolicy(int id, PolicyDTO Policy)
        {
            try
            {
                var prod = _policyRepository.Get(id);
                if (prod != null)
                {
                    Policy newPolicy = _mapper.Map<Policy>(Policy);
                    var updatedPolicy = await _policyRepository.Update(id, newPolicy);
                    return updatedPolicy.PolicyId;
                }
                else
                {
                    throw new NotFoundException("Not found");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Some error occured");
            }

        }

        public async Task<Policy> GetPolicy(int id)
        {
            try
            {
                var Policy = await _policyRepository.Get(id);
                if (Policy != null)
                {
                    return Policy;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Not found");
            }

        }

        public async Task<IEnumerable<Policy>> GetAllPolicy()
        {
            try
            {
                var Policy = await _policyRepository.GetAll();
                if (Policy != null)
                {
                    return Policy;
                }
                else
                {
                    throw new NotFoundException("Produtc not found");
                }
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Not found");

            }

        }

    }
}
