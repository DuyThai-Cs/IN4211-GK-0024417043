namespace EduHub.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ClubId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    public int Capacity { get; private set; }

    private Event() { }

    public static Event Create(Guid clubId, string title, DateTime date, int capacity)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(title);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        return new Event { ClubId = clubId, Title = title.Trim(), Date = date, Capacity = capacity };
    }
}