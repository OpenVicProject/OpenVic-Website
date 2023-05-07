using GameDevPortal.Core.Extensions;

namespace GameDevPortal.Core.Models.Notifications;

public class Notification
{
    public string Title { get; private set; }
    public string Body { get; private set; }
    public IReadOnlyList<string> Recipients => _recipients.AsReadOnly();

    private readonly List<string> _recipients = new();

    public Notification(string title, string body, IEnumerable<string> recipients)
    {
        title.ThrowIfEmptyOrNull(nameof(title));
        Title = title;

        body.ThrowIfEmptyOrNull(nameof(body));
        Body = body;

        recipients.ThrowIfEmptyOrNull(nameof(recipients));
        _recipients.AddRange(recipients);
    }
}