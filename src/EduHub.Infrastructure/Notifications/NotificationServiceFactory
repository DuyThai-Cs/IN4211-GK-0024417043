using EduHub.Application.Notifications;

namespace EduHub.Infrastructure.Notifications;

public class NotificationServiceFactory : INotificationServiceFactory
{
    private readonly IEnumerable<INotificationService> _services;

    public NotificationServiceFactory(IEnumerable<INotificationService> services)
    {
        _services = services;
    }

    public INotificationService Create(string channel)
    {
        return _services.FirstOrDefault(s =>
                   string.Equals(s.Channel, channel, StringComparison.OrdinalIgnoreCase))
               ?? throw new ArgumentException($"Kênh thông báo không hỗ trợ: {channel}", nameof(channel));
    }
}