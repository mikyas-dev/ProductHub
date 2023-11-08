namespace ProductHub.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return Equals((object?) other);
    }

    public override bool Equals(object? obj) {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject) obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());

    }
    
    public static bool operator ==(ValueObject? a, ValueObject? b) {
       return Equals(a, b);
    }

    public static bool operator !=(ValueObject? a, ValueObject? b)
    {
        return !Equals(a, b);
    }
    
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x?.GetHashCode() ?? 0)
            .Aggregate((x, y) => x ^ y);
    }
}