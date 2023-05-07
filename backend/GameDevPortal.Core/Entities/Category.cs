using GameDevPortal.Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.Core.Entities;

public class Category : Entity
{
    [MaxLength(50)]
    public string Name { get; private set; }

    [MaxLength(7)]
    public string HexColour { get; private set; }

    public Guid? ProjectId { get; private set; }
    public IReadOnlyList<ProgressReport> ProgressReports => _progressReports.AsReadOnly();

    private readonly List<ProgressReport> _progressReports = new();

    public Category(string name, string hexColour, Guid projectId)
    {
        SetChangableValues(name, hexColour);
        ProjectId = projectId;
    }

    public void SetChangableValues(string name, string hexColour)
    {
        name.ThrowIfEmptyOrNull(nameof(name));
        Name = name;

        hexColour.ThrowIfNotHex(nameof(hexColour));
        HexColour = hexColour;
    }

    private Category()
    { }
}