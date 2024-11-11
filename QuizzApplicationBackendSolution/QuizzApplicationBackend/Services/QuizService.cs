﻿using AutoMapper;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Exceptions;
using QuizzApplicationBackend.Interfaces;
using QuizzApplicationBackend.Models;

public class QuizService : IQuizService<IEnumerable<Question>, int>
{
    private readonly IRepository<int, Quiz> _quizRepository;
    private readonly IRepository<int, Question> _questionRepository;
    private readonly IRepository<int, Option> _optionRepository;
    private readonly IMapper _mapper;

    public QuizService(IRepository<int, Quiz> quizRepository, IRepository<int, Question> questionRepository, IRepository<int, Option> optionRepository, IMapper mapper)
    {
        _quizRepository = quizRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
        _mapper = mapper;
    }

    public async Task<bool> CreateQuiz(QuizDTO quizDto)
    {
        try
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            var question = await GetRandomQuestionsByCategory(quiz.Category, quizDto.NoOfQuestions);
            quiz.Questions = question.ToList();
            quiz.NoOfQuestions = quizDto.NoOfQuestions;
            quiz.MaxPoint = question.Select(q => q.Points).Sum();

            var result = await _quizRepository.Add(quiz);
            return result != null;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while creating quiz: " + ex.Message);
        }
    }

    public async Task<bool> DeleteQuiz(int id)
    {
        try
        {
            var result = await _quizRepository.Delete(id);
            return result != null;
        }
        catch (NotFoundException)
        {
            throw new NotFoundException($"Quiz with ID {id} not found.");
        }
        catch (Exception ex)
        {
            throw new Exception("Error while deleting quiz: " + ex.Message);
        }
    }

    public async Task<bool> EditQuiz(int id, QuizDTO quizDto)
    {
        try
        {
            var existingQuiz = await _quizRepository.Get(id);
            var updatedQuiz = _mapper.Map(quizDto, existingQuiz);

            var result = await _quizRepository.Update(id, updatedQuiz);
            return result != null;
        }
        catch (NotFoundException)
        {
            throw new NotFoundException($"Quiz with ID {id} not found.");
        }
        catch (Exception ex)
        {
            throw new Exception("Error while editing quiz: " + ex.Message);
        }
    }

    public async Task<QuizQuestionReponseDTO> GetQuiz(int id)
    {
        try
        {
            var quiz = await _quizRepository.Get(id);

            if (quiz == null)
            {
                throw new NotFoundException($"Quiz with ID {id} not found.");
            }

            var questions = await _questionRepository.GetAll();

            var quizQuestions = questions
            .Where(q => q.Category == quiz.Category)
            .Take(quiz.NoOfQuestions)
            .ToList();

            var options = await _optionRepository.GetAll();

            var questionDtos = quizQuestions.Select(question => new QuestionResponseDTO()
            {
                QuestionId = question.QuestionId,
                QuestionName = question.QuestionName,
                Category = question.Category,
                Points = question.Points,
                Options = options.Where(option => option.QuestionId == question.QuestionId)
                                 .Select(option => new OptionResponseDTO()
                                 {
                                     OptionId = option.OptionId,
                                     Text = option.Text,
                                    
                                 })
                                 .ToList()
            }).ToList();

            return new QuizQuestionReponseDTO()
            {
                QuizId = quiz.QuizId,
                Title = quiz.Title,
                questions = questionDtos,
                MaxPoints = quiz.MaxPoint,
            };
        }
        catch (Exception ex)
        {
            throw new Exception("Error while retrieving quiz: " + ex.Message);
        }
    }

    // Other than CRUD
    public async Task<IEnumerable<Question>> GetRandomQuestionsByCategory(Categories category, int noOfQuestions)
    {
        try
        {
            var questions = await _questionRepository.GetAll();
            var filteredQuestions = questions.Where(q => q.Category == category).ToList();

            if (!filteredQuestions.Any())
            {
                throw new CollectionEmptyException("No questions found for the specified category.");
            }

            var random = new Random();
            var randomizedQuestions = filteredQuestions.OrderBy(q => random.Next()).ToList();

            var selectedQuestions = randomizedQuestions.Take(noOfQuestions).ToList();

            return selectedQuestions;
        }
        catch (CollectionEmptyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while retrieving random questions by category: " + ex.Message);
        }
    }

    public async Task<IEnumerable<QuizQuestionReponseDTO>> GetAllQuizzesWithQuestions()
    {
        try
        {
            var quizzes = await _quizRepository.GetAll();

            if (!quizzes.Any())
            {
                throw new CollectionEmptyException("No quizzes available.");
            }

            var questions = await _questionRepository.GetAll();
            var options = await _optionRepository.GetAll();

            var quizResponseDtos = quizzes.Select(quiz =>
            {
                var quizQuestions = questions
                    .Where(q => q.Category == quiz.Category)
                    .Take(quiz.NoOfQuestions)
                    .ToList();

                var questionDtos = quizQuestions.Select(question => new QuestionResponseDTO()
                {
                    QuestionId = question.QuestionId,
                    QuestionName = question.QuestionName,
                    Category = question.Category,
                    Points = question.Points,
                    Options = options
                        .Where(option => option.QuestionId == question.QuestionId)
                        .Select(option => new OptionResponseDTO()
                        {
                            OptionId = option.OptionId,
                            Text = option.Text
                        })
                        .ToList()
                }).ToList();

                return new QuizQuestionReponseDTO()
                {
                    QuizId = quiz.QuizId,
                    Title = quiz.Title,
                    MaxPoints = quiz.MaxPoint,
                    questions = questionDtos
                };
            });

            return quizResponseDtos;
        }
        catch (CollectionEmptyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new Exception("Error while retrieving quizzes: " + ex.Message);
        }
    }


}
