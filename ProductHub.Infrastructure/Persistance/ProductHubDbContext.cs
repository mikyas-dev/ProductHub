using ProductHub.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace ProductHub.Infrastructure.Persistance;

public class ProductHubDbContext : DbContext
{
    public ProductHubDbContext(DbContextOptions<ProductHubDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}