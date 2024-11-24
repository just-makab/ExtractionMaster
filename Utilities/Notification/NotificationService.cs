using EM_WebApp.Utilities.Notification;
using System.Net.Mail;
using System.Net;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

/***************************************************************************************
*    Title: Programmable Messaging for WhatsApp and C#/.Net Quickstart
*    Author: Twilio
*   URL: https://www.twilio.com/docs/whatsapp/quickstart/csharp
***************************************************************************************/

/***************************************************************************************
*    Title: How to Use Twilio WhatsApp Sandbox to Send and Receive Messages Using Twilio WhatsApp Number?
*    Author: Outright Systems
*   URL:https://www.youtube.com/watch?v=zBceQa8S91o
***************************************************************************************/


//System for Proccessing and Sending Emails and WhatsApp Messages
public class NotificationService : INotificationService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IConfiguration configuration, ILogger<NotificationService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string receiverEmail, string subject, string body)
    {
        try
        {
            //Variables can be found within appsettings.json, Just add yout own variables to use the system
            var senderEmail = _configuration["EmailSettings:SenderEmail"];
            var emailPassword = _configuration["EmailSettings:EmailPassword"];
            var smtpServer = _configuration["EmailSettings:SmtpServer"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);

            using var client = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(senderEmail, emailPassword)
            };

            using var message = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body,
            };

            message.To.Add(receiverEmail);

            await client.SendMailAsync(message);

            _logger.LogInformation("Email sent successfully to {ReceiverEmail}", receiverEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send email to {ReceiverEmail}", receiverEmail);
            throw;
        }
    }

    public async Task SendWhatsAppMessageAsync(string phoneNumber, string message)
    {
        try
        {
            var accountSid = _configuration["Twilio:AccountSid"];
            var authToken = _configuration["Twilio:AuthToken"];
            var fromWhatsAppNumber = _configuration["Twilio:WhatsAppFrom"];

            TwilioClient.Init(accountSid, authToken);

            var messageResource = await MessageResource.CreateAsync(
                body: message,
                from: new Twilio.Types.PhoneNumber($"whatsapp:{fromWhatsAppNumber}"),
                to: new Twilio.Types.PhoneNumber($"whatsapp:{phoneNumber}")
            );

            _logger.LogInformation("WhatsApp message sent to {PhoneNumber}: {Sid}", phoneNumber, messageResource.Sid);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to send WhatsApp message to {PhoneNumber}", phoneNumber);
            throw;
        }
    }
}
