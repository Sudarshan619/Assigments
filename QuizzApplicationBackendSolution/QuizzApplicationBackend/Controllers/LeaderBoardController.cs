using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Exceptions;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly ILeaderBoardService _leaderBoardService;

        public LeaderBoardController(ILeaderBoardService leaderBoardService)
        {
            _leaderBoardService = leaderBoardService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLeaderBoard( LeaderBoardDTO leaderBoardDto)
        {
            try
            {
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
            catch (CollectionEmptyException ex)
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
        public async Task<IActionResult> GetAllLeaderBoards()
        {
            try
            {
                var leaderBoards = await _leaderBoardService.GetAllLeaderBoard();
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
    }
}
