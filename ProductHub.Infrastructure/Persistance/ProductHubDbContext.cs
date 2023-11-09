using ProductHub.Domain.User;
using Microsoft.EntityFrameworkCore;
using ProductHub.Domain.Category;
using ProductHub.Domain.Product;

namespace ProductHub.Infrastructure.Persistance;

public class ProductHubDbContext : DbContext
{
    public ProductHubDbContext(DbContextOptions<ProductHubDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}