namespace GameDevPortal.Core.Interfaces;

public interface IEntity
{
    Guid Id { get; }
    DateTime CreatedAt { get; }
    DateTime UpdatedAt { get; }
    bool IsDeleted { get; }
}