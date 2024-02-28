using Core.Persistence.EntityBaseModel;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistence.Repositories;

//Entity class'ları Entity<TId> 'den türetildi. Böylece aşağıdaki interface, sadece Entity class'ları için kullanılabilecek.

//Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null başka tabloyu include etmek için yazıldı.
public interface IEntityRepository<TEntity, TId> where TEntity : Entity<TId>
{
    void Add(TEntity entity);
    TEntity? GetById(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    TEntity? GetByFilter(Expression<Func<TEntity, bool>> predicate,
                         Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    
}
