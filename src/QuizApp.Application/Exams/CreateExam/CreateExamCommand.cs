using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Exams.CreateExam;

public record CreateExamCommand(
    Guid testerId,
    DateTime startsAt,
    DateTime endsAt,
    DateTime duration,
    int QuestionCount) : ICommand<CreateExamResponse>;

