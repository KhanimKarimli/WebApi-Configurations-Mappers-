using WebApiWithMappers.Core.DAL.Repository.Concrete.EfCore;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.Repositories.Abstract;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.DAL.Repositories.Concrete.EfCore
{
    public class EfProductRepository : EfBaseRepository<Product, ApiDbContext>, IProductRepository
	{
		public EfProductRepository(ApiDbContext context) : base(context)
		{

		}
    }
}
