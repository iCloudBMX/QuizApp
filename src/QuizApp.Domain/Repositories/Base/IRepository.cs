namespace QuizApp.Domain.Repositories;

public interface IRepository<T> where T : class
{
    Task<IReadOnlyList<T>> SelectAllAsync(
        CancellationToken cancellationToken = default);

    Task<T> SelectAsync(
        CancellationToken cancellationToken = default,
        params object[] entityIds);
    /// <summary>
    /// Returns IQueryable for adding new expressions and send response to database
    /// </summary>
    /// <returns></returns>
    public IQueryable<T> SelectAsync();
    void Insert(T entity);
    void Update(T entity);
    void Delete(T entity);
    void ExacuteSqlQuery(string sqlQury);
}