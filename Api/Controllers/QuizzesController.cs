using Domain.Handlers;
using Domain.Handlers.QuizDTOs;
using Domain.Handlers.QuizHandler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class QuizzesController : ControllerBase
{
    private readonly QuizHandler _quizHandler;
    private readonly UserManager<IdentityUser> _userManager;

    public QuizzesController(QuizHandler quizHandler, UserManager<IdentityUser> userManager)
    {
        _quizHandler = quizHandler;
        _userManager = userManager;
    }

    /// <summary>
    /// Get all active quizzes
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<QuizListDto>>> GetAllQuizzes()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var quizzes = await _quizHandler.GetAllActiveQuizzesAsync();

        var quizList = quizzes.ToList();
        foreach (var quiz in quizList)
        {
            quiz.HasCompleted = await _quizHandler.HasUserCompletedQuizAsync(quiz.Id, user.Id);
        }

        return Ok(quizList);
    }

    /// <summary>
    /// Get quiz details by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<QuizDetailDto>> GetQuizById(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var hasCompleted = await _quizHandler.HasUserCompletedQuizAsync(id, user.Id);
        if (hasCompleted)
            return BadRequest(new { message = "You have already completed this quiz" });

        var quiz = await _quizHandler.GetQuizByIdAsync(id);
        if (quiz == null)
            return NotFound();

        return Ok(quiz);
    }

    /// <summary>
    /// Start a quiz attempt
    /// </summary>
    [HttpPost("{id}/start")]
    public async Task<ActionResult<UserQuizDto>> StartQuiz(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        var userQuiz = await _quizHandler.StartQuizAsync(id, user.Id);
        if (userQuiz == null)
            return BadRequest(new { message = "Quiz not found or already completed" });

        return Ok(userQuiz);
    }

    /// <summary>
    /// Submit quiz answers
    /// </summary>
    [HttpPost("submit")]
    public async Task<ActionResult<QuizResultDto>> SubmitQuiz([FromBody] SubmitQuizDto submitQuizDto)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return Unauthorized();

        submitQuizDto.UserId = user.Id;

        try
        {
            var result = await _quizHandler.SubmitQuizAsync(submitQuizDto);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}