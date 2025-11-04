using Domain.Contexts;
using Domain.Handlers.QuizDTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Handlers.QuizHandler
{
    /// <summary>
    /// Handles all quiz-related operations such as listing quizzes,
    /// fetching quiz details, starting, and submitting quizzes.
    /// </summary>
    public class QuizHandler : IQuizHandler
    {
        private readonly DefaultContext _context;

        public QuizHandler(DefaultContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all active quizzes with basic information.
        /// </summary>
        public async Task<IEnumerable<QuizListDto>> GetAllActiveQuizzesAsync()
        {
            return await _context.Quizzes
                .Where(q => q.IsActive)
                .Select(q => new QuizListDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    DurationInMinutes = q.DurationInMinutes,
                    QuestionCount = q.Questions.Count,
                    PassingScore = q.PassingScore,
                    HasCompleted = false
                })
                .ToListAsync();
        }

        /// <summary>
        /// Gets quiz details including questions and options.
        /// </summary>
        public async Task<QuizDetailDto?> GetQuizByIdAsync(int quizId)
        {
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(q => q.Id == quizId && q.IsActive);

            if (quiz == null)
                return null;

            var quizDto = new QuizDetailDto
            {
                Id = quiz.Id,
                Title = quiz.Title,
                Description = quiz.Description,
                DurationInMinutes = quiz.DurationInMinutes,
                PassingScore = quiz.PassingScore,
                EnableNegativeMarking = quiz.EnableNegativeMarking,
                NegativeMarkingPercentage = quiz.NegativeMarkingPercentage,
                Questions = quiz.Questions.Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Text = q.Text,
                    QuestionType = (int)q.QuestionType,
                    Points = q.Points,
                    Order = q.Order,
                    Options = q.Options.Select(o => new OptionDto
                    {
                        Id = o.Id,
                        Text = o.Text,
                        Order = o.Order
                    }).ToList()
                }).ToList()
            };

            // Randomize questions if enabled
            quizDto.Questions = quiz.RandomizeQuestions
                ? quizDto.Questions.OrderBy(x => Guid.NewGuid()).ToList()
                : quizDto.Questions.OrderBy(q => q.Order).ToList();

            // Randomize options if enabled
            foreach (var question in quizDto.Questions)
            {
                question.Options = quiz.RandomizeOptions
                    ? question.Options.OrderBy(x => Guid.NewGuid()).ToList()
                    : question.Options.OrderBy(o => o.Order).ToList();
            }

            return quizDto;
        }

        /// <summary>
        /// Starts a quiz for a given user and returns quiz session info.
        /// </summary>
        public async Task<UserQuizDto?> StartQuizAsync(int quizId, string userId)
        {
            // Check if user already completed this quiz
            var existingQuiz = await _context.UserQuizzes
                .FirstOrDefaultAsync(uq => uq.QuizId == quizId && uq.UserId == userId && uq.IsCompleted);

            if (existingQuiz != null)
                return null;

            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                .FirstOrDefaultAsync(q => q.Id == quizId && q.IsActive);

            if (quiz == null)
                return null;

            var userQuiz = new UserQuiz
            {
                UserId = userId,
                QuizId = quizId,
                StartedAt = DateTime.UtcNow,
                TotalQuestions = quiz.Questions.Count,
                IsCompleted = false
            };

            _context.UserQuizzes.Add(userQuiz);
            await _context.SaveChangesAsync();

            return new UserQuizDto
            {
                UserQuizId = userQuiz.Id,
                QuizId = userQuiz.QuizId,
                StartedAt = userQuiz.StartedAt,
                ExpiresAt = userQuiz.StartedAt.AddMinutes(quiz.DurationInMinutes)
            };
        }

        /// <summary>
        /// Checks if a user has already completed a specific quiz.
        /// </summary>
        public async Task<bool> HasUserCompletedQuizAsync(int quizId, string userId)
        {
            return await _context.UserQuizzes
                .AnyAsync(uq => uq.QuizId == quizId && uq.UserId == userId && uq.IsCompleted);
        }

        /// <summary>
        /// Submits a quiz and calculates results including score, correctness, and details.
        /// </summary>
        public async Task<QuizResultDto> SubmitQuizAsync(SubmitQuizDto submitQuizDto)
        {
            var userQuiz = await _context.UserQuizzes
                .Include(uq => uq.Quiz)
                    .ThenInclude(q => q.Questions)
                        .ThenInclude(q => q.Options)
                .FirstOrDefaultAsync(uq => uq.Id == submitQuizDto.UserQuizId
                                          && uq.UserId == submitQuizDto.UserId
                                          && !uq.IsCompleted);

            if (userQuiz == null)
                throw new InvalidOperationException("Quiz not found or already completed.");

            var quiz = userQuiz.Quiz;
            decimal totalScore = 0;
            int correctAnswers = 0;
            int wrongAnswers = 0;

            foreach (var answer in submitQuizDto.Answers)
            {
                var question = quiz.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                if (question == null) continue;

                var correctOption = question.Options.FirstOrDefault(o => o.IsCorrect);
                var isCorrect = answer.SelectedOptionId.HasValue &&
                               answer.SelectedOptionId == correctOption?.Id;

                decimal pointsAwarded = 0;
                if (isCorrect)
                {
                    pointsAwarded = question.Points;
                    correctAnswers++;
                }
                else if (answer.SelectedOptionId.HasValue && quiz.EnableNegativeMarking)
                {
                    pointsAwarded = -(question.Points * quiz.NegativeMarkingPercentage / 100);
                    wrongAnswers++;
                }
                else if (answer.SelectedOptionId.HasValue)
                {
                    wrongAnswers++;
                }

                totalScore += pointsAwarded;

                var userAnswer = new UserAnswer
                {
                    UserQuizId = userQuiz.Id,
                    QuestionId = answer.QuestionId,
                    SelectedOptionId = answer.SelectedOptionId,
                    IsCorrect = isCorrect,
                    PointsAwarded = pointsAwarded,
                    AnsweredAt = DateTime.UtcNow
                };

                _context.UserAnswers.Add(userAnswer);
            }

            // Calculate final result
            decimal maxPossibleScore = quiz.Questions.Sum(q => q.Points);
            decimal percentageScore = maxPossibleScore > 0 ? (totalScore / maxPossibleScore) * 100 : 0;
            percentageScore = Math.Max(0, percentageScore);

            userQuiz.Score = percentageScore;
            userQuiz.CorrectAnswers = correctAnswers;
            userQuiz.WrongAnswers = wrongAnswers;
            userQuiz.UnansweredQuestions = userQuiz.TotalQuestions - correctAnswers - wrongAnswers;
            userQuiz.CompletedAt = DateTime.UtcNow;
            userQuiz.IsCompleted = true;

            await _context.SaveChangesAsync();

            var result = new QuizResultDto
            {
                UserQuizId = userQuiz.Id,
                QuizTitle = quiz.Title,
                Score = percentageScore,
                PassingScore = quiz.PassingScore,
                Passed = percentageScore >= quiz.PassingScore,
                TotalQuestions = userQuiz.TotalQuestions,
                CorrectAnswers = correctAnswers,
                WrongAnswers = wrongAnswers,
                UnansweredQuestions = userQuiz.UnansweredQuestions,
                CompletedAt = userQuiz.CompletedAt.Value,
                QuestionResults = new List<QuestionResultDto>()
            };

            var userAnswers = await _context.UserAnswers
                .Include(ua => ua.Question)
                    .ThenInclude(q => q.Options)
                .Include(ua => ua.SelectedOption)
                .Where(ua => ua.UserQuizId == userQuiz.Id)
                .ToListAsync();

            foreach (var ua in userAnswers)
            {
                var correctOption = ua.Question.Options.FirstOrDefault(o => o.IsCorrect);
                result.QuestionResults.Add(new QuestionResultDto
                {
                    QuestionId = ua.QuestionId,
                    QuestionText = ua.Question.Text,
                    Points = ua.Question.Points,
                    PointsAwarded = ua.PointsAwarded,
                    IsCorrect = ua.IsCorrect,
                    UserAnswer = ua.SelectedOption?.Text ?? "Not answered",
                    CorrectAnswer = correctOption?.Text ?? "N/A"
                });
            }

            return result;
        }
    }
}
