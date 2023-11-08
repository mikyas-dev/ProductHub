using ProductHub.Domain.Common.Models;
using ProductHub.Domain.Product.ValueObjects;

namespace ProductHub.Domain.Product.Entities;

public class Booking : AggregateRoot<BookingId>
{
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int Quantity { get; private set; }

    public Booking(BookingId id, DateTime startDate, DateTime endDate, int quantity) : base(id)
    {
        StartDate = startDate;
        EndDate = endDate;
        Quantity = quantity;
    }

    public static Booking Create(DateTime startDate, DateTime endDate, int quantity)
    {
        return new(BookingId.CreateUnique(), startDate, endDate, quantity);
    }

    public void Update(DateTime startDate, DateTime endDate, int quantity)
    {
        StartDate = startDate;
        EndDate = endDate;
        Quantity = quantity;
    }

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private Booking() { }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}