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
    public class ScoreCardController : ControllerBase
    {
        private readonly IScoreCardService _scoreCardService;

        public ScoreCardController(IScoreCardService scoreCardService)
        {
            _scoreCardService = scoreCardService;
        }

        // POST: api/ScoreCard
        [HttpPost]
        public async Task<IActionResult> CreateScoreCard(ScoreCardDTO scoreCardDto)
        {
            if (scoreCardDto == null)
            {
                return BadRequest("ScoreCard data is required.");
            }

            var result = await _scoreCardService.CreateScoreCard(scoreCardDto);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Error while creating score card.");
        }

        // PUT: api/ScoreCard/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateScoreCard(int id, [FromBody] ScoreCardDTO scoreCardDto)
        {
            if (scoreCardDto == null)
            {
                return BadRequest("ScoreCard data is required.");
            }

            var result = await _scoreCardService.UpdateScoreCard(id, scoreCardDto);
            if (result)
            {
                return NoContent(); // Successfully updated
            }

            return NotFound($"ScoreCard with ID {id} not found.");
        }

        // DELETE: api/ScoreCard/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScoreCard(int id)
        {
            var result = await _scoreCardService.DeleteScoreCard(id);
            if (result)
            {
                return Ok($"successfull deleted for id {id}"); // Successfully deleted
            }

            return NotFound($"ScoreCard with ID {id} not found.");
        }

        // GET: api/ScoreCard/{id}
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
    }
}
