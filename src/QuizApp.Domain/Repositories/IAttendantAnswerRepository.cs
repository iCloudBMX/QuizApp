using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Repositories;

public interface IAttendantAnswerRepository : IRepository<AttendantAnswer>
{
    public ValueTask<IQueryable<AttendantAnswer>> SelectWithQuestions();
}
