using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;
using System.Linq.Expressions;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class ExamAttendantRepository : Repository<ExamAttendant>, IExamAttendantRepository
{
    public ExamAttendantRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }

    public async ValueTask<IQueryable<ExamAttendant>> SelectAsync()
    {
       return applicationDbContext.Set<ExamAttendant>();
    }
}
