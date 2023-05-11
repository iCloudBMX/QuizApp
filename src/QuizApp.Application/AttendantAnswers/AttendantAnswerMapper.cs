using QuizApp.Application.AttendantAnswers.CreateAttendantAnswer;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.AttendantAnswers;

public static class AttendantAnswerMapper
{
    public static AttendantAnswer MapToAnswer(CreateAttendantAnswerCommand command)
    {
        return new AttendantAnswer()
        {
            ExamId = command.ExamId,
            ExamAttendantId = command.ExamAttendantId,
            QuestionId = command.QuestionId,
            OptionId = command.OptionId
        };
    }

    public static AttendantAnswerResponse MapToResponse(AttendantAnswer answer)
    {
        return new AttendantAnswerResponse(
            answer.Id,
            answer.ExamAttendantId,
            answer.QuestionId,
            answer.OptionId,
            answer.ExamId,
            answer.Question,
            answer.Option);
    }
}
