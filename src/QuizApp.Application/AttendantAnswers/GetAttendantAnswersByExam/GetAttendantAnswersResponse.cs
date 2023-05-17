namespace QuizApp.Application.AttendantAnswers.GetAttendantAnswerByExam;

public record GetAttendantAnswersResponse(
    ICollection<AttendantAnswerResponse> Response);