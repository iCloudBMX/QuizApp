using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> SelectUserWithOtpCodesAsync(Guid userId);
}
