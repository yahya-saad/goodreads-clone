using Goodreads.Application.Common.Extensions;
using Goodreads.Application.Common.Interfaces;
using Goodreads.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Goodreads.Infrastructure.Repositories;
internal class GenericRepository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<T?> GetByIdAsync(string id, params string[] includes)
    {
        var query = _dbSet.AsQueryable();

        query = query.ApplyIncludes(includes);

        return await query.FirstOrDefaultAsync(e => EF.Property<string>(e, "Id") == id);
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<(IEnumerable<T> Items, int Count)> GetAllAsync(
        Expression<Func<T, bool>> filter = null,
        string[]? includes = null,
        string? sortColumn = null, string? sortOrder = null,
        int? pageNumber = null, int? pageSize = null)
    {
        var query = _dbSet.AsQueryable();

        query = query.ApplyIncludes(includes);

        if (filter != null)
            query = query.Where(filter);

        var count = await query.CountAsync();

        query = query.ApplySorting(sortColumn, sortOrder);
        query = query.ApplyPaging(pageNumber, pageSize);

        var items = await query.ToListAsync();

        return (items, count);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? filter = null)
    {
        return await _dbSet.CountAsync(filter ?? (_ => true));
    }


    public async Task<T?> GetSingleOrDefaultAsync(Expression<Func<T, bool>> filter, string[]? includes = null)
    {
        var query = _dbSet.AsQueryable();
        query = query.ApplyIncludes(includes);
        return await query.SingleOrDefaultAsync(filter);
    }

}
