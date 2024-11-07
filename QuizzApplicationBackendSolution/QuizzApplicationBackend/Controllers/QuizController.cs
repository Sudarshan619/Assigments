using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService<IEnumerable<Question>, int> _quizService;

        public QuizController(IQuizService<IEnumerable<Question>, int> quizService)
        {
            _quizService = quizService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuiz(QuizDTO quizDto)
        {
            if (quizDto == null)
            {
                return BadRequest("Quiz data is null.");
            }

            var result = await _quizService.CreateQuiz(quizDto);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Failed to create quiz.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                var result = await _quizService.DeleteQuiz(id);
                if (result)
                {
                    return Ok(result); // Successfully deleted
                }
                return NotFound($"Quiz with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditQuiz(int id, [FromBody] QuizDTO quizDto)
        {
            if (quizDto == null)
            {
                return BadRequest("Quiz data is null.");
            }

            try
            {
                var result = await _quizService.EditQuiz(id, quizDto);
                if (result)
                {
                    return NoContent(); // Successfully edited
                }
                return NotFound($"Quiz with ID {id} not found.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuiz(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuiz(id);
                return Ok(quiz);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
