using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Services;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class ScoreCardController : ControllerBase
    {
        private readonly IScoreCardService _scoreCardService;

        public ScoreCardController(IScoreCardService scoreCardService)
        {
            _scoreCardService = scoreCardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateScoreCard(SubmittedOptionDTO submittedOptionDTO)
        {
            if (submittedOptionDTO == null)
            {
                return BadRequest("ScoreCard data is required.");
            }

            var result = await _scoreCardService.CreateScoreCard(submittedOptionDTO);
            if (result)
            {
                return Ok("Succesfully created");
            }
            return BadRequest("Error while creating score card.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScoreCard(int id,ScoreCardDTO scoreCardDto)
        {
            if (scoreCardDto == null)
            {
                return BadRequest("ScoreCard data is required.");
            }

            var result = await _scoreCardService.UpdateScoreCard(id, scoreCardDto);
            if (result)
            {
                return NoContent();
            }

            return NotFound($"ScoreCard with ID {id} not found.");
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScoreCard(int id)
        {
            var result = await _scoreCardService.DeleteScoreCard(id);
            if (result)
            {
                return Ok($"successfull deleted for id {id}");
            }

            return NotFound($"ScoreCard with ID {id} not found.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScoreCard(int id)
        {
            try
            {
                var scoreCard = await _scoreCardService.GetScoreCard(id);
                return Ok(scoreCard);
            }
            catch (NotFoundException)
            {
                return NotFound($"ScoreCard with ID {id} not found.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScoreCard()
        {
            try
            {
                var scoreCard = await _scoreCardService.GetAllScoreCards();
                return Ok(scoreCard);
            }
            catch (NotFoundException)
            {
                return NotFound($"ScoreCard is empty");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
