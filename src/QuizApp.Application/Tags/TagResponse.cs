namespace QuizApp.Application.Tags.GetTagById;

public record TagResponse(
    Guid Id,
    string Title,
    Guid TesterId);