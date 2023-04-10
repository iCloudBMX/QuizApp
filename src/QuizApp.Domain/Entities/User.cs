namespace QuizApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime LastLogin { get; set; }

    public ICollection<Question> Questions { get; set; }
    public ICollection<Tag> Tags { get; set; }
    //public ICollection<Exam> Exams { get; set; }
}
