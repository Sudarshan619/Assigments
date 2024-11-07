using AutoMapper;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.Repository;
using Day29_BackendPlan.Exceptions;
using Day29_BackendPlan.Model;
using Day29_BackendPlan.DTO;

namespace Day29_BackendPlan.Services
{
    public class PolicyItemService: IPolicyItemService
    {
        IRepository<int,PolicyItem> _PolicyItemRepository;
        IMapper _mapper;
        public PolicyItemService(IRepository<int, PolicyItem> repository, IMapper mapper)
        {
            _PolicyItemRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddPolicyItem(PolicyItemDTO PolicyItem)
        {
            PolicyItem newPolicyItem = _mapper.Map<PolicyItem>(PolicyItem);
            var addedPolicyItem = await _PolicyItemRepository.Add(newPolicyItem);
            return addedPolicyItem != null;
        }

        public async Task<int> UpdatePolicyItem(int id, PolicyItemDTO PolicyItem)
        {
            try
            {
                var prod = _PolicyItemRepository.Get(id);
                if (prod != null)
                {
                    PolicyItem newPolicyItem = _mapper.Map<PolicyItem>(PolicyItem);
                    var updatedPolicyItem = await _PolicyItemRepository.Update(id, newPolicyItem);
                    return updatedPolicyItem.SNo;
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

        public async Task<PolicyItem> GetPolicyItem(int id)
        {
            try
            {
                var PolicyItem = await _PolicyItemRepository.Get(id);
                if (PolicyItem != null)
                {
                    return PolicyItem;
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

        public async Task<IEnumerable<PolicyItem>> GetAllPolicyItem()
        {
            try
            {
                var PolicyItem = await _PolicyItemRepository.GetAll();
                if (PolicyItem != null)
                {
                    return PolicyItem;
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
