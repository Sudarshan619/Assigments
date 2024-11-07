using Microsoft.AspNetCore.Cors.Infrastructure;
using Day29_BackendPlan.Services;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Day29_BackendPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyHolderController : ControllerBase
    {
        private readonly IPolicyHolderService _PolicyHolderService;
        private readonly ILogger<PolicyHolderController> _logger;

        public PolicyHolderController(IPolicyHolderService PolicyHolderService, ILogger<PolicyHolderController> logger)
        {
            _PolicyHolderService = PolicyHolderService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddPolicyHolder(PolicyHolderDTO PolicyHolder)
        {
            try
            {
                var PolicyHolderId = await _PolicyHolderService.AddPolicyHolder(PolicyHolder);
                return Ok("PolicyHolder added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdatePolicyHolder(int id, PolicyHolderDTO PolicyHolder)
        {
            try
            {
                var PolicyHolderId = await _PolicyHolderService.UpdatePolicyHolder(id, PolicyHolder);
                return Ok(PolicyHolderId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllPolicies()
        {
            try
            {
                var PolicyHolder = await _PolicyHolderService.GetAllPolicyHolder();
                return Ok(PolicyHolder);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicyHolder(int id)
        {
            try
            {
                var PolicyHolder = await _PolicyHolderService.GetPolicyHolder(id);
                return Ok(PolicyHolder);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

    }
}
