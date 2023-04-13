using QuizApp.Application.Abstractions;
using QuizApp.Application.ExamAttendants.CreateExamAttendant;

namespace QuizApp.Application.ExamAttendants;

public record CreateExamAttendantCommand()
    : ICommand<CreateExamAttendantResponse>;
