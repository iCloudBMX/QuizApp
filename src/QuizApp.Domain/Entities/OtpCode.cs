using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Entities;

public sealed class OtpCode
{
    private const int OTP_EXPIRATION_TIME_IN_SECONDS = 120;

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

    public bool IsExpired() =>
        CreatedAt.AddSeconds(OTP_EXPIRATION_TIME_IN_SECONDS) < DateTimeOffset.Now;

    public bool IsValid(string otpCode) => Code.Equals(otpCode);

    public void MarkAsVerified() => Status = OtpCodeStatus.Verified;

    public void MarkAsExpired() => Status = OtpCodeStatus.Expired;
}