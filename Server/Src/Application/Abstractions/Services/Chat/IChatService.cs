using Application.Contracts;
using Application.Dtos.Requests.Chat;
using Application.Dtos.Responses.Chat;

namespace Application.Abstractions.Services.Chat;

public interface IChatService
{
    Task<Response<ChatResponseDto>> GetAnswerAsync(ChatRequestDto chatRequest, CancellationToken cancellationToken);
}