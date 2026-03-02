namespace Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid userId, CancellationToken ct = default);
}