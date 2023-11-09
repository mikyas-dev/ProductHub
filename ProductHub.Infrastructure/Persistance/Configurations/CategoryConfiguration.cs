using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductHub.Domain.Category;
using ProductHub.Domain.Category.ValueObjects;

namespace ProductHub.Infrastructure.Persistance.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoriesTable(builder);
    }


    private void ConfigureCategoriesTable(EntityTypeBuilder<Category> builder)
    {
        builder
            .ToTable("Categories");

        builder
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));

        builder
            .Property(c => c.Name)
            .HasMaxLength(50);

        builder
            .Property(c => c.Description)
            .HasMaxLength(150);

    }
}