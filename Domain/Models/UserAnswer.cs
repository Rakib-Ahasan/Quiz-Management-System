using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserAnswer
    {
        public int Id { get; set; }
        public int UserQuizId { get; set; }
        public int QuestionId { get; set; }
        public int? SelectedOptionId { get; set; }
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
        public bool IsCorrect { get; set; }
        public decimal PointsAwarded { get; set; }

        // Navigation properties
        public UserQuiz UserQuiz { get; set; } = null!;
        public Question Question { get; set; } = null!;
        public Option? SelectedOption { get; set; }
    }
}
