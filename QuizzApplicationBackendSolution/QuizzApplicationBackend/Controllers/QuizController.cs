﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
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
                throw new NotFoundException($"Quiz with ID {id} not found.");
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
                throw new NotFoundException($"Quiz with ID {id} not found.");
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
                return NotFound("No quizzes found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string quizTitle)
        {
            try
            {
                var quizzes = await _quizService.Search(quizTitle);
                return Ok(quizzes);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound("No quizzes found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("by category")]
        public async Task<IActionResult> GetAllQuizzes(Categories category)
        {
            try
            {
                var quizzes = await _quizService.GetAllQuizzesWithQuestionsByCategory(category);
                return Ok(quizzes);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound("No quizzes found in this category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }

        [HttpGet("Getallcategory")]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var category = await _quizService.GetAllCategory();
                return Ok(category);
            }
            catch (CollectionEmptyException ex)
            {
                return NotFound("No quizzes found in this category.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}
