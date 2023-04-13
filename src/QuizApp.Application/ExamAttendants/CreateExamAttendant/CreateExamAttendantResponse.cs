namespace QuizApp.Application.ExamAttendants.CreateExamAttendant;

public record CreateExamAttendantResponse(
    Guid examId,
    string name);
