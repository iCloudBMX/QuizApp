using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Entities;

public sealed class OtpCode
{
    public Guid Id { get; private set; }
    public string Code { get; private set; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
    public OtpCodeStatus Status { get; private set; }

    public Guid UserId { get; }
    public User User { get; }

    public OtpCode(
        string code)
    {
        Code = code;
        Status = OtpCodeStatus.Unverified;
    }
}