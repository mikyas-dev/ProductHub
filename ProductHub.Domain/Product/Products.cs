using UserId = ProductHub.Domain.User.ValueObjects.UserId;
using ProductHub.Domain.Common.Models;
using ProductHub.Domain.Product.ValueObjects;
using ProductHub.Domain.Category.ValueObjects;

namespace ProductHub.Domain.Product;
using User = User.User;
using Category = Category.Category;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public CategoryId CategoryId { get; private set; }
    public User User { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
    public UserId UserId { get; private set; }

    public int Quantity { get; private set;}

    public bool IsAvailable => Quantity > 0;




    public Product(ProductId id, string name, string description, decimal price, CategoryId categoryId, UserId userId, int quantity) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        UserId = userId;
        Quantity = quantity;
    }

    public Product(ProductId id, string name, string description, decimal price, CategoryId categoryId, UserId userId, int quantity, User user, Category category) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        UserId = userId;
        Quantity = quantity;
        User = user;
        Category = category;
    }
    

    public static Product Create(string name, string description, decimal price, CategoryId categoryId, UserId userId, int quantity)
    {
        return new(ProductId.CreateUnique(), name, description, price, categoryId, userId, quantity);
    }

    public void Update(string name, string description, decimal price, CategoryId categoryId, int quantity)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        Quantity = quantity;
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}