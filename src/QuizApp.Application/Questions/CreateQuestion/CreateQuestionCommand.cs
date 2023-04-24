using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.CreateQuestion;
public record CreateQuestionCommand(
    int Type,
    int Level,
    string Content,
    Guid UserId) : ICommand<CreateQuestionResponse>;
