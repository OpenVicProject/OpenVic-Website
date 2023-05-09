namespace GameDevPortal.Core.Interfaces.Notifications;

public interface ITemplateProvider
{
    public abstract string ReadTemplate(string templateName);
}