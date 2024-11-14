using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using System.Security.Cryptography;
using QuizzApplicationBackend.Exceptions;
using System.Text;

namespace QuizzApplicationBackend.Services
{
    public class UserService : IAuthentication
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<string, User> userRepository,
            ITokenService tokenService,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;


        }
        public async Task<LoginResponseDTO> Autheticate(LoginRequestDTO loginUser)
        {
            var user = await _userRepository.Get(loginUser.UserName);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }
            HMACSHA256 hmac = new HMACSHA256(user.HashKey);
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUser.Password));
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != user.Password[i])
                {
                    throw new NotFoundException("Invalid username or password");
                }
            }
            return new LoginResponseDTO()
            {
                Username = user.Name,
                Token = await _tokenService.GenerateToken(new UserTokenDTO()
                {
                    Username=user.Name,
                    Role = user.Role.ToString()
                }),
                StatusCode = 200
            };
        }

        public async Task<LoginResponseDTO> Register(CreateUserDTO  registerUser)
        {
            HMACSHA256 hmac = new HMACSHA256();
            byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password));
            User user = new User()
            {
                 Name = registerUser.Username,
                Password = passwordHash,
                Email = registerUser.Email,
                HashKey = hmac.Key,
                Role = registerUser.Role
            };
            try
            {
                var addesUser = await _userRepository.Add(user);
                LoginResponseDTO response = new LoginResponseDTO()
                {
                    Username = user.Name,
                    Token = "",
                    StatusCode = 200,
                };
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Could not register user");
                throw new NotFoundException("Could not register user");
            }
        }

        public Task<IEnumerable<string>> GetRoles()
        {
            var roles = Enum.GetNames(typeof(Roles)).ToList();
            return Task.FromResult<IEnumerable<string>>(roles);
        }
    }
}
