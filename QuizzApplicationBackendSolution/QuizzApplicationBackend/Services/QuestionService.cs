﻿using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Repositories;
using System.Collections.Generic;

namespace QuizzApplicationBackend.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IRepository<int, Question> _repository;
        private readonly IRepository<int, Option> _optionRepository;
        private readonly IMapper _mapper;

        public QuestionService(IRepository<int, Question> repository, IRepository<int, Option> optionRepository, IMapper mapper)
        {
            _repository = repository;
            _optionRepository = optionRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateQuestion(QuestionDTO question)
        {
            var questionEntity = _mapper.Map<Question>(question);
            var result = await _repository.Add(questionEntity);
            return result != null;
        }

        public async Task<bool> DeleteQuestion(int id)
        {
            try
            {
                var res = await _repository.Delete(id);
                return res != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Question with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while deleting question: " + ex.Message);
            }
        }

        //Other than crud
        public async Task<bool> AddOptionsToQuestion(int id)
        {
            var question = await _repository.Get(id);
            var option = await _optionRepository.GetAll();
            var requiredOption = option.Where(e => e.QuestionId == question.QuestionId).ToList();
            question.Options = requiredOption;

            return requiredOption != null;
        }

        public async Task<QuestionResponseDTO> GetQuestion(int id)
        {
            var questionEntity = await _repository.Get(id);

            // Fetch the options for this question based on the QuestionId
            var options = await _optionRepository.GetAll();
            var questionOptions = options.Where(option => option.QuestionId == id).ToList();

            try
            {
                return new QuestionResponseDTO()
                {
                    QuestionName = questionEntity.QuestionName,
                    Category = questionEntity.Category,
                    Options = _mapper.Map<ICollection<OptionResponseDTO>>(questionOptions)
                };
            }
            
            catch (NotFoundException)
            {
                throw new NotFoundException($"Question with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving question: " + ex.Message);
            }
        }

        public async Task<IEnumerable<QuestionResponseDTO>> Search(string question)
        {
            try
            {
                var questions = await _repository.GetAll();
                var requiredQuestions = questions.Where(q => !string.IsNullOrEmpty(q.QuestionName) &&
                        q.QuestionName.Contains(question, StringComparison.OrdinalIgnoreCase));
                var questionDTOs = requiredQuestions.Select(q => new QuestionResponseDTO
                {
                    QuestionId = q.QuestionId,
                    QuestionName = q.QuestionName,
                });
                return questionDTOs;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<QuestionResponseDTO>> GetAllQuestions(int pageNumber)
        {
            try
            {
                var questions = await _repository.GetAll();

                var allOptions = await _optionRepository.GetAll();

                var questionResponseDTOs = questions.Select(question =>
                {
                    var questionOptions = allOptions.Where(option => option.QuestionId == question.QuestionId).ToList();
                    return new QuestionResponseDTO
                    {
                        QuestionId = question.QuestionId,
                        QuestionName = question.QuestionName,
                        Category = question.Category,
                        Points = question.Points,
                        Options = _mapper.Map<ICollection<OptionResponseDTO>>(questionOptions)
                    };
                });

                var paginatedQuestions = questionResponseDTOs.
                    Skip((pageNumber - 1) * 5)
                    .Take(5)
                    .ToList();

                return paginatedQuestions;
            }
            catch (CollectionEmptyException)
            {
                throw new CollectionEmptyException("No questions available.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while retrieving questions: " + ex.Message);
            }
        }

       

        public async Task<bool> EditQuestion(int id, EditQuestionDTO question)
        {
            try
            {
                var existingQuestion = await _repository.Get(id);
                var updatedQuestion = _mapper.Map(question, existingQuestion);

                var result = await _repository.Update(id, updatedQuestion);
                return result != null;
            }
            catch (NotFoundException)
            {
                throw new NotFoundException($"Question with ID {id} not found.");
            }
            catch (Exception ex)
            {
                throw new Exception("Error while editing question: " + ex.Message);
            }
        }
    }
}
