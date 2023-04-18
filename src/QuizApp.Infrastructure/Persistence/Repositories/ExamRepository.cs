using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories
{
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        private readonly ApplicationDbContext applicationDbContext;
        public ExamRepository(ApplicationDbContext aplicationDbContext) :
            base(aplicationDbContext)
        {
            this.applicationDbContext = aplicationDbContext;
        }
    }
}
