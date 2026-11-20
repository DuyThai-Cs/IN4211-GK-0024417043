using EduHub.Application.Notifications;

namespace EduHub.Infrastructure.Notifications;

public class SmsNotificationService : INotificationService
{
    public string Channel => "sms";
    public void Send(string message) => Console.WriteLine($"[SMS] {message}");
}