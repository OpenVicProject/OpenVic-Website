using GameDevPortal.Core.Models;
using SendGrid.Helpers.Mail;
using SendGrid;
using Microsoft.Extensions.Options;
using GameDevPortal.Infrastructure.Notifications.Configuration;
using GameDevPortal.Core.Models.Notifications;
using GameDevPortal.Core.Interfaces.Notifications;

namespace GameDevPortal.Infrastructure.Notifications.Services
{
    public class EmailNotificationService : INotificationService
    {
        private readonly IOptionsMonitor<SendGridConfiguration> _sendgridConfig;

        public EmailNotificationService(IOptionsMonitor<SendGridConfiguration> sendgridConfig)
        {
            _sendgridConfig = sendgridConfig ?? throw new ArgumentNullException(nameof(sendgridConfig));
        }

        async Task<OperationResult> INotificationService.Send(Notification notification)
        {
            string apiKey = _sendgridConfig.CurrentValue.ApiKey;
            SendGridClient client = new(apiKey);
            EmailAddress from = new(_sendgridConfig.CurrentValue.SenderEmail, _sendgridConfig.CurrentValue.SenderName);
            string subject = notification.Title;
            string plainTextContent = notification.Body;

            List<EmailAddress> recipientEmailAddresses = new();
            foreach (string recipient in notification.Recipients)
            {
                recipientEmailAddresses.Add(new EmailAddress(recipient));
            }

            SendGridMessage msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipientEmailAddresses, subject, plainTextContent, string.Empty);

            Response response = await client.SendEmailAsync(msg);

            return response.IsSuccessStatusCode ? OperationResult.CreateSuccessResult() : OperationResult.CreateFailure(new Exception(response.Headers.ToString()));
        }
    }
}