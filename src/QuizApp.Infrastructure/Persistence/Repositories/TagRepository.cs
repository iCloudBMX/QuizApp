using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Repositories;

namespace QuizApp.Infrastructure.Persistence.Repositories;

internal class TagRepository : Repository<Tag>, ITagRepository
{
    public TagRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    { }

    public async ValueTask<IList<Tag>> GetAllTagsWithQuestions()
    {
        return applicationDbContext.Set<Tag>().Include(t => t.Questions).ToList();
    }
}
