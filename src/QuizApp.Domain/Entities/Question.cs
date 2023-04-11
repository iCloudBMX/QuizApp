using QuizApp.Domain.Enums;

namespace QuizApp.Domain.Entities;

public class Question
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public QuestionTypes Type { get; set; }
    public QuestionLevel Level { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }

    public User Tester { get; set; }
    public ICollection<QuestionOption> QuestionOptions { get; set; }
    public ICollection<Tag> Tags { get; set; }
    public ICollection<Exam> Exams { get; set; }
    public ICollection<AttendantAnswer> AttendantAnswers { get; set; }
}