using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ChatMessageRepository : BaseRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(ElomiaContext context) : base(context) { }
    
    public async Task AddAsync(ChatMessage message, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(message, ct);
    }

    public async Task<IReadOnlyList<ChatMessage>> GetRecentByUserIdAsync(Guid userId, int take, CancellationToken ct = default)
    {
        var items = await _dbSet
            .AsNoTracking()
            .Where(x => x.UserId == userId && !x.IsDeleted)
            .OrderByDescending(x => x.CreatedAt)
            .Take(take)
            .ToListAsync(ct);

        return items
            .OrderBy(x => x.CreatedAt)
            .ToList();
    }
}