using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.UpdateQuestion;
public record UpdateQuestionCommand(
    int? Type,
    int? Level,
    string? Content) : ICommand<UpdateQuestionResponse>;
