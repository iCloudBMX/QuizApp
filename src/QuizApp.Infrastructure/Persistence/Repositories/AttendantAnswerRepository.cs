using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class AttendantAnswerRepository : Repository<AttendantAnswer>, IAttendantAnswerRepository
{
    public AttendantAnswerRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    { }

    public async ValueTask<IQueryable<AttendantAnswer>> SelectWithQuestions()
    {
        return applicationDbContext
            .Set<AttendantAnswer>()
            .Include(a => a.Option)
            .Include(a => a.Question)
            .ThenInclude(q => q.QuestionOptions);
    }
}
