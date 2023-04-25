using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class QuestionOptionRepository : Repository<QuestionOption>, IQuestionOptionRepository
{
    public QuestionOptionRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    { }
}
