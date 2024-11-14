using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using QuizzApplicationBackend.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace QuizzApplicationBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        [Authorize(Roles = "QuizzCreator")]
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
        [Authorize(Roles = "QuizzCreator")]
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
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> GetAllQuestions(int pageNumber)
        {
            try
            {
                var questions = await _questionService.GetAllQuestions(pageNumber);
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

        [HttpPut("{id}")]
        [Authorize(Roles = "QuizzCreator")]
        public async Task<IActionResult> EditQuestion(int id, QuestionDTO questionDto)
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


        [HttpDelete("{id}")]
        [Authorize(Roles = "QuizzCreator")]
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

        [HttpPost("{id}/AddOptions")]
        [Authorize(Roles = "QuizzCreator")]
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
