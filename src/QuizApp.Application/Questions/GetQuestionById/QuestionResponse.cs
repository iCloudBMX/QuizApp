using QuizApp.Application.Users.GetUserById;

namespace QuizApp.Application.Questions.GetQuestionById;
public record QuestionResponse(
    Guid id,
    int type,
    int level,
    string content,
    DateTime creatAt,
    Guid userId
    //UserResponse tester
    //ICollection<QuestionOptionResponce> questionOptions,
    //ICollection<TagResponce> tags,
    //ICollection<ExamResponce> exams,
    //ICollection<AttendantAnswerResponce> attendantAnswer
    );
