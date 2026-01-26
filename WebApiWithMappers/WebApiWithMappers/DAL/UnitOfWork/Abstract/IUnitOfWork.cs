using WebApiWithMappers.DAL.Repositories.Abstract;

namespace WebApiWithMappers.DAL.UnitOfWork.Abstract
{
    public interface IUnitOfWork
    {
        public ICategoryRepository CategoryRepository { get; }
		public IProductRepository ProductRepository { get; }
		Task SaveAsync();
	}
}
