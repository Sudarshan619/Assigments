using Microsoft.AspNetCore.Cors.Infrastructure;
using Day29_BackendPlan.Services;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.DTO;
using Microsoft.AspNetCore.Mvc;
using Day29_BackendPlan.Misc;
using Microsoft.AspNetCore.Authorization;

namespace Day29_BackendPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [FileValidation]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _PolicyService;
        private readonly ILogger<PolicyController> _logger;

        public PolicyController(IPolicyService PolicyService, ILogger<PolicyController> logger)
        {
            _PolicyService = PolicyService;
            _logger = logger;

        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPolicy(PolicyDTO Policy)
        {
            if (ModelState.IsValid) { 
                await _PolicyService.AddPolicy(Policy);
                return Ok("Policy added");
            }
            else
            {
                throw new Exception("Invalid fields");
            }
                     
        }

        [HttpPost("Update")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePolicy(int id, PolicyDTO Policy)
        {
            if (ModelState.IsValid) { 
                var PolicyId = await _PolicyService.UpdatePolicy(id, Policy);
                return Ok(PolicyId);
            }
            else
            {
                throw new Exception("One or more field invalid");
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPolicies()
        {
            try
            {
                var Policy = await _PolicyService.GetAllPolicy();
                return Ok(Policy);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicy(int id)
        {
            try
            {
                var Policy = await _PolicyService.GetPolicy(id);
                return Ok(Policy);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

    }
}
