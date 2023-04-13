namespace QuizApp.Application.Tag.GetTagById;

public record TagResponse(
    Guid Id,
    string Title,
    Guid TesterId);