namespace QuizApp.Application.Questions.UpdateQuestion;
public record UpdateQuestionResponse(
    Guid Id,
    int Type,
    int Level,
    string Content);
