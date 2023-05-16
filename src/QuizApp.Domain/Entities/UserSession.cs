using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Entities
{
    public class UserSession
    {
        public Guid Id { get; private set; }
        public string Token { get; private set; }
        public SessionStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.Now;
        public Guid UserId { get; private set; }
        public User User { get; private set; }

        public bool IsVerified() =>
            Status == SessionStatus.Verified;
        public void MarkVerified() =>
            Status = SessionStatus.Verified;
    }
}
