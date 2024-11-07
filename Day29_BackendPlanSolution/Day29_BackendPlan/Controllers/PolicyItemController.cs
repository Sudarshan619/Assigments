using Microsoft.AspNetCore.Cors.Infrastructure;
using Day29_BackendPlan.Services;
using Day29_BackendPlan.Interface;
using Day29_BackendPlan.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Day29_BackendPlan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyItemController : ControllerBase
    {
        private readonly IPolicyItemService _PolicyItemService;
        private readonly ILogger<PolicyItemController> _logger;

        public PolicyItemController(IPolicyItemService PolicyItemService, ILogger<PolicyItemController> logger)
        {
            _PolicyItemService = PolicyItemService;
            _logger = logger;

        }
        [HttpPost]
        public async Task<IActionResult> AddPolicyItem(PolicyItemDTO PolicyItem)
        {
            try
            {
                var PolicyItemId = await _PolicyItemService.AddPolicyItem(PolicyItem);
                return Ok("PolicyItem added");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdatePolicyItem(int id, PolicyItemDTO PolicyItem)
        {
            try
            {
                var PolicyItemId = await _PolicyItemService.UpdatePolicyItem(id, PolicyItem);
                return Ok(PolicyItemId);
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
                var PolicyItem = await _PolicyItemService.GetAllPolicyItem();
                return Ok(PolicyItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPolicyItem(int id)
        {
            try
            {
                var PolicyItem = await _PolicyItemService.GetPolicyItem(id);
                return Ok(PolicyItem);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

    }
}
