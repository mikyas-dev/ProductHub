using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.User;

public sealed class Category : AggregateRoot<CategoryId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Category(CategoryId id, string name, string description) : base(id)
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string description)
    {
        return new(CategoryId.CreateUnique(), name, description);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Category() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}