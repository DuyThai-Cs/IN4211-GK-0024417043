using EduHub.Application.Notifications;
using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Application.Events;

public class EventService
{
    private readonly IEventRepository _repository;
    private readonly INotificationServiceFactory _notificationFactory;

    public EventService(IEventRepository repository, INotificationServiceFactory notificationFactory)
    {
        _repository = repository;
        _notificationFactory = notificationFactory;
    }

    public Event CreateEvent(Guid clubId, string title, DateTime date, int capacity, string channel)
    {
        var ev = Event.Create(clubId, title, date, capacity);   // validate trong Domain
        var notifier = _notificationFactory.Create(channel);    // kênh sai thì lỗi trước khi lưu
        _repository.Add(ev);
        notifier.Send($"Sự kiện mới: {ev.Title}");
        return ev;
    }
    public IEnumerable<Event> GetByClub(Guid clubId) => _repository.GetByClubId(clubId);

    public Event? GetById(Guid id) => _repository.GetById(id);

    public void Delete(Guid id)
    {
        if (!_repository.Remove(id))
            throw new KeyNotFoundException($"Không tìm thấy sự kiện {id}.");
    }
}