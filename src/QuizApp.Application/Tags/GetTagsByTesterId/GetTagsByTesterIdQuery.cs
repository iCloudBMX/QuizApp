using QuizApp.Application.Abstractions;
using QuizApp.Application.Tags.GetTagById;

namespace QuizApp.Application.Tags.GetTagsByTesterId;

public record GetTagsByTesterIdQuery (Guid TesterId)
    : IQuery<IList<TagResponse>>;
