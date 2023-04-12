namespace QuizApp.Domain.Entities;

public class Exam
{
    public Guid Id { get; set; }
    public Guid TesterId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public DateTime Duration { get; set; }
    public string Link { get; set; }
    public int QuestionCount { get; set; }
    public User Tester { get; set; }
    public ICollection<ExamAttendant> Attendants { get; set; }
    public ICollection<Question> Questions { get; set; }
}
