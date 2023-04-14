using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Questions.GetQuestionById;
public record GetQuestionByIdQuery(Guid questionId) : IQuery<QuestionResponse>;
