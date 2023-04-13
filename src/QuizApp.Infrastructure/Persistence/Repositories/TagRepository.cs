using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class TagRepository : Repository<Tag>,ITagRepository
{
    private readonly ApplicationDbContext applicationDbContext;

    public TagRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }
}
