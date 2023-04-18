using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;
internal class QuestionRepository : Repository<Question>, IQuestionRepository
{
    private readonly ApplicationDbContext applicationDbContext;
    public QuestionRepository(ApplicationDbContext applicationDbContext) 
        : base(applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }
}
