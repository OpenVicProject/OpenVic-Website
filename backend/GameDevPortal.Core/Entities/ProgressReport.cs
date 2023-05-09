using GameDevPortal.Core.Extensions;
using System.ComponentModel.DataAnnotations;

namespace GameDevPortal.Core.Entities;

public class ProgressReport : Entity
{
    [MaxLength(150)]
    public string Title { get; private set; }

    [MaxLength(500)]
    public string Description { get; private set; }

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyList<Guid> CategoryIds => _categoryIds.AsReadOnly();
    public IReadOnlyList<byte[]> Files => _files.AsReadOnly();
    public IReadOnlyList<Guid> FileIds => _fileIds.AsReadOnly();
    public DateTime MadePublicAt { get; private set; }

    public Project? Project { get; private set; }
    public Guid ProjectId { get; private set; }

    private readonly List<Category> _categories = new List<Category>();
    private readonly List<Guid> _categoryIds = new List<Guid>();
    private readonly List<byte[]> _files = new List<byte[]>();
    private readonly List<Guid> _fileIds = new List<Guid>();

    public ProgressReport(string title, string description, DateTime madePublicAt, Guid projectId, IEnumerable<Guid> categoryIds)
    {
        SetChangableValues(title, description, madePublicAt, categoryIds);
        ProjectId = projectId;
    }

    public void SetChangableValues(string title, string description, DateTime madePublicAt, IEnumerable<Guid> categoryIds)
    {
        title.ThrowIfEmptyOrNull(nameof(title));
        Title = title;

        description.ThrowIfEmptyOrNull(nameof(description));
        Description = description;

        MadePublicAt = madePublicAt;

        _categoryIds.Clear();
        foreach (Guid categoryId in categoryIds)
        {
            _categoryIds.Add(categoryId);
        }
    }

    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }

    public void AddCategories(IEnumerable<Category> categories)
    {
        _categories.AddRange(categories);
    }

    public void ClearCategories()
    {
        _categories.Clear();
    }

    public void SyncCategoryIdsToCategories()
    {
        _categoryIds.Clear();
        _categories.ForEach(c => _categoryIds.Add(c.Id));
    }

    private ProgressReport()
    { }
}