namespace EduHub.Domain.Entities;

public class Member
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Email { get; private set; } = string.Empty;

    private Member() { }

    public static Member Create(string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        if (!email.Contains('@'))
            throw new ArgumentException("Email phải chứa ký tự '@'.", nameof(email));

        return new Member { Email = email.Trim() };
    }
}
