using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IOptionService
    {
        public Task<bool> CreateOption(OptionDTO question);

        public Task<bool> CreateOptionBulk(IEnumerable<OptionDTO> options);

        public Task<bool> DeleteOption(int id);

        public Task<OptionResponseDTO> GetOption(int id);

        public Task<IEnumerable<OptionResponseDTO>> GetAllOptions(int pageNumber);

        public Task<bool> EditOption(int id, OptionDTO question);
    }
}
