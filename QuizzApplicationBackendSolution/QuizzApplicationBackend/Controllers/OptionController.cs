using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class OptionController : ControllerBase
    {
        private readonly IOptionService _optionService;

        public OptionController(IOptionService optionService)
        {
            _optionService = optionService;
        }

        [HttpGet]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> GetAllOptions(int pageNumber)
        {
            try
            {
                var options = await _optionService.GetAllOptions(pageNumber);
                return Ok(options);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound( ex.Message );
            }
        }

        // GET: api/Option/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> GetOption(int id)
        {
            try
            {
                var option = await _optionService.GetOption(id);
                return Ok(option);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        [HttpPost]
        [Authorize(Roles = "QuizzCreator")]
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CreateMultipleOption")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> CreateMultipleOption(IEnumerable<OptionDTO> optionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var success = await _optionService.CreateOptionBulk(optionDto);
                if (success)
                    return Ok(success);

                return BadRequest("Could not create option.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "QuizzCreator")]
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

                throw new NotFoundException($"Option with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> DeleteOption(int id)
        {
            try
            {
                var success = await _optionService.DeleteOption(id);
                if (success)
                    return NoContent();
                throw new NotFoundException("Not found");

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
           
        }
    }
}
