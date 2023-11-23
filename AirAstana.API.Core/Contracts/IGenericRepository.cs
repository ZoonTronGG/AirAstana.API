using AirAstana.API.Core.DTOs;

namespace AirAstana.API.Core.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<TResult> GetByIdAsync<TResult>(int id);
    Task<IList<T>> GetAllAsync();
    Task<IList<TResult>> GetAllAsync<TResult>();
    Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters);
    Task<T> AddAsync(T entity);
    Task<TResult> AddAsync<TSource, TResult>(TSource source);
    Task<T> UpdateAsync(T entity);
    Task UpdateAsync<TSource>(int id, TSource source);
    Task RemoveByIdAsync(int id);
    Task<bool> ExistsAsync(int id);
    IQueryable<T> GetAllQueryable();
}