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
                var requiredquiz = quiz.FirstOrDefault(e => e.QuizId == _leaderBoard.QuizId);
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
                var users = await _userRepo.GetAll();
                var quiz = await _quizRepository.GetAll();
                var requiredQuiz = quiz.FirstOrDefault(e => e.QuizId == leaderBoard.QuizId);

                if (requiredQuiz == null)
                {
                    throw new Exception("Quiz with specified category not found");
                }
                var sortedScoreCards = scoreCards
                .Select(sc => new ScoreCardResponseDTO
                {
                    ScoreCardId = sc.ScoreCardId,
                    Username = users.FirstOrDefault(u => u.Id == sc.UserId)?.Name ?? "Unknown User",
                    Score = sc.Score,
                    QuizId = sc.QuizId,
                    Acuuracy = sc.Acuuracy
                });

                sortedScoreCards = sortedScoreCards.OrderByDescending(sc => sc.Score).ToList();

                var scoreCardDtos = sortedScoreCards
                    .Where(e => e.QuizId == requiredQuiz.QuizId)
                    .Select(sc => _mapper.Map<ScoreCardResponseDTO>(sc))
                    .ToList();

                return new LeaderBoardResponseDTO()
                {
                    LeaderBoardName = leaderBoard.LeaderBoardName,
                    Category = leaderBoard.Categories.ToString(),
                    ScoreCards = scoreCardDtos
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Could not get leader board: " + ex.Message);
            }
        }


        public async Task<IEnumerable<LeaderBoardResponseDTO>> GetAllLeaderBoard(int pageNumber,int pageSize)
        {
            try
            {     
                var leaderBoards = await _leaderBoardRepository.GetAll();

                if (!leaderBoards.Any())
                {
                    throw new CollectionEmptyException("No leaderBoard found");
                }

               
                var scoreCards = await _scoreCardRepository.GetAll();
                var quizzes = await _quizRepository.GetAll();

                var users = await _userRepo.GetAll();

                var pagedLeaderBoards = leaderBoards
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();

                var leaderBoardDTOs = pagedLeaderBoards.Select(leaderBoard =>
                {
               
                    var requiredQuiz = quizzes.FirstOrDefault(q => q.QuizId == leaderBoard.QuizId);
                    if (requiredQuiz == null)
                    {
                        throw new Exception("Quiz with specified category not found");
                    }
                 var sortedScoreCards = scoreCards
                .Select(sc => new ScoreCardResponseDTO
                {
                    ScoreCardId = sc.ScoreCardId,
                    Username = users.FirstOrDefault(u => u.Id == sc.UserId)?.Name ?? "Unknown User",
                    Score = sc.Score,
                    QuizId = sc.QuizId,
                    Acuuracy = sc.Acuuracy
                });

                    var scoreCardDtos = sortedScoreCards
                        .Where(sc => sc.QuizId == requiredQuiz.QuizId)
                        .Select(sc => _mapper.Map<ScoreCardResponseDTO>(sc))
                        .ToList();


                    return new LeaderBoardResponseDTO
                    {
                        LeaderBoardName = leaderBoard.LeaderBoardName,
                        Category = leaderBoard.Categories.ToString(),
                        ScoreCards = scoreCardDtos
                    };
                }).ToList();

                return leaderBoardDTOs;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not retrieve leader boards: " + ex.Message);
            }
        }

        public async Task<LeaderBoardResponseDTO> SortLeaderBoard(Choice choice,int id)
        {
            var scoreCards = await _scoreCardRepository.GetAll();
            var leaderBoard = await _leaderBoardRepository.Get(id);
     
            var users = await _userRepo.GetAll();

            var sortedScoreCards = scoreCards
                .Select(sc => new ScoreCardResponseDTO
                {
                    ScoreCardId = sc.ScoreCardId,
                    Username = users.FirstOrDefault(u => u.Id == sc.UserId)?.Name ?? "Unknown User",
                    Score = sc.Score,
                    QuizId = sc.QuizId,
                    Acuuracy = sc.Acuuracy
                });

            if (choice == Choice.Score)
            {
                sortedScoreCards = sortedScoreCards.OrderByDescending(sc => sc.Score).ToList();
            }
            else if (choice == Choice.Username)
            {
                sortedScoreCards = sortedScoreCards.OrderBy(sc => sc.Username).ToList();
            }

            return new LeaderBoardResponseDTO
            {
                LeaderBoardName = leaderBoard.LeaderBoardName,
                Category = leaderBoard.Categories.ToString(),
                ScoreCards = sortedScoreCards
                    .Select(sc => _mapper.Map<ScoreCardResponseDTO>(sc))
                    .ToList()
            };

        }
        public async Task<bool> DeleteLeaderBoard(int id)
        {
            try
            {
                var leaderBoard = await _leaderBoardRepository.Get(id);
                if (leaderBoard == null)
                {
                    throw new CollectionEmptyException($"LeaderBoard with ID {id} not found.");
                }

                await _leaderBoardRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not delete leader board: " + ex.Message);
            }
        }

        public async Task<bool> UpdateLeaderBoard(int id, LeaderBoardDTO leaderBoardDto)
        {
            try
            {
                var existingLeaderBoard = await _leaderBoardRepository.Get(id);
                if (existingLeaderBoard == null)
                {
                    throw new CollectionEmptyException($"LeaderBoard with ID {id} not found.");
                }

                existingLeaderBoard.LeaderBoardName = leaderBoardDto.LeaderBoardName;
                existingLeaderBoard.Categories = leaderBoardDto.Category;

                await _leaderBoardRepository.Update(id, existingLeaderBoard);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Could not update leader board: " + ex.Message);
            }
        }

    }
}
