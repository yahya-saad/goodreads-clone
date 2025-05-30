using System.Linq.Expressions;

namespace Goodreads.Application.Common.Interfaces;
public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(string id, params string[] includes);
    Task<IEnumerable<T>> GetAllAsync();
    Task<(IEnumerable<T> Items, int Count)> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        string[]? includes = null,
        string? sortColumn = null,
        string? sortOrder = null,
        int? pageNumber = null,
        int? pageSize = null);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
}
