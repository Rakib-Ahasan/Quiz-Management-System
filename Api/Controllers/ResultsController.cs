using Domain.Handlers;
using Domain.Handlers.QuizDTOs;
using Domain.Handlers.QuizHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResultsController : ControllerBase
{
    private readonly ResultHandler _resultHandler;
    private readonly UserManager<IdentityUser> _userManager;

    public ResultsController(ResultHandler resultHandler, UserManager<IdentityUser> userManager)
    {
        _resultHandler = resultHandler;
        _userManager = userManager;
    }

    /// <summary>
    /// Get quiz result by UserQuiz ID
    /// </summary>
    [HttpGet("{userQuizId}")]
    public async Task<ActionResult<QuizResultDto>> GetResult(int userQuizId)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var result = await _resultHandler.GetQuizResultAsync(userQuizId, user.Id);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    /// <summary>
    /// Get user's quiz history
    /// </summary>
    [HttpGet("history")]
    public async Task<ActionResult<IEnumerable<UserQuizHistoryDto>>> GetHistory()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var history = await _resultHandler.GetUserQuizHistoryAsync(user.Id);
        return Ok(history);
    }
}