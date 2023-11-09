using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Product;
using ProductHub.Domain.Product.ValueObjects;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        ConfigureProductsTable(builder);
    }

    private void ConfigureProductsTable(EntityTypeBuilder<Product> builder)
    {
        builder
            .ToTable("Products");

        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProductId.Create(value));

        builder
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder
            .Property(p => p.Description)
            .HasMaxLength(150);

        builder
            .Property(p => p.Price);

        builder
            .Property(p => p.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));

        builder
            .Property(p => p.UserId)
            .HasConversion(
                id => id.Value,
                value => UserId.Create(value));

    }
}
