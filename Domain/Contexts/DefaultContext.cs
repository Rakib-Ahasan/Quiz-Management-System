using Domain.Aggregates;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using RapidFireLib.Lib.Core;
using RapidFireLib.Models;
using System;

namespace Domain.Contexts
{
    public class DefaultContext : RFCoreDbContext
    {
        public DefaultContext() : base("DefaultConnection", contextType: ContextType.PGSQL) { }
        public DefaultContext(SAASType sAASType = SAASType.NoSaas) : base("DefaultConnection", sAASType, ContextType.PGSQL) { }

        public DbSet<DataVerificationLog> DataVerificationLog { get; set; }
        public DbSet<UserGeo> UserGeo { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Upazila> Upazila { get; set; }
        public DbSet<Unions> Unions { get; set; }
        public DbSet<Village> Village { get; set; }

        //Quiz Module
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<UserQuiz> UserQuizzes { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Quiz configuration
            modelBuilder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.PassingScore).HasPrecision(5, 2);
                entity.Property(e => e.NegativeMarkingPercentage).HasPrecision(5, 2);
            });

            // Question configuration
            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.Points).HasPrecision(5, 2);

                entity.HasOne(e => e.Quiz)
                    .WithMany(q => q.Questions)
                    .HasForeignKey(e => e.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Option configuration
            modelBuilder.Entity<Option>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Text).IsRequired().HasMaxLength(500);

                entity.HasOne(e => e.Question)
                    .WithMany(q => q.Options)
                    .HasForeignKey(e => e.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // UserQuiz configuration
            modelBuilder.Entity<UserQuiz>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Score).HasPrecision(5, 2);

                entity.HasOne(e => e.Quiz)
                    .WithMany(q => q.UserQuizzes)
                    .HasForeignKey(e => e.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasIndex(e => new { e.UserId, e.QuizId });
            });

            // UserAnswer configuration
            modelBuilder.Entity<UserAnswer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PointsAwarded).HasPrecision(5, 2);

                entity.HasOne(e => e.UserQuiz)
                    .WithMany(uq => uq.UserAnswers)
                    .HasForeignKey(e => e.UserQuizId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Question)
                    .WithMany(q => q.UserAnswers)
                    .HasForeignKey(e => e.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.SelectedOption)
                    .WithMany(o => o.UserAnswers)
                    .HasForeignKey(e => e.SelectedOptionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Quizzes
            modelBuilder.Entity<Quiz>().HasData(
                new Quiz
                {
                    Id = 1,
                    Title = "C# Fundamentals Quiz",
                    Description = "Test your knowledge of C# basics",
                    DurationInMinutes = 30,
                    PassingScore = 70,
                    EnableNegativeMarking = false,
                    RandomizeQuestions = true,
                    RandomizeOptions = true,
                    CreatedAt = new DateTime(2025, 11, 1, 0, 0, 0, DateTimeKind.Utc), // ✅ static
                },
                new Quiz
                {
                    Id = 2,
                    Title = "ASP.NET Core Advanced Quiz",
                    Description = "Advanced concepts in ASP.NET Core",
                    DurationInMinutes = 45,
                    PassingScore = 75,
                    EnableNegativeMarking = true,
                    NegativeMarkingPercentage = 25,
                    RandomizeQuestions = true,
                    RandomizeOptions = true,
                    CreatedAt = new DateTime(2025, 11, 1, 0, 0, 0, DateTimeKind.Utc), // ✅ static
                }
            );

            // Seed Questions for Quiz 1
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 1, QuizId = 1, Text = "What is the default access modifier for a class in C#?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 1 },
                new Question { Id = 2, QuizId = 1, Text = "Which keyword is used to inherit a class in C#?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 2 },
                new Question { Id = 3, QuizId = 1, Text = "What does 'var' keyword do in C#?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 3 },
                new Question { Id = 4, QuizId = 1, Text = "Is C# a case-sensitive language?", QuestionType = QuestionType.MultipleChoice, Points = 1, Order = 4 },
                new Question { Id = 5, QuizId = 1, Text = "Which of the following is a value type in C#?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 5 }
            );

            // Seed Questions for Quiz 2
            modelBuilder.Entity<Question>().HasData(
                new Question { Id = 6, QuizId = 2, Text = "What is middleware in ASP.NET Core?", QuestionType = QuestionType.MultipleChoice, Points = 3, Order = 1 },
                new Question { Id = 7, QuizId = 2, Text = "Which method is used to configure services in ASP.NET Core?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 2 },
                new Question { Id = 8, QuizId = 2, Text = "What is dependency injection?", QuestionType = QuestionType.MultipleChoice, Points = 3, Order = 3 },
                new Question { Id = 9, QuizId = 2, Text = "What is the purpose of the Configure method in Startup.cs?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 4 },
                new Question { Id = 10, QuizId = 2, Text = "Which attribute is used for model validation in ASP.NET Core?", QuestionType = QuestionType.MultipleChoice, Points = 2, Order = 5 }
            );

            // Seed Options for Quiz 1
            modelBuilder.Entity<Option>().HasData(
                // Question 1 options
                new Option { Id = 1, QuestionId = 1, Text = "public", IsCorrect = false, Order = 1 },
                new Option { Id = 2, QuestionId = 1, Text = "private", IsCorrect = false, Order = 2 },
                new Option { Id = 3, QuestionId = 1, Text = "internal", IsCorrect = true, Order = 3 },
                new Option { Id = 4, QuestionId = 1, Text = "protected", IsCorrect = false, Order = 4 },

                // Question 2 options
                new Option { Id = 5, QuestionId = 2, Text = "inherits", IsCorrect = false, Order = 1 },
                new Option { Id = 6, QuestionId = 2, Text = "extends", IsCorrect = false, Order = 2 },
                new Option { Id = 7, QuestionId = 2, Text = ":", IsCorrect = true, Order = 3 },
                new Option { Id = 8, QuestionId = 2, Text = "base", IsCorrect = false, Order = 4 },

                // Question 3 options
                new Option { Id = 9, QuestionId = 3, Text = "Declares a variable without type", IsCorrect = false, Order = 1 },
                new Option { Id = 10, QuestionId = 3, Text = "Implicitly types a variable", IsCorrect = true, Order = 2 },
                new Option { Id = 11, QuestionId = 3, Text = "Creates a dynamic type", IsCorrect = false, Order = 3 },
                new Option { Id = 12, QuestionId = 3, Text = "Declares a variant type", IsCorrect = false, Order = 4 },

                // Question 4 options
                new Option { Id = 13, QuestionId = 4, Text = "Yes", IsCorrect = true, Order = 1 },
                new Option { Id = 14, QuestionId = 4, Text = "No", IsCorrect = false, Order = 2 },
                new Option { Id = 15, QuestionId = 4, Text = "Only for keywords", IsCorrect = false, Order = 3 },
                new Option { Id = 16, QuestionId = 4, Text = "Only for variables", IsCorrect = false, Order = 4 },

                // Question 5 options
                new Option { Id = 17, QuestionId = 5, Text = "string", IsCorrect = false, Order = 1 },
                new Option { Id = 18, QuestionId = 5, Text = "int", IsCorrect = true, Order = 2 },
                new Option { Id = 19, QuestionId = 5, Text = "object", IsCorrect = false, Order = 3 },
                new Option { Id = 20, QuestionId = 5, Text = "dynamic", IsCorrect = false, Order = 4 }
            );

            // Seed Options for Quiz 2
            modelBuilder.Entity<Option>().HasData(
                // Question 6 options
                new Option { Id = 21, QuestionId = 6, Text = "A design pattern", IsCorrect = false, Order = 1 },
                new Option { Id = 22, QuestionId = 6, Text = "Software that processes HTTP requests", IsCorrect = true, Order = 2 },
                new Option { Id = 23, QuestionId = 6, Text = "A database layer", IsCorrect = false, Order = 3 },
                new Option { Id = 24, QuestionId = 6, Text = "A routing mechanism", IsCorrect = false, Order = 4 },

                // Question 7 options
                new Option { Id = 25, QuestionId = 7, Text = "Configure", IsCorrect = false, Order = 1 },
                new Option { Id = 26, QuestionId = 7, Text = "ConfigureServices", IsCorrect = true, Order = 2 },
                new Option { Id = 27, QuestionId = 7, Text = "AddServices", IsCorrect = false, Order = 3 },
                new Option { Id = 28, QuestionId = 7, Text = "SetupServices", IsCorrect = false, Order = 4 },

                // Question 8 options
                new Option { Id = 29, QuestionId = 8, Text = "A caching mechanism", IsCorrect = false, Order = 1 },
                new Option { Id = 30, QuestionId = 8, Text = "A design pattern for providing dependencies", IsCorrect = true, Order = 2 },
                new Option { Id = 31, QuestionId = 8, Text = "A database connection method", IsCorrect = false, Order = 3 },
                new Option { Id = 32, QuestionId = 8, Text = "A routing technique", IsCorrect = false, Order = 4 },

                // Question 9 options
                new Option { Id = 33, QuestionId = 9, Text = "To configure services", IsCorrect = false, Order = 1 },
                new Option { Id = 34, QuestionId = 9, Text = "To configure the HTTP request pipeline", IsCorrect = true, Order = 2 },
                new Option { Id = 35, QuestionId = 9, Text = "To configure the database", IsCorrect = false, Order = 3 },
                new Option { Id = 36, QuestionId = 9, Text = "To configure logging", IsCorrect = false, Order = 4 },

                // Question 10 options
                new Option { Id = 37, QuestionId = 10, Text = "[Validate]", IsCorrect = false, Order = 1 },
                new Option { Id = 38, QuestionId = 10, Text = "[Required]", IsCorrect = true, Order = 2 },
                new Option { Id = 39, QuestionId = 10, Text = "[Check]", IsCorrect = false, Order = 3 },
                new Option { Id = 40, QuestionId = 10, Text = "[Mandatory]", IsCorrect = false, Order = 4 }
            );
        }
    }
}
