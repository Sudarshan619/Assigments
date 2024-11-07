using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Misc;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Day27_Webapi_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomExceptionFilter]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<LoginResponseDTO>> Register(UserCreateDTO createDTO)
        {
            
                if (ModelState.IsValid)
                {
                    var user = await _userService.Register(createDTO);
                    return Ok(user);
                }
                else
                {
                   throw new Exception("one or more validation error");
                }
          
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO requestDTO)
        {
            var user = await _userService.Autheticate(requestDTO);
                return Ok(user);
        }     
    }
}
