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
    [CustomExceptionFilter]
    public class ClaimReportController:ControllerBase
    {
        private readonly IClaimReportService _ClaimReportService;
        private readonly ILogger<ClaimReportController> _logger;

        public ClaimReportController(IClaimReportService ClaimReportService, ILogger<ClaimReportController> logger)
        {
            _ClaimReportService = ClaimReportService;
            _logger = logger;

        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddClaimReport(ClaimReportDTO ClaimReport)
       {
            
                if (ModelState.IsValid)
                {
                    await _ClaimReportService.AddClaimReport(ClaimReport);
                    return Ok("ClaimReport added");
                }
                else
                {
                throw new Exception("one or more valid exception");
                }
               
            
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateClaimReport(int id, ClaimReportDTO ClaimReport)
        {
            try
            {
                var ClaimReportId = await _ClaimReportService.UpdateClaimReport(id, ClaimReport);
                return Ok(ClaimReportId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }


        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllClaimReports()
        {
            try
            {
                var ClaimReport = await _ClaimReportService.GetAllReport();
                return Ok(ClaimReport);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetClaimReport(int id)
        {
            try
            {
                var ClaimReport = await _ClaimReportService.GetReport(id);
                return Ok(ClaimReport);
            }
            catch (Exception ex)
            {
                throw new Exception("Some error");
            }
        }

    }
}
