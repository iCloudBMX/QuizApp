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
}
