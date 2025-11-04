using Domain.Handlers.QuizDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Handlers.QuizHandler
{
    public interface IResultHandler
    {
        Task<QuizResultDto?> GetQuizResultAsync(int userQuizId, string userId);
        Task<IEnumerable<UserQuizHistoryDto>> GetUserQuizHistoryAsync(string userId);
    }
}
