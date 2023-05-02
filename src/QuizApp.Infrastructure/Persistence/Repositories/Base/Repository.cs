using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Repositories;
using System.Linq.Expressions;

namespace QuizApp.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext applicationDbContext;

    public Repository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public void Insert(T entity) =>
        this.applicationDbContext
        .Set<T>()
        .Add(entity);

    public async Task<IReadOnlyList<T>> SelectAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await this.applicationDbContext
            .Set<T>()
            .ToListAsync(cancellationToken);
    }

    public async Task<T> SelectAsync(
        CancellationToken cancellationToken = default,
        params object[] entityIds)
    {
        return await this.applicationDbContext
            .Set<T>()
            .FindAsync(
                keyValues: entityIds,
                cancellationToken: cancellationToken);
    }

    public void Update(T entity) =>
        this.applicationDbContext.Entry(entity).State = EntityState.Modified;

    public void Delete(T entity) =>
        this.applicationDbContext
        .Set<T>()
        .Remove(entity);

    public void ExacuteSqlQuery(string sqlQury)
    {
        applicationDbContext.Database.ExecuteSqlRaw(sqlQury);
    }
}
