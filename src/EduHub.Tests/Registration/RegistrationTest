using EduHub.Domain.Entities;

namespace EduHub.Tests.Registrations;

public class RegistrationTests
{
    private readonly Guid memberId = Guid.NewGuid();
    private readonly Guid eventId = Guid.NewGuid();

    [Fact]
    public void Create_WithValidIds_StatusIsPending()
    {
        var r = Registration.Create(memberId, eventId);

        Assert.Equal(RegistrationStatus.Pending, r.Status);
        Assert.Equal(memberId, r.MemberId);
        Assert.Equal(eventId, r.EventId);
    }

    [Fact]
    public void Create_WithEmptyMemberId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(Guid.Empty, eventId));
    }

    [Fact]
    public void Create_WithEmptyEventId_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => Registration.Create(memberId, Guid.Empty));
    }

    [Fact]
    public void Confirm_FromPending_StatusIsConfirmed()
    {
        var r = Registration.Create(memberId, eventId);

        r.Confirm();

        Assert.Equal(RegistrationStatus.Confirmed, r.Status);
    }

    [Fact]
    public void Cancel_FromConfirmed_StatusIsCancelled()
    {
        var r = Registration.Create(memberId, eventId);
        r.Confirm();

        r.Cancel();

        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }

    // Tình huống bắt buộc của đề: Cancel() rồi Confirm()
    [Fact]
    public void Confirm_AfterCancel_ThrowsAndStatusStaysCancelled()
    {
        var r = Registration.Create(memberId, eventId);
        r.Cancel();

        Assert.Throws<InvalidOperationException>(() => r.Confirm());
        Assert.Equal(RegistrationStatus.Cancelled, r.Status);
    }
}