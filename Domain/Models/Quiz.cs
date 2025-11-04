using DocumentFormat.OpenXml.Office.SpreadSheetML.Y2023.MsForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public decimal PassingScore { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EnableNegativeMarking { get; set; } = false;
        public decimal NegativeMarkingPercentage { get; set; } = 0;
        public bool RandomizeQuestions { get; set; } = true;
        public bool RandomizeOptions { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();
    }
}
