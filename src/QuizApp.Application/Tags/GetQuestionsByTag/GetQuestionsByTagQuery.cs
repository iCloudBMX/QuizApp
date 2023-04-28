using QuizApp.Application.Abstractions;
using QuizApp.Application.Questions.GetQuestionById;

namespace QuizApp.Application.Tags.GetQuestionsByTag;

public record GetQuestionsByTagQuery (Guid TagId) :
    IQuery<IList<GetQuestionByIdResponse>>;