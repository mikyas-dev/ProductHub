using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.Category.ValueObjects;

public class CategoryId: ValueObject
{
    public Guid Value { get; }
    
    public CategoryId(Guid value)
    {
        Value = value;
    }
    
    public static CategoryId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CategoryId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator CategoryId(Guid v)
    {
        return new(v);
    }
}