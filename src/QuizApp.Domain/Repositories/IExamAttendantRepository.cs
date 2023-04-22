using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Repositories;

public interface IExamAttendantRepository : IRepository<ExamAttendant>
{
    public ValueTask<IQueryable<ExamAttendant>> SelectAsync();
}
