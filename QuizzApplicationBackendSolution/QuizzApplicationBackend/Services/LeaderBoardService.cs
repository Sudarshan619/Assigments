using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Models;
using AutoMapper;
using QuizzApplicationBackend.Repositories;

namespace QuizzApplicationBackend.Services
{
    public class LeaderBoardService : ILeaderBoardService
    {
        private readonly IRepository<int, LeaderBoard> _leaderBoardRepository;
        private readonly IRepository<int, ScoreCard> _scoreCardRepository;
        private readonly IRepository<int, Quiz> _quizRepository;
        private readonly IRepository<string, User> _userRepo;

        private readonly IMapper _mapper;

        public LeaderBoardService(IRepository<int, Quiz> quizRepository,IRepository<int,LeaderBoard> repository, IRepository<int, ScoreCard> scoreCardRepository, IRepository<string, User> userRepo,IMapper mapper)
        {
            _leaderBoardRepository = repository;
            _scoreCardRepository = scoreCardRepository;
            _quizRepository = quizRepository;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<bool> CreateLeaderBoard(LeaderBoardDTO _leaderBoard)
        {
            try
            {
                var scoreCards = await _scoreCardRepository.GetAll();
                var quiz = await _quizRepository.GetAll();
                var requiredquiz = quiz.FirstOrDefault(e => e.Category == _leaderBoard.Category);
                var requiredScores = scoreCards.Where(e => e.QuizId == requiredquiz.QuizId);
                var leaderBoard = _mapper.Map<LeaderBoard>(_leaderBoard);
                leaderBoard.ScoreCard = scoreCards;
                var result =  await _leaderBoardRepository.Add(leaderBoard);
                return result != null;
            }
            catch(Exception ex)
            {
                throw new Exception("Could not create leader board");
            }
        }

        public Task<bool> DeleteLeaderBoard(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<LeaderBoardResponseDTO> GetLeaderBoard(int id)
        {
            try
            {
                var leaderBoard = await _leaderBoardRepository.Get(id);

                if (leaderBoard == null)
                {
                    throw new CollectionEmptyException("No leaderBoard found");
                }

                var scoreCards = await _scoreCardRepository.GetAll();
                var quiz = await _quizRepository.GetAll();
                var requiredQuiz = quiz.FirstOrDefault(e => e.Category == leaderBoard.Categories);

                if (requiredQuiz == null)
                {
                    throw new Exception("Quiz with specified category not found");
                }

                var scoreCardDtos = scoreCards
                    .Where(e => e.QuizId == requiredQuiz.QuizId)
                    .Select(sc => _mapper.Map<ScoreCardResponseDTO>(sc))
                    .ToList();

                return new LeaderBoardResponseDTO()
                {
                    LeaderBoardName = leaderBoard.LeaderBoardName,
                    Category = leaderBoard.Categories,
                    ScoreCards = scoreCardDtos
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get leader board: " + ex.Message);
            }
        }


        public async Task<IEnumerable<LeaderBoardDTO>> GetAllLeaderBoard()
        {
            //try
            //{
            //    var leaderBoard = await _leaderBoardRepository.GetAll();


            //    if (leaderBoard ==null) {

            //        throw new CollectionEmptyException("No leaderBoard found");
            //    }
            //    var scoreCards = await _scoreCardRepository.GetAll();


            //    return leaderBoardDTOs;


            //}
            //catch(Exception ex)
            //{

            //}
            throw new NotImplementedException();
        }

        public async Task<LeaderBoardResponseDTO> SortLeaderBoard(Choice choice,int id)
        {
            var scoreCards = await _scoreCardRepository.GetAll();
            var leaderBoard = await _leaderBoardRepository.Get(id);
            var filteredScoreCards = scoreCards.Where(e => e.ScoreCardId == id);

            
            var users = await _userRepo.GetAll();

            var sortedScoreCards = filteredScoreCards
                .Select(sc => new ScoreCardResponseDTO
                {
                    Username = users.FirstOrDefault(u => u.Id == sc.UserId)?.Name ?? "Unknown User",
                    Score = sc.Score,
                    QuizId = sc.QuizId,
                    Acuuracy = sc.Acuuracy
                });

            if (choice == Choice.Score)
            {
                sortedScoreCards = sortedScoreCards.OrderBy(sc => sc.Score).ToList();
            }
            else if (choice == Choice.Username)
            {
                sortedScoreCards = sortedScoreCards.OrderBy(sc => sc.Username).ToList();
            }
            //var scoreCard = _mapper.Map<IEnumerable<ScoreCard>>(sortedScoreCards);
            return new LeaderBoardResponseDTO
            {
                LeaderBoardName = leaderBoard.LeaderBoardName,
                Category = leaderBoard.Categories,
                ScoreCards = sortedScoreCards
                    .Select(sc => _mapper.Map<ScoreCardResponseDTO>(sc))
                    .ToList()
            };

        }
        public Task<bool> UpdateLeaderBoard(int id,LeaderBoardDTO leaderBoard)
        {
            throw new NotImplementedException();
        }
    }
}
