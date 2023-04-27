using QuizApp.Application.ExamAttendants.GetExamAttendantsByExam;
using QuizApp.Domain.Entities;

namespace QuizApp.Application.ExamAttendants;

internal class ExamAttendantMapper
{
    public static ExamAttendant MapToExamAttendant(CreateExamAttendantCommand command)
    {
        return new ExamAttendant()
        {
            ExamId = command.ExamId,
            Name = command.Name,
            Score = 0,
            Token = Guid.NewGuid().ToString()
        };
    }
    public static ExamAttendantResponse? MapToExamAttendantResponse(ExamAttendant? attendant)
    {
        if(attendant == null)
        {
            return null;
        }
        return new ExamAttendantResponse(
            attendant.Id,
            attendant.ExamId,
            attendant.Name,
            attendant.Token,
            attendant.Score);
    }
}
