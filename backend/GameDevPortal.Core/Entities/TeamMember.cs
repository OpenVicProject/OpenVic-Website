using GameDevPortal.Core.Models;

namespace GameDevPortal.Core.Entities;

public class TeamMember : Entity
{
    public ProjectRole Role { get; private set; }

    public User? User { get; private set; }
    public Guid UserId { get; private set; }

    public Project? Project { get; private set; }
    public Guid ProjectId { get; private set; }

    private TeamMember() 
    { 
    }
}