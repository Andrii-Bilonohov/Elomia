using Application.Abstractions.Services.Ai;
using Google.GenAI;

namespace Application.Services.Ai;

public class AiService : IAiService
{
    private readonly Client _client;

    public AiService()
    {
        var apiKey =
            Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ??
            Environment.GetEnvironmentVariable("GEMINI_API_KEY");

        if (string.IsNullOrWhiteSpace(apiKey))
            throw new InvalidOperationException("API key not found.");

        _client = new Client(apiKey: apiKey);
    }

    public async Task<string> GetAnswerAsync(string prompt, CancellationToken cancellationToken = default)
    {
        var response = await _client.Models.GenerateContentAsync(
            model: "gemini-3-flash-preview",
            contents: prompt,
            cancellationToken: cancellationToken);

        var text = response?.Candidates?
            .FirstOrDefault()?
            .Content?
            .Parts?
            .FirstOrDefault()?
            .Text;

        if (string.IsNullOrWhiteSpace(text))
            throw new InvalidOperationException("Empty response from AI.");

        return text;
    }
}