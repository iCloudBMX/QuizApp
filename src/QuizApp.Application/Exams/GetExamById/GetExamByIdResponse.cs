using QuizApp.Domain.Entities;

namespace QuizApp.Application.Exams.GetExamById;

public record GetExamByIdResponse(
    Guid id,
    Guid testerId,
    DateTime startsAt,
    DateTime endsAt,
    DateTime duration,
    string link,
    string title,
    int questionCount,
    ICollection<ExamAttendant> attendants,
    ICollection<Question> questions);
