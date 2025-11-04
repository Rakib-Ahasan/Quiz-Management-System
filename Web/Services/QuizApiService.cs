using Domain.Handlers;
using Domain.Handlers.QuizDTOs;
using System.Net.Http.Json;
using System.Text.Json;

namespace Web.Services;

public class QuizApiService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public QuizApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<List<QuizListDto>> GetAllQuizzesAsync()
    {
        var response = await _httpClient.GetAsync("/api/quizzes");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<QuizListDto>>(_jsonOptions) ?? new();
    }

    public async Task<QuizDetailDto?> GetQuizByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"/api/quizzes/{id}");
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<QuizDetailDto>(_jsonOptions);
    }

    public async Task<UserQuizDto?> StartQuizAsync(int quizId)
    {
        var response = await _httpClient.PostAsync($"/api/quizzes/{quizId}/start", null);
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<UserQuizDto>(_jsonOptions);
    }

    public async Task<QuizResultDto?> SubmitQuizAsync(SubmitQuizDto submitQuizDto)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/quizzes/submit", submitQuizDto);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<QuizResultDto>(_jsonOptions);
    }

    public async Task<QuizResultDto?> GetResultAsync(int userQuizId)
    {
        var response = await _httpClient.GetAsync($"/api/results/{userQuizId}");
        if (!response.IsSuccessStatusCode)
            return null;
        return await response.Content.ReadFromJsonAsync<QuizResultDto>(_jsonOptions);
    }

    public async Task<List<UserQuizHistoryDto>> GetHistoryAsync()
    {
        var response = await _httpClient.GetAsync("/api/results/history");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<UserQuizHistoryDto>>(_jsonOptions) ?? new();
    }
}