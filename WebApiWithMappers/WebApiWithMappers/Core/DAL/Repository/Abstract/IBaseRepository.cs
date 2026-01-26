using System.Linq.Expressions;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.Core.DAL.Repository.Abstract
{
    public interface IBaseRepository<TEntity>
		where TEntity : class,new()
    {
		Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes);
		Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, params string[] includes);
		Task<List<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes);
		Task AddAsync(TEntity entity);
		void Delete(Guid id);
		void Update(TEntity entity);
		
	}
}
