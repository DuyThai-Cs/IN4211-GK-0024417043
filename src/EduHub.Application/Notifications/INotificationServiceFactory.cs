namespace EduHub.Application.Notifications;

public interface INotificationServiceFactory
{
    INotificationService Create(string channel);
}