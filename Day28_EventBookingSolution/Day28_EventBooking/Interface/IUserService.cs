using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Interface
{
    public interface IUserService
    {
        public Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser);
        public Task<LoginResponseDTO> Register(UserCreateDTO registerUser);
    }
}
