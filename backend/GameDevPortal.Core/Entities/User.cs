using GameDevPortal.Core.Extensions;
using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.ValueTypes;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace GameDevPortal.Core.Entities;

public class User : IdentityUser<Guid>, IEntity
{
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    public IReadOnlyList<TeamMember> TeamMemberships => _teamMemberships.AsReadOnly();

    private readonly List<TeamMember> _teamMemberships = new List<TeamMember>();

    public void SetChangableValues(string username)
    {
        username.ThrowIfEmptyOrNull(nameof(username));
        UserName = username.Trim();
    }

    protected User()
    {
        IsDeleted = false;
    }
}