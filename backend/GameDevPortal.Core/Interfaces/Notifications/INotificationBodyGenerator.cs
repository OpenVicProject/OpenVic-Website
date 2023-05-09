using GameDevPortal.Core.Models.Notifications;
using System.Linq.Expressions;

namespace GameDevPortal.Core.Interfaces.Notifications;

public interface INotificationBodyGenerator
{
    string Generate(ITemplateProvider templateProvider);
}