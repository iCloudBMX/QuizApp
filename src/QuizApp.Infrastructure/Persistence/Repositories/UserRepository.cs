using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }

    public async Task<User> SelectUserWithOtpCodesAsync(Guid userId)
    {
        return await this.applicationDbContext
            .Set<User>()
            .Include(p => p.OtpCodes)
            .FirstOrDefaultAsync(p => p.Id == userId);
    }
}
