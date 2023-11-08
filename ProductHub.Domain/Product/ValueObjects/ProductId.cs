using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.Product.ValueObjects;

public class ProductId : ValueObject
{
    public Guid Value { get; }

    public ProductId(Guid value)
    {
        Value = value;
    }

    public static ProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ProductId Create(Guid value)
    {
        return new(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator ProductId(Guid v)
    {
        return new(v);
    }
}