using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizzApplicationBackend.Services
{
    public class ScoreCardService : IScoreCardService
    {
        private readonly IRepository<int, ScoreCard> _scoreCardRepository;
        private readonly IRepository<int, Quiz> _quizRepository;
        private readonly IRepository<string , User> _userRepository;
        private readonly IMapper _mapper;

        public ScoreCardService(IRepository<string, User> userRepository,IRepository<int, ScoreCard> scoreCardRepository, IRepository<int, Quiz> quizRepository, IMapper mapper)
        {
            _scoreCardRepository = scoreCardRepository;
            _userRepository = userRepository;
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateScoreCard(ScoreCardDTO scoreCardDto)
        {
            var user = await _userRepository.GetAll();
            if(user == null)
            {
                throw new NotFoundException("User not found while gettinf scorecard");
            }

            
                var scoreCard = _mapper.Map<ScoreCard>(scoreCardDto);
                var accuracy = await CalculateAccuracy(scoreCardDto);
                scoreCard.Acuuracy = accuracy;
                var result = await _scoreCardRepository.Add(scoreCard);
                return result != null;
 
            
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
        private async Task<double> CalculateAccuracy(ScoreCardDTO scoreCard)
        {
            try
            {
                var quiz = await _quizRepository.Get(scoreCard.QuizId);

                double accuracy = ((double)scoreCard.Score / (double)quiz.MaxPoint) * 100;

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
