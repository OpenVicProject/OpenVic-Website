using GameDevPortal.Core.Models;
using GameDevPortal.Core.Models.Notifications;

namespace GameDevPortal.Core.Interfaces.Notifications;

public interface INotificationService
{
    Task<OperationResult> Send(Notification notification);
}