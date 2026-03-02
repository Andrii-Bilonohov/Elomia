using Domain.Models.Base;

namespace Domain.Models;

public class User : BaseModel
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }

    public ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();
    public ICollection<MoodRecord> Emotions { get; set; } = new List<MoodRecord>();
    public ICollection<Entry> Entries { get; set; } = new List<Entry>();
}