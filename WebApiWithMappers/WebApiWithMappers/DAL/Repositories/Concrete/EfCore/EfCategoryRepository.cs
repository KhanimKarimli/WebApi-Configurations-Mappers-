using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiWithMappers.Core.DAL.Repository.Concrete.EfCore;
using WebApiWithMappers.DAL.EfCore;
using WebApiWithMappers.DAL.Repositories.Abstract;
using WebApiWithMappers.Entities;

namespace WebApiWithMappers.DAL.Repositories.Concrete.EfCore
{
    public class EfCategoryRepository : EfBaseRepository<Category, ApiDbContext>, ICategoryRepository
    {
        public EfCategoryRepository(ApiDbContext context) : base(context)
        {

        }
    }
}
