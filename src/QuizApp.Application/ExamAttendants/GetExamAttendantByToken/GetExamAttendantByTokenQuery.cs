using QuizApp.Application.Abstractions;

namespace QuizApp.Application.ExamAttendants.GetExamAttendantByToken;

public record GetExamAttendantByTokenQuery(
    string token):IQuery<ExamAttendantResponse>;
