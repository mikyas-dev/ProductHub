using ProductHub.Domain.Common.Models;

namespace ProductHub.Domain.Common.Models;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}