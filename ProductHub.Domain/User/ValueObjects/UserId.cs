using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.User.ValueObjects;

public class UserId: ValueObject
{
    public Guid Value { get; }
    
    public UserId(Guid value)
    {
        Value = value;
    }
    
    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserId Create(Guid value)
    {
        return new(value);
    }
    
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static explicit operator UserId(Guid v)
    {
        return new(v);
    }
}