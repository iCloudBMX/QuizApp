namespace QuizApp.Application.ExamAttendants;

public record ExamAttendantResponse(
    Guid Id,
    Guid ExamId,
    string Name,
    string Token,
    float Score);
