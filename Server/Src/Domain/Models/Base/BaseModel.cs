namespace Domain.Models.Base;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    public void Delete()
    {
        IsDeleted = true;

        Update();
    }

    public void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}