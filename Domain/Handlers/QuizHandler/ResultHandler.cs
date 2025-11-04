using Domain.Contexts;
using Domain.Handlers.QuizDTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Handlers.QuizHandler
{
    public class ResultHandler : IResultHandler
    {
        private readonly DefaultContext _context;

        public ResultHandler(DefaultContext context)
        {
            _context = context;
        }

        public async Task<QuizResultDto?> GetQuizResultAsync(int userQuizId, string userId)
        {
            var userQuiz = await _context.UserQuizzes
                .Include(uq => uq.Quiz)
                .Include(uq => uq.UserAnswers)
                    .ThenInclude(ua => ua.Question)
                        .ThenInclude(q => q.Options)
                .Include(uq => uq.UserAnswers)
                    .ThenInclude(ua => ua.SelectedOption)
                .FirstOrDefaultAsync(uq => uq.Id == userQuizId && uq.UserId == userId && uq.IsCompleted);

            if (userQuiz == null)
                return null;

            return new QuizResultDto
            {
                UserQuizId = userQuiz.Id,
                QuizTitle = userQuiz.Quiz.Title,
                Score = userQuiz.Score,
                PassingScore = userQuiz.Quiz.PassingScore,
                Passed = userQuiz.Score >= userQuiz.Quiz.PassingScore,
                TotalQuestions = userQuiz.TotalQuestions,
                CorrectAnswers = userQuiz.CorrectAnswers,
                WrongAnswers = userQuiz.WrongAnswers,
                UnansweredQuestions = userQuiz.UnansweredQuestions,
                CompletedAt = userQuiz.CompletedAt!.Value,
                QuestionResults = userQuiz.UserAnswers.Select(ua => new QuestionResultDto
                {
                    QuestionId = ua.QuestionId,
                    QuestionText = ua.Question.Text,
                    Points = ua.Question.Points,
                    PointsAwarded = ua.PointsAwarded,
                    IsCorrect = ua.IsCorrect,
                    UserAnswer = ua.SelectedOption?.Text ?? "Not answered",
                    CorrectAnswer = ua.Question.Options.FirstOrDefault(o => o.IsCorrect)?.Text ?? "N/A"
                }).ToList()
            };
        }

        public async Task<IEnumerable<UserQuizHistoryDto>> GetUserQuizHistoryAsync(string userId)
        {
            return await _context.UserQuizzes
                .Include(uq => uq.Quiz)
                .Where(uq => uq.UserId == userId && uq.IsCompleted)
                .OrderByDescending(uq => uq.CompletedAt)
                .Select(uq => new UserQuizHistoryDto
                {
                    UserQuizId = uq.Id,
                    QuizTitle = uq.Quiz.Title,
                    Score = uq.Score,
                    PassingScore = uq.Quiz.PassingScore,
                    Passed = uq.Score >= uq.Quiz.PassingScore,
                    CompletedAt = uq.CompletedAt!.Value,
                    TotalQuestions = uq.TotalQuestions,
                    CorrectAnswers = uq.CorrectAnswers
                })
                .ToListAsync();
        }
    }
}
