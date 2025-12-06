using Microsoft.EntityFrameworkCore;
using MaziwaPlus.Data.Data;

namespace MaziwaPlus.Data.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    private readonly MaziwaPlusContext _db;
    public EfRepository(MaziwaPlusContext db) => _db = db;

    public async Task<T> AddAsync(T entity)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public Task<T?> GetByIdAsync(int id)
    {
        return _db.Set<T>().FindAsync(id).AsTask();
    }

    public async Task<List<T>> ListAsync(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
    {
        var query = _db.Set<T>().AsQueryable();
        if (predicate != null) query = query.Where(predicate);
        return await query.ToListAsync();
    }
}
