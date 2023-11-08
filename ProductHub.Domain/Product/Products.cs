using ProductHub.Domain.Category.ValueObjects;
using ProductHub.Domain.Common.Models;
using ProductHub.Domain.Product.Entities;
using ProductHub.Domain.Product.ValueObjects;

namespace ProductHub.Domain.Product;

public class Product : AggregateRoot<ProductId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public CategoryId CategoryId { get; private set; }
    private List<Booking> _bookings = new();

    public IReadOnlyList<Booking> Bookings => _bookings.AsReadOnly();



    public Product(ProductId id, string name, string description, decimal price, CategoryId categoryId, List<Booking>? bookings) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        _bookings = bookings ?? new List<Booking>();
    }

    public static Product Create(string name, string description, decimal price, CategoryId categoryId, List<Booking>? bookings)
    {
        return new(ProductId.CreateUnique(), name, description, price, categoryId, bookings);
    }

    public void Update(string name, string description, decimal price, CategoryId categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
    }

    public void AddBooking(DateTime startDate, DateTime endDate, int quantity)
    {
        _bookings.Add(Booking.Create(startDate, endDate, quantity));
    }

    public void UpdateBooking(BookingId bookingId, DateTime startDate, DateTime endDate, int quantity)
    {
        var booking = Bookings.Single(x => x.Id == bookingId);
        booking.Update(startDate, endDate, quantity);
    }

    public void RemoveBooking(BookingId bookingId)
    {
        var booking = Bookings.Single(x => x.Id == bookingId);
        _bookings.Remove(booking);
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Product() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}