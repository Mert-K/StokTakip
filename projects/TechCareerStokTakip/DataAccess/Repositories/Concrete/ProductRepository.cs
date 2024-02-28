using Core.Persistence.Repositories;
using DataAccess.Context;
using DataAccess.Repositories.Abstracts;
using Models.Dtos.ResponseDto;
using Models.Entities;

namespace DataAccess.Repositories.Concrete
{
    public class ProductRepository : EfRepositoryBase<BaseDbContext, Product, Guid>, IProductRepository
    {
        public ProductRepository(BaseDbContext context) : base(context)
        {
        }

        public List<ProductDetailDto> GetAllProductDetails() =>
                Context.Products.Join
                (Context.Categories,
                p => p.CategoryId,
                c => c.Id,
                (product, category) => new ProductDetailDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryName = category.Name
                }).ToList();


        public List<ProductDetailDto> GetDetailsByCategoryId(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ProductDetailDto GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}
