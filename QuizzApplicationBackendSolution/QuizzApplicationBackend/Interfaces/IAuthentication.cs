using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IAuthentication
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(CreateUserDTO loginUser);

        public Task<IEnumerable<string>> GetRoles();

        public Task<IEnumerable<LoginResponseDTO>> Search(string name);

        public Task<LoginResponseDTO> UpdateUser(string username, UpdateUserDTO updateUserDTO);

        public Task<string> UploadImage(string username, IFormFile imageFile);
    }
}
