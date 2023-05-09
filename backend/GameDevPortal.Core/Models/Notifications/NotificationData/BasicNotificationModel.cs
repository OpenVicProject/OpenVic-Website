using GameDevPortal.Core.Extensions;
using GameDevPortal.Core.Interfaces.Notifications;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace GameDevPortal.Core.Models.Notifications.NotificationData;

public class BasicNotificationModel : INotificationBodyGenerator
{
    private const string _templateFile = "basicTemplate.txt";
    public string ProjectTitle { get; private set; }
    public string ProjectDescription { get; private set; }

    public BasicNotificationModel(string projectTitle, string projectDescription)
    {
        projectTitle.ThrowIfEmptyOrNull(nameof(projectTitle));
        ProjectTitle = projectTitle;

        projectTitle.ThrowIfEmptyOrNull(nameof(projectTitle));
        ProjectDescription = projectDescription;
    }

    public string Generate(ITemplateProvider templateProvider)
    {
        string template = templateProvider.ReadTemplate(_templateFile);

        return template.FillTemplate(Title => ProjectTitle, Description => ProjectDescription);
    }
}