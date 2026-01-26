using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiWithMappers.Core.DAL.Repository.Abstract;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.Core.DAL.Repository.Concrete.EfCore
{
    public abstract class EfBaseRepository<TEntity,TContext>:IBaseRepository<TEntity>
		where TEntity : class,new()
		where TContext : DbContext
    {
		private readonly TContext _context;
		private readonly DbSet<TEntity> _entities;

		public EfBaseRepository(TContext context)
		{
			_context=context;
			_entities=_context.Set<TEntity>();
		}

		public async Task AddAsync(TEntity entity)
		{
			await _entities.AddAsync(entity);
		}

		public void Delete(Guid id)
		{
			var entity = _entities.Find(id);
			_entities.Remove(entity);
		}

		public Task<TEntity> Get(Expression<Func<TEntity, bool>> filter, params string[] includes)
		{
			IQueryable<TEntity> query = GetQuery(includes);
			return query.FirstOrDefaultAsync(filter);
		}

		public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, params string[] includes)
		{
			IQueryable<TEntity> query = GetQuery(includes);
			return filter==null
				? query.ToListAsync()
				: query.Where(filter)
				.ToListAsync();
		}

		public Task<List<TEntity>> GetPaginatedAsync(int page, int size, Expression<Func<TEntity, bool>> filter = null, params string[] includes)
		{
			IQueryable<TEntity> query = GetQuery(includes);
			return filter==null
				? query.ToListAsync()
				: query.Where(filter).Skip((page-1)*size).Take(size)
				.ToListAsync();
		}

	

		public void Update(TEntity entity)
		{
			_entities.Update(entity);
		}

		private IQueryable<TEntity> GetQuery(string[] includes)
		{
			IQueryable<TEntity> query = _entities;
			foreach (var include in includes)
			{
				query=query.Include(include);
			}
			return query;
		}
	}
}

