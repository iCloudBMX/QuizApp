using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class AttendantAnswerRepository : Repository<AttendantAnswer>, IAttendantAnswerRepository
{
    public AttendantAnswerRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    { }
}
