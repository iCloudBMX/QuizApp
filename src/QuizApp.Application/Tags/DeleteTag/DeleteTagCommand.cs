using QuizApp.Application.Abstractions;

namespace QuizApp.Application.Tags.DeleteTag;

public record DeleteTagCommand(Guid TagId) : ICommand<Guid>;
