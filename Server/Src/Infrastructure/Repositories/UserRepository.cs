using Application.Abstractions.Repositories;
using Domain.Models;
using Infrastructure.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(ElomiaContext context) : base(context) {}


    public async Task<bool> ExistsAsync(Guid userId, CancellationToken ct = default)
    {
        return await _dbSet.AnyAsync(x => x.Id == userId && !x.IsDeleted, ct);
    }
}