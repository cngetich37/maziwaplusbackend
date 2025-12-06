using System.Linq.Expressions;

namespace MaziwaPlus.Data.Repositories;

public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task<List<T>> ListAsync(Expression<Func<T, bool>>? predicate = null);
}
