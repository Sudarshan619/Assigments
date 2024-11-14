using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IAuthentication
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(CreateUserDTO loginUser);

        public Task<IEnumerable<string>> GetRoles();
    }
}
