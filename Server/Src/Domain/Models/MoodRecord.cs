using Domain.Models.Base;

namespace Domain.Models;

public class MoodRecord : BaseModel
{
    public required string Name { get; set; } 
    public int Intensity { get; set; } 
    public string? Reason { get; set; }

    public string? StickerFileId { get; set; }
    public string? Emoji { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}