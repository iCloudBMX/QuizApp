using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class QuestionOptionRepository : Repository<QuestionOption>, IQuestionOptionRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public QuestionOptionRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }
}
