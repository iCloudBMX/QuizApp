using QuizApp.Application.Abstractions;

namespace QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;

public record GetExamAttendantByExamQuery(Guid ExamId):IQuery<IQueryable<ExamAttendantResponse>>;

