namespace QuizApp.Domain.Entities;

public class AttendantsAnswer
{
    public Guid Id { get; set; }
    public Guid ExamAttendantsId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid OptionId { get; set; }
    public Guid ExamId { get; set; }
    public ExamAttendants ExamAttendants { get; set; }
    //public Question Question { get; set; }
    //public QuestionOption Option { get;set; }
    public Exam Exam { get; set; }

}
