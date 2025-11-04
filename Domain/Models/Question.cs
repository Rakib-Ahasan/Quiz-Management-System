using Microsoft.CodeAnalysis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int QuizId { get; set; }
        public string Text { get; set; } = string.Empty;
        public QuestionType QuestionType { get; set; } = QuestionType.MultipleChoice;
        public decimal Points { get; set; } = 1;
        public int Order { get; set; }

        // Navigation properties
        public Quiz Quiz { get; set; } = null!;
        public ICollection<Option> Options { get; set; } = new List<Option>();
        public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }

    public enum QuestionType
    {
        MultipleChoice = 0,
        TrueFalse = 1,
        ShortAnswer = 2
    }
}
