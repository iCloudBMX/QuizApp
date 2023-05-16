using QuizApp.Application.Abstractions;

namespace QuizApp.Application.AttendantAnswers.CreateAttendantAnswer;

public record CreateAttendantAnswerCommand(
    Guid ExamAttendantId,
    Guid QuestionId,
    Guid OptionId,
    Guid ExamId):ICommand<Guid>;