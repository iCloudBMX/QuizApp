using QuizApp.Application.Abstractions;

namespace QuizApp.Application.AttendantAnswers.GetAttendantAnswerByExam;

public record GetAttendantAnswersQuery(
    string? Token,
    Guid? ExamId)
    : IQuery<GetAttendantAnswersQueryResponse>;