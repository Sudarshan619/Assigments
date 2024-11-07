using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuizzApplicationBackend.Exceptions;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion(QuestionDTO questionDto)
        {
            var result = await _questionService.CreateQuestion(questionDto);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest("Failed to create question");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            try
            {
                var question = await _questionService.GetQuestion(id);
                return Ok(question);
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
        public async Task<IActionResult> GetAllQuestions()
        {
            try
            {
                var questions = await _questionService.GetAllQuestions();
                return Ok(questions);
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

        // PUT: api/Question/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> EditQuestion(int id, [FromBody] QuestionDTO questionDto)
        {
            try
            {
                var result = await _questionService.EditQuestion(id, questionDto);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Failed to edit question");
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

        // DELETE: api/Question/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var result = await _questionService.DeleteQuestion(id);
                if (result)
                {
                    return NoContent();
                }
                return BadRequest("Failed to delete question");
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

        // POST: api/Question/{id}/AddOptions
        [HttpPost("{id}/AddOptions")]
        public async Task<IActionResult> AddOptionsToQuestion(int id)
        {
            try
            {
                var result = await _questionService.AddOptionsToQuestion(id);
                if (result)
                {
                    return Ok("Options added to question successfully");
                }
                return BadRequest("Failed to add options to question");
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
