namespace QuizApp.Domain.Entities;

public class QuestionOption
{
    public Guid Id { get; set; }
    public bool IsCorrect { get; set; }
    public string Content { get; set; }

    public Guid QuestionId { get; set; }
    public Question Question { get; set; }

    public ICollection<AttendantsAnswer> AttendantsAnswers { get; set; }
}