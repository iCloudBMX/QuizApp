using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }
}
