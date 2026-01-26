using Microsoft.EntityFrameworkCore;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.Repositories.Abstract;
using WebApiWithMappers.DAL.Repositories.Concrete.EfCore;
using WebApiWithMappers.DAL.UnitOfWork.Abstract;

namespace WebApiWithMappers.DAL.UnitOfWork.Concrete
{
    public class UnitOfWork:IUnitOfWork
    {
		private readonly ApiDbContext _context;
        private readonly ICategoryRepository _categoryRepository;
		private readonly IProductRepository _productRepository;

		public UnitOfWork(ApiDbContext context)
        {
            _context=context;
        }

        public ICategoryRepository CategoryRepository => _categoryRepository ?? new EfCategoryRepository(_context);
		public IProductRepository ProductRepository => _productRepository ?? new EfProductRepository(_context);

		public async Task SaveAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
