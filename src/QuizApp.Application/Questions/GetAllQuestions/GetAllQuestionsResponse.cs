using QuizApp.Application.Questions.GetQuestionById;

namespace QuizApp.Application.Questions.GetAllQuestions;
public record GetAllQuestionsResponse(
    IList<GetQuestionByIdResponse> Allquestion);
