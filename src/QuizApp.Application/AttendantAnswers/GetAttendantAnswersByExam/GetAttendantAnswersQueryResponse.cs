namespace QuizApp.Application.AttendantAnswers.GetAttendantAnswerByExam;

public record GetAttendantAnswersQueryResponse(
    ICollection<AttendantAnswerResponse> Response);