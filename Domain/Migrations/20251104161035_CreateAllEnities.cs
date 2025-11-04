using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllEnities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    PassingScore = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EnableNegativeMarking = table.Column<bool>(type: "boolean", nullable: false),
                    NegativeMarkingPercentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    RandomizeQuestions = table.Column<bool>(type: "boolean", nullable: false),
                    RandomizeOptions = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quizzes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuizId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    QuestionType = table.Column<int>(type: "integer", nullable: false),
                    Points = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserQuizzes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    QuizId = table.Column<int>(type: "integer", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Score = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    TotalQuestions = table.Column<int>(type: "integer", nullable: false),
                    CorrectAnswers = table.Column<int>(type: "integer", nullable: false),
                    WrongAnswers = table.Column<int>(type: "integer", nullable: false),
                    UnansweredQuestions = table.Column<int>(type: "integer", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserQuizzes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserQuizzes_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Options_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserQuizId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    SelectedOptionId = table.Column<int>(type: "integer", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    PointsAwarded = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Options_SelectedOptionId",
                        column: x => x.SelectedOptionId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserAnswers_UserQuizzes_UserQuizId",
                        column: x => x.UserQuizId,
                        principalTable: "UserQuizzes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Quizzes",
                columns: new[] { "Id", "CreatedAt", "Description", "DurationInMinutes", "EnableNegativeMarking", "IsActive", "NegativeMarkingPercentage", "PassingScore", "RandomizeOptions", "RandomizeQuestions", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 4, 16, 10, 31, 945, DateTimeKind.Utc).AddTicks(1370), "Test your knowledge of C# basics", 30, false, true, 0m, 70m, true, true, "C# Fundamentals Quiz" },
                    { 2, new DateTime(2025, 11, 4, 16, 10, 31, 945, DateTimeKind.Utc).AddTicks(1892), "Advanced concepts in ASP.NET Core", 45, true, true, 25m, 75m, true, true, "ASP.NET Core Advanced Quiz" }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Order", "Points", "QuestionType", "QuizId", "Text" },
                values: new object[,]
                {
                    { 1, 1, 2m, 0, 1, "What is the default access modifier for a class in C#?" },
                    { 2, 2, 2m, 0, 1, "Which keyword is used to inherit a class in C#?" },
                    { 3, 3, 2m, 0, 1, "What does 'var' keyword do in C#?" },
                    { 4, 4, 1m, 0, 1, "Is C# a case-sensitive language?" },
                    { 5, 5, 2m, 0, 1, "Which of the following is a value type in C#?" },
                    { 6, 1, 3m, 0, 2, "What is middleware in ASP.NET Core?" },
                    { 7, 2, 2m, 0, 2, "Which method is used to configure services in ASP.NET Core?" },
                    { 8, 3, 3m, 0, 2, "What is dependency injection?" },
                    { 9, 4, 2m, 0, 2, "What is the purpose of the Configure method in Startup.cs?" },
                    { 10, 5, 2m, 0, 2, "Which attribute is used for model validation in ASP.NET Core?" }
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "IsCorrect", "Order", "QuestionId", "Text" },
                values: new object[,]
                {
                    { 1, false, 1, 1, "public" },
                    { 2, false, 2, 1, "private" },
                    { 3, true, 3, 1, "internal" },
                    { 4, false, 4, 1, "protected" },
                    { 5, false, 1, 2, "inherits" },
                    { 6, false, 2, 2, "extends" },
                    { 7, true, 3, 2, ":" },
                    { 8, false, 4, 2, "base" },
                    { 9, false, 1, 3, "Declares a variable without type" },
                    { 10, true, 2, 3, "Implicitly types a variable" },
                    { 11, false, 3, 3, "Creates a dynamic type" },
                    { 12, false, 4, 3, "Declares a variant type" },
                    { 13, true, 1, 4, "Yes" },
                    { 14, false, 2, 4, "No" },
                    { 15, false, 3, 4, "Only for keywords" },
                    { 16, false, 4, 4, "Only for variables" },
                    { 17, false, 1, 5, "string" },
                    { 18, true, 2, 5, "int" },
                    { 19, false, 3, 5, "object" },
                    { 20, false, 4, 5, "dynamic" },
                    { 21, false, 1, 6, "A design pattern" },
                    { 22, true, 2, 6, "Software that processes HTTP requests" },
                    { 23, false, 3, 6, "A database layer" },
                    { 24, false, 4, 6, "A routing mechanism" },
                    { 25, false, 1, 7, "Configure" },
                    { 26, true, 2, 7, "ConfigureServices" },
                    { 27, false, 3, 7, "AddServices" },
                    { 28, false, 4, 7, "SetupServices" },
                    { 29, false, 1, 8, "A caching mechanism" },
                    { 30, true, 2, 8, "A design pattern for providing dependencies" },
                    { 31, false, 3, 8, "A database connection method" },
                    { 32, false, 4, 8, "A routing technique" },
                    { 33, false, 1, 9, "To configure services" },
                    { 34, true, 2, 9, "To configure the HTTP request pipeline" },
                    { 35, false, 3, 9, "To configure the database" },
                    { 36, false, 4, 9, "To configure logging" },
                    { 37, false, 1, 10, "[Validate]" },
                    { 38, true, 2, 10, "[Required]" },
                    { 39, false, 3, 10, "[Check]" },
                    { 40, false, 4, 10, "[Mandatory]" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Options_QuestionId",
                table: "Options",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizId",
                table: "Questions",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_SelectedOptionId",
                table: "UserAnswers",
                column: "SelectedOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserQuizId",
                table: "UserAnswers",
                column: "UserQuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizzes_QuizId",
                table: "UserQuizzes",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_UserQuizzes_UserId_QuizId",
                table: "UserQuizzes",
                columns: new[] { "UserId", "QuizId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswers");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "UserQuizzes");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");
        }
    }
}
