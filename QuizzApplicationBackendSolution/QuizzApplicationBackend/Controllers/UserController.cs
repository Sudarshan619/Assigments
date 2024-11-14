using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace QuizzApplicationBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowAll")]
    public class UserController : ControllerBase
    {
        private readonly IAuthentication _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IAuthentication userService)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]

        public async Task<ActionResult<LoginResponseDTO>> Register(CreateUserDTO user)
        {
            var response = await _userService.Register(user);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO requestDTO)
        {
            var user = await _userService.Autheticate(requestDTO);
            return Ok(user);
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetRoles()
        {
            try
            {
                return await _userService.GetRoles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
