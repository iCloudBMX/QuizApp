using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Phone { get; private set; }
    public string Email { get; private set; }
    public UserRole Role { get; private set; }
    public string Salt { get; set; }
    public string PasswordHash { get; private set; }
    public DateTime RegisteredAt { get; } = DateTime.Now;
    public DateTime? LastLogin { get; private set; }
    public UserStatus Status { get; private set; }

    public ICollection<Question> Questions { get; private set; } =
        new List<Question>();

    public ICollection<Tag> Tags { get; private set; } = 
        new List<Tag>();

    public ICollection<Exam> Exams { get; private set; } =
        new List<Exam>(); 

    public ICollection<OtpCode> OtpCodes { get; private set; } = 
        new List<OtpCode>();

    public User(
        string firstName,
        string lastName,
        string phone,
        string email,
        string salt,
        string passwordHash,
        UserRole role)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Status = UserStatus.New;
    }

    public void MarkAsActive() => Status = UserStatus.Active;
}


