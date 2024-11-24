namespace EM_WebApp.Utilities.Notification
{
    public interface INotificationService
    {
        Task SendEmailAsync(string receiverEmail, string subject, string body);
        Task SendWhatsAppMessageAsync(string phoneNumber, string message);

    }
}
