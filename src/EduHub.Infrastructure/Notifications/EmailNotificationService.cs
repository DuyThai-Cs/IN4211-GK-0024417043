using EduHub.Application.Notifications;

namespace EduHub.Infrastructure.Notifications;

public class EmailNotificationService : INotificationService
{
    public string Channel => "email";
    public void Send(string message) => Console.WriteLine($"[EMAIL] {message}");
}