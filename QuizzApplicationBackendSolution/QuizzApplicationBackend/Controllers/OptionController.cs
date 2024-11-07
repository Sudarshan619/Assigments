using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOptions()
        {
            try
            {
                var options = await _optionService.GetAllOptions();
                return Ok(options);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET: api/Option/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOption(int id)
        {
            try
            {
                var option = await _optionService.GetOption(id);
                return Ok(option);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST: api/Option
        [HttpPost]
        public async Task<IActionResult> CreateOption(OptionDTO optionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            try
            {
                var success = await _optionService.CreateOption(optionDto);
                if (success)
                    return Ok(success) ;
                return BadRequest("Could not create option.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditOption(int id,OptionDTO optionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            try
            {
                var success = await _optionService.EditOption(id, optionDto);
                if (success)
                    return NoContent();
                return NotFound($"Option with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            try
            {
                var success = await _optionService.DeleteOption(id);
                if (success)
                    return NoContent();
                return NotFound($"Option with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
