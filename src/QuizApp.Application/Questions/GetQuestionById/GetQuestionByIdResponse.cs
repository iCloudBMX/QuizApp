namespace QuizApp.Application.Questions.GetQuestionById;
public record GetQuestionByIdResponse(
    Guid Id,
    int Type,
    int Level,
    string Content,
    DateTime CreateAt);
