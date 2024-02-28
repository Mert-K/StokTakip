using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.Entities;

namespace DataAccess.Repositories.Concrete;

public class CategoryRepository : EfRepositoryBase<BaseDbContext, Category, int>, ICategoryRepository
{
    public CategoryRepository(BaseDbContext context) : base(context)
    {

    }
}
