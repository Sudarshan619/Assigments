using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IOptionService
    {
        public Task<bool> CreateOption(OptionDTO question);

        public Task<bool> DeleteOption(int id);

        public Task<OptionDTO> GetOption(int id);

        public Task<IEnumerable<OptionDTO>> GetAllOptions();

        public Task<bool> EditOption(int id, OptionDTO question);
    }
}
