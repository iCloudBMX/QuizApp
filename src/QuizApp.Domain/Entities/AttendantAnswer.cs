namespace QuizApp.Domain.Entities;

public class AttendantAnswer
{
    public Guid Id { get; set; }
    public Guid ExamAttendantId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid ExamId { get; set; }
    public ExamAttendant ExamAttendant { get; set; }
    public Question Question { get; set; }
    public QuestionOption Option { get;set; }
    public Exam Exam { get; set; }

}
