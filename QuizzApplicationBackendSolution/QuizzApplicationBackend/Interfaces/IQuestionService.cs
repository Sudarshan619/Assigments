﻿using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Models;

namespace QuizzApplicationBackend.Interfaces
{
    public interface IQuestionService
    {

        public Task<bool> CreateQuestion(QuestionDTO question);

        public Task<bool> DeleteQuestion(int id);

        public Task<bool> EditQuestion(int id , QuestionDTO question);

        public Task<bool> AddOptionsToQuestion(int id);

        public Task<IEnumerable<QuestionDTO>> GetAllQuestions();

        public Task<QuestionResponseDTO> GetQuestion(int id);


    }
}