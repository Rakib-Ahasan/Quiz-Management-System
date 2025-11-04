using Domain.Handlers.QuizDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Handlers.QuizHandler
{
    public interface IQuizHandler
    {
        Task<IEnumerable<QuizListDto>> GetAllActiveQuizzesAsync();
        Task<QuizDetailDto?> GetQuizByIdAsync(int quizId);
        Task<UserQuizDto?> StartQuizAsync(int quizId, string userId);
        Task<bool> HasUserCompletedQuizAsync(int quizId, string userId);
        Task<QuizResultDto> SubmitQuizAsync(SubmitQuizDto submitQuizDto);
    }
}
