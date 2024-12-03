using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using System.Security.Cryptography;
using QuizzApplicationBackend.Exceptions;
using System.Text;
using AutoMapper;

namespace QuizzApplicationBackend.Services
{
    public class UserService : IAuthentication
    {
        private readonly IRepository<string, User> _userRepository;
        private readonly ILogger<UserService> _logger;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(IRepository<string, User> userRepository,
            ITokenService tokenService,
            ILogger<UserService> logger,
            IMapper mapper
            )
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;
            _mapper = mapper;


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
                Id = user.Id,
                Username = user.Name,
                Role = user.Role,
                Image = user.Image,
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
            var oldUser = await _userRepository.Get(registerUser.Username);
            if(oldUser != null && oldUser.Email == registerUser.Email)
            {
                throw new Exception("User already exist");
            }

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

        public async Task<IEnumerable<LoginResponseDTO>> Search(string name)
        {
            try
            {
                var users = await _userRepository.GetAll();
                var requiredUsers = users.Where(q => !string.IsNullOrEmpty(q.Name) &&
                        q.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

                if (requiredUsers.Any())
                {
                    return _mapper.Map<IEnumerable<LoginResponseDTO>>(requiredUsers);
                }
                else
                {
                    throw new NotFoundException("Users not found");
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<LoginResponseDTO> UpdateUser(string username, UpdateUserDTO updateUserDTO)
        {
            var existingUser = await _userRepository.Get(username);
            if (existingUser == null)
            {
                throw new NotFoundException($"User with username '{username}' not found.");
            }

            existingUser.Name = updateUserDTO.UserName ?? existingUser.Name;
            existingUser.Email = updateUserDTO.Email ?? existingUser.Email;

            try
            {
                var updatedUser = await _userRepository.Update(username, existingUser);
                return _mapper.Map<LoginResponseDTO>(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not update user.");
                throw new Exception("Could not update user. Please try again later.");
            }
        }

        public async Task<string> UploadImage(string username, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid image file.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(imageFile.FileName).ToLower();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Invalid file format. Only JPG, JPEG, PNG are allowed.");
            }

            var user = await _userRepository.Get(username);
            if (user == null)
            {
                throw new NotFoundException($"User with username '{username}' not found.");
            }

            string uploadsFolder = Path.Combine("wwwroot", "uploads", "user_images");
            Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = $"{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            user.Image = $"/uploads/user_images/{uniqueFileName}";

            try
            {
                await _userRepository.Update(username, user);
                return user.Image;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not upload image.");
                throw new Exception("Could not upload image. Please try again later.");
            }
        }



    }
}
