using QuizApp.Application.Abstractions;
using QuizApp.Application.Tags.GetTagById;

namespace QuizApp.Application.Tags.CreateTag;

public record CreateTagCommand (string Title, Guid TesterId) 
    : ICommand<TagResponse>;