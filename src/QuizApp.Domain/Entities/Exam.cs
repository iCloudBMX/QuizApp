namespace QuizApp.Domain.Entities;

public class Exam
{
    public Guid Id { get; set; }
    public Guid TesterId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public TimeOnly Duration { get; set; }
    public string Link { get; set; }
    public int QuestionCount { get; set; }
    public User Tester { get; set; }
    public ICollection<AttendantAnswer> AttendantAnswers { get; set; }
    public ICollection<ExamAttendant> ExamAttendants { get; set; }
}
