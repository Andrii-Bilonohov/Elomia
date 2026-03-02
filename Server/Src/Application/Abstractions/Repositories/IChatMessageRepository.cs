using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IChatMessageRepository
{
    public void Add(ChatMessage chatMessage);
    Task<IReadOnlyList<ChatMessage>> GetRecentByUserIdAsync(Guid userId, int take, CancellationToken ct = default);
}