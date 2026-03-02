using Domain.Enums;
using Domain.Models.Base;

namespace Domain.Models;

public class ChatMessage : BaseModel
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public ChatRole Role { get; set; }
    public required string Text { get; set; }
}