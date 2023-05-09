namespace GameDevPortal.Core.Entities;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
        IsDeleted = false;
    }
}