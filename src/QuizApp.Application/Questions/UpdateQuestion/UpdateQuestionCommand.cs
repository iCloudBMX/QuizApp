using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.UpdateQuestion;
public record UpdateQuestionCommand(
    Guid Id,
    int? Type,
    int? Level,
    string? Content) : ICommand<UpdateQuestionResponse>;
