using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class ExamAttendantRepository : Repository<ExamAttendant>, IExamAttendantRepository
{
    public ExamAttendantRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
