using Domain.Models.Base;

namespace Domain.Models;

public class Entry : BaseModel
{
    public required string Message { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}