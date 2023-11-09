using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Common.Models;
using ProductHub.Domain.Product.ValueObjects;
using ProductHub.Domain.User.ValueObjects;

namespace ProductHub.Domain.Product;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public CategoryId CategoryId { get; private set; }
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
    

    public static Product Create(string name, string description, decimal price, CategoryId categoryId, UserId userId, int quantity)
    {
        return new(ProductId.CreateUnique(), name, description, price, categoryId, userId, quantity);
    }

    public void Update(string name, string description, decimal price, CategoryId categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }


    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}