using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Tags.GetTagById;

public record GetTagByIdQuery(Guid TagId) : IQuery<TagResponse>;
