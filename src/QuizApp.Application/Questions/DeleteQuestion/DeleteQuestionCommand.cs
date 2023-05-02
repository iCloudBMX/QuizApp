using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.DeleteQuestion;
public record DeleteQuestionCommand(Guid id) : ICommand<Guid>;
