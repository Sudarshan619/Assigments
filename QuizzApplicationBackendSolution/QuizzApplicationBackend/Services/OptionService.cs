using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Exceptions;
using AutoMapper;

namespace QuizzApplicationBackend.Services
{
    public class OptionService : IOptionService
    {
        private readonly IRepository<int, Option> _repository;
        private readonly IMapper _mapper;

        public OptionService(IRepository<int, Option> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> CreateOption(OptionDTO optionDto)
        {
            try
            {
                var option = _mapper.Map<Option>(optionDto);
                var result = await _repository.Add(option);
                return result != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating option: " + ex.Message);
            }
        }

        public async Task<bool> DeleteOption(int id)
        {
            try
            {
                var result = await _repository.Delete(id);
                return result != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Option with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting option: " + ex.Message);
            }
        }

        public async Task<bool> EditOption(int id, OptionDTO optionDto)
        {
            try
            {
                var existingOption = await _repository.Get(id);
                var updatedOption = _mapper.Map(optionDto, existingOption);

                var result = await _repository.Update(id, updatedOption);
                return result != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Option with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while editing option: " + ex.Message);
            }
        }

        public async Task<OptionResponseDTO> GetOption(int id)
        {
            try
            {
                var option = await _repository.Get(id);
                return _mapper.Map<OptionResponseDTO>(option);
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Option with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving option: " + ex.Message);
            }
        }

        public async Task<IEnumerable<OptionResponseDTO>> GetAllOptions(int pageNumber)
        {
            try
            {
                var options = await _repository.GetAll();
                var paginatedOptions = options
           .Skip((pageNumber - 1) * 5)
           .Take(5)
           .ToList();

                return _mapper.Map<IEnumerable<OptionResponseDTO>>(paginatedOptions);
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("No options available.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving options: " + ex.Message);
            }
        }

        public async Task<bool> CreateOptionBulk(IEnumerable<OptionDTO> options)
        {
            try
            {
                bool isAdded = true;
                foreach(var option in options)
                {
                    var requiredOption = _mapper.Map<Option>(option);
                    var result = await _repository.Add(requiredOption);

                    if (result == null) {
                        return !isAdded;
                    }
                } 
                return isAdded;
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error while creating option: " + ex.Message);
            }
        }
    }
}
