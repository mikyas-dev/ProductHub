using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.Booking.ValueObjects;

public class BookingId : ValueObject
{
    public Guid Value { get; }

    public BookingId(Guid value)
    {
        Value = value;
    }

    public static BookingId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static BookingId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator BookingId(Guid v)
    {
        return new(v);
    }
}