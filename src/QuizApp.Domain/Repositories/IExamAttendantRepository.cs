using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Repositories;

public interface IExamAttendantRepository : IRepository<ExamAttendant>
{
    /// <summary>
    /// Returns IQueryable for adding new expressions and send response to database
    /// </summary>
    /// <returns></returns>
    public ValueTask<IQueryable<ExamAttendant>> SelectAsync();
}
