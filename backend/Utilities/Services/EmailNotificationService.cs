using GameDevPortal.Core.Interfaces;
using GameDevPortal.Core.Models;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Utilities.Services
{
    public class EmailNotificationService : INotificationService
    {
        async Task<OperationResult> INotificationService.Send(Notification notification)
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY")!;
            SendGridClient client = new(apiKey);
            EmailAddress from = new("y.rombouts@betabit.nl", "Youri Rombouts");
            string subject = notification.Title;
            string plainTextContent = notification.Body;

            List<EmailAddress> recipientEmailAddresses = new();
            foreach(string recipient in notification.Recipients)
            {
                recipientEmailAddresses.Add(new EmailAddress(recipient));
            }

            SendGridMessage msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, recipientEmailAddresses, subject, plainTextContent, "");

            Response response = await client.SendEmailAsync(msg);

            if (response.IsSuccessStatusCode)
            {
                return OperationResult.CreateSuccessResult();
            }
            else
            {
                return OperationResult.CreateFailure(new Exception(response.Headers.ToString()));
            }
        }
    }
}