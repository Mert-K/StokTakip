using Core.Persistence.Repositories;
using DataAccess.Context;
using Models.Entities;

namespace DataAccess.Repositories.Abstracts;

public interface ICategoryRepository : IEntityRepository<Category,int>
{

}
