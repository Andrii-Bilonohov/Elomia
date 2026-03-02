namespace Application.Abstractions.Services.Ai;

public interface IAiService
{
    Task<string> GetAnswerAsync(string prompt, CancellationToken cancellationToken = default);
}