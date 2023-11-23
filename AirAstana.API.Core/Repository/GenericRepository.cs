using AirAstana.API.Core.Contracts;
using AirAstana.API.Core.DTOs;
using AirAstana.API.Core.Exceptions;
using AirAstana.API.DAL;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AirAstana.API.Core.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AirAstanaDbContext Db;
    protected readonly IMapper Mapper;

    protected GenericRepository(AirAstanaDbContext db, IMapper mapper)
    {
        Db = db;
        Mapper = mapper;
    }

    public async Task<T> GetByIdAsync(int id)
    {
        try
        {
            return await Db.Set<T>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while trying to find entity with id {id}.", ex);
        }
    }

    public async Task<TResult> GetByIdAsync<TResult>(int id)
    {
        var result = await Db.Set<T>()
            .FindAsync(id);

        if (result == null)
            throw new NotFoundException(typeof(T).Name, id);
        
        return Mapper.Map<TResult>(result);
    }

    public async Task<IList<T>> GetAllAsync()
    {
       return await Db.Set<T>().ToListAsync();
    }

    public async Task<IList<TResult>> GetAllAsync<TResult>()
    {
        return await Db
            .Set<T>()
            .ProjectTo<TResult>(Mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters queryParameters)
    {
        var totalSize = await Db.Set<T>().CountAsync();
        var items = await Db.Set<T>()
            .Skip(queryParameters.StartIndex)
            .Take(queryParameters.PageSize)
            .ProjectTo<TResult>(Mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<TResult>
        {
            Items = items,
            PageNumber = queryParameters.StartIndex,
            RecordNumber = queryParameters.PageSize,
            TotalCount = totalSize
        };
    }

    public async Task<T> AddAsync(T entity)
    {
        Db.Set<T>().Add(entity);
        await Db.SaveChangesAsync();
        return entity;
    }

    public async Task<TResult> AddAsync<TSource, TResult>(TSource source)
    {
        var entity = Mapper.Map<T>(source);

        await Db.AddAsync(entity);
        await Db.SaveChangesAsync();
        
        return Mapper.Map<TResult>(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Db.Set<T>().Update(entity);
        await Db.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync<TSource>(int id, TSource source)
    {
        var entity = await GetByIdAsync(id);
        
        if (entity == null)
        {
            throw new NotFoundException(typeof(T).Name, id);
        }
        
        Mapper.Map(source, entity);
        Db.Update(entity);
        await Db.SaveChangesAsync();
    }

    public async Task RemoveByIdAsync(int id)
    {
        T entity = Activator.CreateInstance<T>();
        typeof(T).GetProperty("Id")?.SetValue(entity, id);
        Db.Set<T>().Remove(entity);
        await Db.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await Db.Set<T>().AnyAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public IQueryable<T> GetAllQueryable()
    {
        return Db.Set<T>().AsQueryable();
    }
}