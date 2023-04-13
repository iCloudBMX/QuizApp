using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Tag.GetTagById;

public record GetTagByIdQuery(Guid TagId) : IQuery<TagResponse>;
