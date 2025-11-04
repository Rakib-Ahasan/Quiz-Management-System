using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class UserQuiz
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int QuizId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public decimal Score { get; set; }
        public int TotalQuestions { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }
        public int UnansweredQuestions { get; set; }
        public bool IsCompleted { get; set; }

        // Navigation properties
        public Quiz Quiz { get; set; } = null!;
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }
}
