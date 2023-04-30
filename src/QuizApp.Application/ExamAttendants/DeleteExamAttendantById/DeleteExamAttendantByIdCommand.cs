using QuizApp.Application.Abstractions;

namespace QuizApp.Application.ExamAttendants.DeleteExamAttendantById;

public record DeleteExamAttendantByIdCommand(
    Guid id) : ICommand<ExamAttendantResponse>;
