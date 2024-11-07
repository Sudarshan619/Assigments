using Day29_BackendPlan.DTO;

namespace Day29_BackendPlan.Interface
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerUser);
    }
}
