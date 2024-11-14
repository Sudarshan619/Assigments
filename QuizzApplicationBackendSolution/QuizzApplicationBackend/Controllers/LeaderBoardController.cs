using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Exceptions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class LeaderBoardController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;

        public LeaderBoardController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> CreateLeaderBoard( LeaderBoardDTO leaderBoardDto)
        {
            try
            {

                if (leaderBoardDto == null) {
                    return BadRequest("LeaderBoardDTo is required");
                }
                var isCreated = await _leaderBoardService.CreateLeaderBoard(leaderBoardDto);
                if (isCreated)
                {
                    return Ok("LeaderBoard created successfully.");
                }
                return BadRequest("Failed to create LeaderBoard.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeaderBoard(int id)
        {
            try
            {
                var leaderBoard = await _leaderBoardService.GetLeaderBoard(id);
                return Ok(leaderBoard);
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

        [HttpGet("sort/{id}")]
        public async Task<IActionResult> SortLeaderBoard(int id, Choice choice)
        {
            try
            {
                var sortedLeaderBoard = await _leaderBoardService.SortLeaderBoard(choice, id);
                return Ok(sortedLeaderBoard);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeaderBoards(int pageNumber,int pageSize)
        {
            try
            {
                var leaderBoards = await _leaderBoardService.GetAllLeaderBoard(pageNumber,pageSize);
                return Ok(leaderBoards);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> UpdateLeaderBoard(int id,LeaderBoardDTO leaderBoardDto)
        {
            try
            {
                if (leaderBoardDto == null)
                {
                    return BadRequest("LeaderBoardDTO is required");
                }

                var isUpdated = await _leaderBoardService.UpdateLeaderBoard(id, leaderBoardDto);
                if (isUpdated)
                {
                    return NoContent();
                }

                return NotFound($"LeaderBoard with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> DeleteLeaderBoard(int id)
        {
            try
            {
                var isDeleted = await _leaderBoardService.DeleteLeaderBoard(id);
                if (isDeleted)
                {
                    return Ok($"LeaderBoard with ID {id} successfully deleted.");
                }

                return NotFound($"LeaderBoard with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
