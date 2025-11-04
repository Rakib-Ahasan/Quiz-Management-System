using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Handlers.QuizDTOs
{
    public class QuizListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public int QuestionCount { get; set; }
        public decimal PassingScore { get; set; }
        public bool HasCompleted { get; set; }
    }

    public class QuizDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public decimal PassingScore { get; set; }
        public bool EnableNegativeMarking { get; set; }
        public decimal NegativeMarkingPercentage { get; set; }
        public List<QuestionDto> Questions { get; set; } = new();
    }

    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int QuestionType { get; set; }
        public decimal Points { get; set; }
        public int Order { get; set; }
        public List<OptionDto> Options { get; set; } = new();
    }

    public class OptionDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }
    }

    public class UserQuizDto
    {
        public int UserQuizId { get; set; }
        public int QuizId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class SubmitQuizDto
    {
        public int UserQuizId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public List<AnswerDto> Answers { get; set; } = new();
    }

    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
    }

    public class QuizResultDto
    {
        public int UserQuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal PassingScore { get; set; }
        public bool Passed { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int UnansweredQuestions { get; set; }
        public DateTime CompletedAt { get; set; }
        public List<QuestionResultDto> QuestionResults { get; set; } = new();
    }

    public class QuestionResultDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public decimal Points { get; set; }
        public decimal PointsAwarded { get; set; }
        public bool IsCorrect { get; set; }
        public string UserAnswer { get; set; } = string.Empty;
        public string CorrectAnswer { get; set; } = string.Empty;
    }

    public class UserQuizHistoryDto
    {
        public int UserQuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;
        public decimal Score { get; set; }
        public decimal PassingScore { get; set; }
        public bool Passed { get; set; }
        public DateTime CompletedAt { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
    }
}
