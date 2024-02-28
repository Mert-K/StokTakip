using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace DataAccess.Context;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions<BaseDbContext> opt):base(opt)
    {
        
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Categories { get; set; }
}
