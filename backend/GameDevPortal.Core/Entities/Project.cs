using GameDevPortal.Core.Extensions;
using GameDevPortal.Core.ValueTypes;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.Core.Entities;

public class Project : Entity
{
    [MaxLength(100)]
    public string Name { get; private set; }

    [MaxLength(500)]
    public string Description { get; private set; }

    public ProjectTimeFrame TimeFrame { get; private set; }

    public IReadOnlyList<ProgressReport> ProgressReports => _progressReports.AsReadOnly();
    public IReadOnlyList<TeamMember> TeamMembers => _teamMembers.AsReadOnly();
    public IReadOnlyList<Faq> Faq => _faq.AsReadOnly();
    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    private readonly List<ProgressReport> _progressReports = new();
    private readonly List<TeamMember> _teamMembers = new();
    private readonly List<Faq> _faq = new();
    private readonly List<Category> _categories = new();

    public Project(string name, string description, ProjectTimeFrame timeFrame)
    {
        SetChangableValues(name, description, timeFrame);
    }

    public void SetChangableValues(string name, string description, ProjectTimeFrame timeFrame)
    {
        name.ThrowIfEmptyOrNull(nameof(name));
        Name = name.Trim();

        description.ThrowIfEmptyOrNull(nameof(description));
        Description = description.Trim();

        TimeFrame = timeFrame;
    }

    private Project()
    {
    }
}