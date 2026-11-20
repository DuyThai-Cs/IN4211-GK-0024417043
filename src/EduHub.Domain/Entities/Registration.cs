namespace EduHub.Domain.Entities;

public class Registration
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime RegisteredAt { get; private set; } = DateTime.UtcNow;
    public RegistrationStatus Status { get; private set; } = RegistrationStatus.Pending;

    private Registration() { }

    public static Registration Create(Guid memberId, Guid eventId)
    {
        if (memberId == Guid.Empty)
            throw new ArgumentException("MemberId không được rỗng.", nameof(memberId));
        if (eventId == Guid.Empty)
            throw new ArgumentException("EventId không được rỗng.", nameof(eventId));

        return new Registration { MemberId = memberId, EventId = eventId };
    }

    public void Confirm()
    {
        if (Status != RegistrationStatus.Pending)
            throw new InvalidOperationException(
                $"Chỉ phiếu Pending mới được xác nhận (hiện tại: {Status}).");
        Status = RegistrationStatus.Confirmed;
    }

    public void Cancel()
    {
        if (Status == RegistrationStatus.Cancelled)
            throw new InvalidOperationException("Phiếu đã huỷ, không thể huỷ lại.");
        Status = RegistrationStatus.Cancelled;
    }
}