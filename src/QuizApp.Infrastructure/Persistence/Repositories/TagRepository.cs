using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class TagRepository : Repository<Tag>,ITagRepository
{
    public TagRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    { }
}
