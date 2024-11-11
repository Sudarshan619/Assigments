using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "QuizzCreator")]
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
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            try
            {
                var result = await _quizService.DeleteQuiz(id);
                if (result)
                {
                    return Ok(result);
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
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> EditQuiz(int id,QuizDTO quizDto)
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
                    return NoContent();
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

        [HttpGet]
        public async Task<IActionResult> GetAllQuizzes()
        {
            try
            {
                var quizzes = await _quizService.GetAllQuizzesWithQuestions();
                return Ok(quizzes);
            }
            catch (CollectionEmptyException ex)
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
