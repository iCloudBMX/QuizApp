using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.CreateQuestion;
public record CreateQuestionResponse(Guid id, string content);
