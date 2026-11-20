using EduHub.Domain.Entities;
using EduHub.Domain.Interfaces;

namespace EduHub.Infrastructure.Repositories;

public class InMemoryEventRepository : IEventRepository
{
    // static: dùng chung giữa các instance (xem TRA-LOI.md câu 5d)
    private static readonly List<Event> _events = new();
    private static readonly object _lock = new();

    public void Add(Event ev)
    {
        lock (_lock) _events.Add(ev);
    }

    public IEnumerable<Event> GetByClubId(Guid clubId)
    {
        lock (_lock) return _events.Where(e => e.ClubId == clubId).ToList();
    }

    public Event? GetById(Guid id)
    {
        lock (_lock) return _events.FirstOrDefault(e => e.Id == id);
    }

    public bool Remove(Guid id)
    {
        lock (_lock) return _events.RemoveAll(e => e.Id == id) > 0;
    }
}
