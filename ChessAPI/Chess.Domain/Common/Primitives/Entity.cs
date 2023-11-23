namespace Chess.Domain;

public class Entity
{
    public Guid Id { get; set; }
    public Guid CreatedById { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Guid? DeletedById { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}