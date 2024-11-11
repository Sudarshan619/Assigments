using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Services;
using QuizzApplicationBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Services
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly IRepository<int, ScoreCard> _scoreCardRepository;
        private readonly IQuizService<IEnumerable<Question>, int> _quizService;
        private readonly IRepository<int, Option> _optionRepo;
        private readonly IQuestionService _questionService;
        private readonly IRepository<string , User> _userRepository;
        private readonly IRepository<int, Quiz> _quizRepository;
        private readonly IMapper _mapper;

        public ScoreCardService(IRepository<int, Option> optionRepo,IQuestionService questionService,IRepository<string, User> userRepository,IRepository<int, ScoreCard> scoreCardRepository, IQuizService<IEnumerable<Question>, int> quizService, IRepository<int, Quiz> quizRepository, IMapper mapper)
        {
            _scoreCardRepository = scoreCardRepository;
            _userRepository = userRepository;
            _questionService = questionService;
            _optionRepo = optionRepo;
            _quizRepository = quizRepository;
            _quizService = quizService;
            _mapper = mapper;
        }

        public async Task<bool> CreateScoreCard(SubmittedOptionDTO submittedOptionDTO)
        {
            var user = await _userRepository.GetAll();
            if(user == null)
            {
                throw new NotFoundException("User not found while gettinf scorecard");
            }         
                var scoreCard = _mapper.Map<ScoreCard>(submittedOptionDTO);
                scoreCard.Score =await  CalculateScore(submittedOptionDTO);
                var accuracy = await CalculateAccuracy(submittedOptionDTO, scoreCard.Score);
                scoreCard.Acuuracy = accuracy;
                var result = await _scoreCardRepository.Add(scoreCard);
                return result != null;
            
        }

        public async Task<int> CalculateScore(SubmittedOptionDTO submittedOptionDTO)
        {
            // Retrieve the quiz by its ID
            var quiz = await _quizService.GetQuiz(submittedOptionDTO.QuizId);
            if (quiz == null)
            {
                return 0;
            }

            var allQuestions = await _questionService.GetAllQuestions(1);
            var options = await _optionRepo.GetAll();

            int totalScore = 0;

            var questions = quiz.questions;

            foreach (var selectedOption in submittedOptionDTO.Options)
            {
                var question = questions.FirstOrDefault(q => q.Options.Any(o => o.OptionId == selectedOption.OptionId));
                Console.WriteLine("question" + question.QuestionId);

                if (question != null)
                {
                   
                    var correctOption = options.FirstOrDefault(o => o.QuestionId == question.QuestionId && o.IsCorrect);
                    Console.WriteLine("Correct optonn" + correctOption.OptionId);
                    if (correctOption != null && correctOption.OptionId == selectedOption.OptionId)
                    {
                        totalScore += question.Points;
                    }
                     Console.WriteLine("Inside the if statement - Correct answer found.");
                }
            }

            return totalScore;
        }



        public async Task<bool> UpdateScoreCard(int id, ScoreCardDTO scoreCardDto)
        {
            var existingScoreCard = await _scoreCardRepository.Get(id);
            if (existingScoreCard == null)
            {
                throw new NotFoundException($"ScoreCard with ID {id} not found.");
            }

            var updatedScoreCard = _mapper.Map(scoreCardDto, existingScoreCard);
            var result = await _scoreCardRepository.Update(id, updatedScoreCard);
            return result != null;
        }
        private async Task<double> CalculateAccuracy(SubmittedOptionDTO submittedOptionDTO, int score)
        {
            try
            {
                var quiz = await _quizRepository.Get(submittedOptionDTO.QuizId);

                double accuracy = ((double)score / (double)quiz.MaxPoint) * 100;

                return accuracy;
            }
            catch (Exception ex) {

                throw new Exception("Error while calulating accuracy");
            
            }          
        }

        public async Task<bool> DeleteScoreCard(int id)
        {
            var result = await _scoreCardRepository.Delete(id);
            return result != null;
        }

        public async Task<ScoreCardResponseDTO> GetScoreCard(int id)
        {
            var scoreCard = await _scoreCardRepository.Get(id);
            if (scoreCard == null)
            {
                throw new NotFoundException($"ScoreCard with ID {id} not found.");
            }

            try
            {
                var score = _mapper.Map<ScoreCardResponseDTO>(scoreCard);
                var user = await _userRepository.GetAll();
                var requiredUser = user.FirstOrDefault(e => e.Id == scoreCard.UserId);
                score.Username = requiredUser.Name;

                return score;
            }

            catch (Exception ex) { 
                throw new Exception("Could not get scorecard");
            }
            
        }

    }
}
