using Day27_Webapi_EF.DTO;

namespace Day27_Webapi_EF.Interface
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerUser);
    }
}
