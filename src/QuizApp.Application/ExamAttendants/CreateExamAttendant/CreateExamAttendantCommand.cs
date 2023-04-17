using QuizApp.Application.Abstractions;

namespace QuizApp.Application.ExamAttendants;

public record CreateExamAttendantCommand(
    Guid ExamId,
    string Name)
    : ICommand<ExamAttendantResponse>;
